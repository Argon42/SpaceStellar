using UnityEngine;
using Zenject;

namespace SpaceStellar.Utility
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
    }
}