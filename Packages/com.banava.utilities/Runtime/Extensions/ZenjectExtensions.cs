using UnityEngine;
using Zenject;

namespace Bananva.Utilities.Extensions
{
    public static class ZenjectExtensions
    {
        public static void CreateInstallerFromPrefab(this DiContainer container, MonoInstaller prefab)
        {
            var installer = Object.Instantiate(prefab);
            container.Inject(installer);
            installer.InstallBindings();
            container.InjectGameObject(installer.gameObject);
        }

        public static void BindClassWithPool<T>(this DiContainer container)
        {
            container.Bind<T>().AsTransient().WhenInjectedInto<MemoryPool<T>>();
            container.BindMemoryPoolCustomInterface<T, MemoryPool<T>, IMemoryPool<T>>()
                .WithInitialSize(2)
                .ExpandByDoubling();
        }
    }
}