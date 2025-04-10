﻿using System;
using System.Diagnostics;
using System.Threading;
using Bananva.Utilities.Logger;
using Cysharp.Threading.Tasks;
using R3;
using PlayerLoopHelper = Cysharp.Threading.Tasks.PlayerLoopHelper;

namespace Bananva.LoadingService
{

    public sealed class LoadingService : IDisposable
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _watch = new();

        private readonly CompositeDisposable _disposable = new();

        public LoadingService(ILogger logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnLoadingBegin(object unit)
        {
            _watch.Restart();
            _logger.Debug($"{unit.GetType().Name} loading is started");
        }

        private async UniTask OnLoadingFinish(object unit, bool isError)
        {
            _watch.Stop();
            _logger.Debug(
                $"{unit.GetType().Name} is {(isError ? "NOT " : "")}loaded with time {_watch.ElapsedMilliseconds}ms");

            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            var mainThreadId = PlayerLoopHelper.MainThreadId;

            if (mainThreadId != currentThreadId)
            {
                _watch.Restart();
                _logger.Debug(
                    $"[THREAD] start switching from '{currentThreadId}' thread to main thread '{mainThreadId}'");
                await UniTask.SwitchToMainThread();
                _watch.Stop();
                _logger.Debug($"[THREAD] switch finished with time {_watch.ElapsedMilliseconds}");
            }
        }

        public async UniTask BeginLoading(ILoadUnit loadUnit, bool skipExceptionThrow = false)
        {
            var isError = true;

            try
            {
                OnLoadingBegin(loadUnit);
                await loadUnit.Load();
                isError = false;
            }
            catch (Exception e)
            {
                _logger.Exception(e);

                if (!skipExceptionThrow)
                {
                    throw;
                }
            }
            finally
            {
                await OnLoadingFinish(loadUnit, isError);
            }
        }

        public async UniTask BeginLoading(IDisposableLoadUnit unit, bool skipExceptionThrow = false)
        {
            _disposable.Add(unit);
            await BeginLoading((ILoadUnit)unit, skipExceptionThrow);
        }

        public async UniTask BeginLoading<T>(ILoadUnit<T> loadUnit, T param, bool skipExceptionThrow = false)
        {
            var isError = true;

            try
            {
                OnLoadingBegin(loadUnit);
                await loadUnit.Load(param);
                isError = false;
            }
            catch (Exception e)
            {
                _logger.Exception(e);

                if (!skipExceptionThrow)
                {
                    throw;
                }
            }
            finally
            {
                await OnLoadingFinish(loadUnit, isError);
            }
        }

        public async UniTask BeginLoading<T>(IDisposableLoadUnit<T> unit, T param, bool skipExceptionThrow = false)
        {
            _disposable.Add(unit);
            await BeginLoading((ILoadUnit<T>)unit, param, skipExceptionThrow);
        }

        public async UniTask BeginLoading(bool skipExceptionThrow = false, params ILoadUnit[] units)
        {
            foreach (var loadUnit in units)
                await BeginLoading(loadUnit, skipExceptionThrow);
        }

        public async UniTask BeginLoadingParallel(string logName, bool skipExceptionThrow = false,
            params ILoadUnit[] units)
        {
            var isError = true;

            try
            {
                OnLoadingBegin(logName);
                var t = UniTask.WhenAll(units.Select(e => e.Load()));
                await t;
                isError = false;
            }
            catch (Exception e)
            {
                _logger.Exception(e);

                if (!skipExceptionThrow)
                {
                    throw;
                }
            }
            finally
            {
                await OnLoadingFinish(logName, isError);
            }
        }
    }
}