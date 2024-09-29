using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;

namespace SpaceStellar.Utility.DataSource
{
    public static class DataSourcesUtils
    {
        /// <summary>
        /// <para> Позволяет подписаться на готовность переданного источника данных. </para>
        /// <para> <paramref name="onNext"/> вызовется сразу, если все источники данных готовы. </para>
        /// </summary>
        /// <param name="onNext"> действие при готовности всех источников данных </param>
        /// <param name="token"> токен отмены </param>
        /// <param name="dataSource"> Источник данных </param>
        public static void SubscribeToAllDataReady(this IAsyncDataSource dataSource, Action onNext,
            CancellationToken token)
        {
            SubscribeToAllDataReady(onNext, token, dataSource);
        }

        /// <summary>
        /// <para> Позволяет подписаться на готовность переданного источника данных. </para>
        /// <para> <paramref name="onNext"/> Вызовется сразу, если все источники данных готовы. </para>
        /// </summary>
        /// <param name="onNext"> действие при готовности всех источников данных </param>
        /// <param name="dataSource"> Источник данных </param>
        public static IDisposable SubscribeToAllDataReady(this IAsyncDataSource dataSource, Action onNext)
        {
            return SubscribeToAllDataReady(onNext, dataSource);
        }

        /// <summary>
        /// <para> Позволяет подписаться на готовность всех переданных источников данных. </para>
        /// <para> <paramref name="onNext"/> вызовется сразу, если все источники данных готовы. </para>
        /// </summary>
        /// <param name="onNext"> действие при готовности всех источников данных </param>
        /// <param name="sources"> Источники данных </param>
        public static IDisposable SubscribeToAllDataReady(Action onNext, params IAsyncDataSource[] sources)
        {
            return DataReadyPropertiesToAsyncEnumerable(sources)
                .ToObservable()
                .Subscribe(_ => { onNext(); });
        }

        public static IDisposable SubscribeToAllDataReadyAndNotifyAfterReady(
            this IAsyncDataSource self,
            Action onNext,
            params IAsyncDataSource[] sources) =>
            SubscribeToAllDataReady(
                () =>
                {
                    onNext();
                    SetDataReady(self);
                }, sources);

        /// <summary>
        /// <para> Позволяет подписаться на готовность всех переданных источников данных. </para>
        /// <para> <paramref name="onNext"/> вызовется сразу, если все источники данных готовы. </para>
        /// </summary>
        /// <param name="onNext"> действие при готовности всех источников данных </param>
        /// <param name="token"> токен отмены </param>
        /// <param name="sources"> Источники данных </param>
        public static void SubscribeToAllDataReady(Action onNext, CancellationToken token,
            params IAsyncDataSource[] sources)
        {
            DataReadyPropertiesToAsyncEnumerable(sources)
                .ToObservable()
                .Subscribe(_ => { onNext(); })
                .AddTo(token);
        }

        private static IAsyncEnumerable<bool[]> DataReadyPropertiesToAsyncEnumerable(
            IAsyncDataSource[] sources)
        {
            return Observable.ZipLatest(sources
                    .Distinct() // Одна и та же реализация может реализовывать несколько интерфейсов. Исключаем дубликаты
                    .Select(source => source.GetDataReady()))
                .Where(list => list.All(ready => ready))
                .ToAsyncEnumerable();
            // return sources
            //     .Distinct() // Одна и та же реализация может реализовывать несколько интерфейсов. Исключаем дубликаты
            //     .Select(source => source.GetDataReady())
            //     .CombineLatest()
            //     .Where(list => list.All(ready => ready))
            //     .ToUniTaskAsyncEnumerable();
        }

        /// <summary>
        /// Бросит эксепшн, если один или более источников данных не готов.
        /// </summary>
        /// <exception cref="DataSourceNotReadyException"></exception>
        public static void ThrowIfDataNotReady(params IAsyncDataSource[] sources)
        {
            var notReadySource = sources.FirstOrDefault(source => !source.GetDataReady().CurrentValue);

            if (notReadySource != null)
            {
                throw new DataSourceNotReadyException(
                    $"Data source of type {notReadySource.GetType().Name} is not ready");
            }
        }

        /// <summary>
        /// Бросит эксепшн, если источник данных не готов.
        /// </summary>
        /// <exception cref="DataSourceNotReadyException"></exception>
        public static void ThrowIfDataNotReady(this IAsyncDataSource source)
        {
            if (!source.GetDataReady().CurrentValue)
            {
                throw new DataSourceNotReadyException($"Data source of type {source.GetType().Name} is not ready");
            }
        }

        /// <summary>
        /// Бросит эксепшн c сообщением, если источник данных не готов.
        /// </summary>
        /// <exception cref="DataSourceNotReadyException"></exception>
        public static void ThrowIfDataNotReady(this IAsyncDataSource source, string message)
        {
            if (!source.GetDataReady().CurrentValue)
            {
                throw new DataSourceNotReadyException(message);
            }
        }

        /// <summary>
        /// <para> Обёртка <see cref="DataSourceBehaviour"/> для сброса напрямую. </para>
        /// </summary>
        public static void Reset(this IAsyncDataSource source)
        {
            source.DataSourceBehaviour.Reset();
        }

        /// <summary>
        /// <para> Обёртка <see cref="DataSourceBehaviour"/> для получения <c>Resettable</c> напрямую </para>
        /// </summary>
        public static bool IsResettable(this IAsyncDataSource source)
        {
            return source.DataSourceBehaviour.Resettable;
        }

        /// <summary>
        /// <para> Обёртка <see cref="DataSourceBehaviour"/> для получения <c>DataReady</c> напрямую </para>
        /// </summary>
        public static ReadOnlyReactiveProperty<bool> GetDataReady(this IAsyncDataSource source)
        {
            return source.DataSourceBehaviour.DataReady;
        }

        /// <summary>
        /// <para> Обёртка <see cref="DataSourceBehaviour"/> для установки <c>DataReady</c> напрямую </para>
        /// </summary>
        public static void SetDataReady(this IAsyncDataSource source)
        {
            source.DataSourceBehaviour.SetReady();
        }

        /// <returns> Готов ли источник данных к работе. </returns>
        public static bool IsReady(this IAsyncDataSource source)
        {
            return source.DataSourceBehaviour.DataReady.CurrentValue;
        }

        /// <returns> Готовы ли источники данных к работе. </returns>
        public static bool AreReady(params IAsyncDataSource[] sources)
        {
            return sources.All(source => source.IsReady());
        }
    }
}