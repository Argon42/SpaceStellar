using System;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace Bananva.Utilities.Tests
{
    internal static class ZenjectTestUtil
    {
        private static string? m_unitTestRunnerGameObjectName;

        /// <summary>
        /// В Unity Test Framework версии 1.x и 2.x используются разные имена
        /// для запуска тестирования. Поскольку нет способа узнать, какая
        /// версия используется во время компиляции, нам приходится
        /// использовать обе версии с первого раза.
        /// </summary>
        private static string UnitTestRunnerGameObjectName =>
            m_unitTestRunnerGameObjectName ??=
                GameObject.Find("Code-based tests runner")?.name // v1
                ?? GameObject.Find("tests runner")?.name; // v2

        /// <summary>
        /// Нам нужно использовать DestroyImmediate, чтобы все IDisposable классы и прочие объекты были обработаны непосредственно перед следующим тестом
        /// Поскольку Destroy помечает объект для уничтожения и удаляется из сцены во время следующего цикла обновления,
        /// а DestroyImmediate уничтожает объект или компонент немедленно.
        /// </summary>
        /// <param name="immediate"></param>
        public static void DestroyEverythingExceptTestRunner(bool immediate)
        {
            GameObject testRunner = GameObject.Find(UnitTestRunnerGameObjectName);
            Assert.IsNotNull(testRunner);
            Object.DontDestroyOnLoad(testRunner);

            DestroyObjectsOnAllScenes();
            DestroyProjectContext(immediate);
        }

        /// <summary>
        /// Нам нужно использовать DestroyImmediate, чтобы все IDisposable классы и прочие объекты были обработаны непосредственно перед следующим тестом
        /// Поскольку Destroy помечает объект для уничтожения и удаляется из сцены во время следующего цикла обновления,
        /// а DestroyImmediate уничтожает объект или компонент немедленно.
        /// </summary>
        /// <param name="immediate"></param>
        public static void FindAndDestroySceneContext(bool immediate)
        {
            SceneContext sceneContext = Object.FindObjectOfType<SceneContext>();

            if (sceneContext == null)
            {
                return;
            }

            if (immediate)
            {
                Object.DestroyImmediate(sceneContext.gameObject);
            }
            else
            {
                Object.Destroy(sceneContext.gameObject);
            }
        }

        private static void DestroyProjectContext(bool immediate)
        {
            if (!ProjectContext.HasInstance)
            {
                return;
            }

            Action<Object> destroyAction = immediate ? Object.DestroyImmediate : Object.Destroy;
            ProjectContext.Instance.gameObject
                .scene
                .GetRootGameObjects()
                .Where(IsNotTestRunner)
                .ForEach(destroyAction);
        }

        private static void DestroyObjectsOnAllScenes() =>
            Enumerable.Range(0, SceneManager.sceneCount)
                .Select(SceneManager.GetSceneAt)
                .SelectMany(scene => scene.GetRootGameObjects())
                .Where(IsNotTestRunner)
                .ForEach(Object.DestroyImmediate);

        private static bool IsNotTestRunner(Object obj) => obj.name != UnitTestRunnerGameObjectName;
    }
}