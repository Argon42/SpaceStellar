using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Bananva.Utilities.Tests
{
    public class EnvironmentBuilder<T> where T : class
    {
        private readonly List<Type> _installers = new();
        private readonly List<MonoInstaller> _installersWithObject = new();
        private readonly List<Action<DiContainer>> _afterInstall = new();
        private readonly List<Action<DiContainer>> _onInstall = new();
        private readonly List<Action<DiContainer>> _afterResolve = new();
        private ZenjectSettings? _overrideSettings;

        public static EnvironmentBuilder<T> Create() => new();

        public T Build()
        {
            typeof(ProjectContext)
                .GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static)!
                .SetValue(null, new GameObject("ProjectContext").AddComponent<ProjectContext>());

            typeof(ProjectContext)
                .GetMethod("Initialize", BindingFlags.NonPublic | BindingFlags.Instance)!
                .Invoke(ProjectContext.Instance, null);

            SceneContext sceneContext = SceneContext.Create();
            sceneContext.gameObject.SetActive(false);
            sceneContext.ParentNewObjectsUnderSceneContext = true;

            sceneContext.PreInstall += InstallBindingsRoot;
            sceneContext.Install();
            sceneContext.PreInstall -= InstallBindingsRoot;

            _afterInstall.ForEach(action => action?.Invoke(sceneContext.Container));

            sceneContext.Resolve();
            _afterResolve.ForEach(action => action?.Invoke(sceneContext.Container));

            sceneContext.gameObject.SetActive(true);
            sceneContext.gameObject.GetComponent<SceneKernel>().Start();

            return sceneContext.Container.Resolve<T>();

            void InstallBindingsRoot()
            {
                sceneContext.Container.Settings =
                    _overrideSettings ?? new ZenjectSettings();
                InitializeContainer(sceneContext.Container);
            }
        }

        public EnvironmentBuilder<T> AddInstaller<TInstaller>() where TInstaller : IInstaller
        {
            _installers.Add(typeof(TInstaller));
            return this;
        }

        public EnvironmentBuilder<T> AddInstallerFromResources<TInstaller>(string path) where TInstaller : MonoInstaller
        {
            MonoInstaller installer = Resources.Load<TInstaller>(path);

            _installersWithObject.Add(installer);

            return this;
        }

        public EnvironmentBuilder<T> AddAfterInstall(Action<DiContainer> afterInstall)
        {
            _afterInstall.Add(afterInstall);
            return this;
        }

        public EnvironmentBuilder<T> AddAfterResolve(Action<DiContainer> afterResolve)
        {
            _afterResolve.Add(afterResolve);
            return this;
        }

        public EnvironmentBuilder<T> OverrideSettings(ZenjectSettings settings)
        {
            _overrideSettings = settings;
            return this;
        }

        public EnvironmentBuilder<T> AddInstallerMethod(Action<DiContainer> onInstall)
        {
            _onInstall.Add(onInstall);
            return this;
        }

        private void InitializeContainer(DiContainer container)
        {
            _onInstall.ForEach(action => action?.Invoke(container));
            _installers.ForEach(installerType => ((IInstaller)container.Instantiate(installerType)).InstallBindings());
            _installersWithObject.Select(Object.Instantiate)
                .ForEach(
                    installer =>
                    {
                        container.Inject(installer);
                        installer.InstallBindings();
                        container.InjectGameObject(installer.gameObject);
                    });

            container.Bind<T>().AsSingle();
        }
    }
}