using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Zenject;

namespace Bananva.Utilities.Tests
{
    /// <summary>
    /// Базовый класс для интеграционных тестов реализующий обязательные методы.
    /// Создание токена отмены на основе параметра командной строки.
    /// Уничтожение сцены для запуска следующего теста или для тестирования кейсов с уничтожением сцены.
    /// </summary>
    public class BaseIntegrationTest
    {
        private CancellationTokenSource? _cts;
        protected CancellationToken CancellationToken => _cts?.Token ?? CancellationToken.None;

        private static readonly TimeSpan Timeout = TimeSpan.FromMinutes(5);

        [SetUp]
        protected void SetUp()
        {
            _cts = new CancellationTokenSource(Timeout);
        }

        [TearDown]
        protected void ClearScene()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            DestroyEverythingInternal(true);
        }

        /// <summary>
        /// Метод для отчистки сцены для тестирования кейсов с уничтожением сцены.
        /// Ожидает один кадр после уничтожения объектов и для сборщика мусора.
        /// </summary>
        protected async UniTask DestroyEverything()
        {
            DestroyEverythingInternal(false);
            await UniTask.NextFrame(CancellationToken);
        }

        private static void DestroyEverythingInternal(bool immediate)
        {
            ZenjectTestUtil.FindAndDestroySceneContext(immediate);

            ZenjectTestUtil.DestroyEverythingExceptTestRunner(immediate);
            StaticContext.Clear();
        }
    }
}