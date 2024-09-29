using SpaceStellar.Utility;
using UnityEngine;
using Zenject;

namespace SpaceStellar.Common.Ui.Ugui
{
    [CreateAssetMenu(fileName = "UguiScreenInstaller", menuName = "Stellar/Installers/UguiScreenInstaller", order = 1)]
    public class UguiScreenInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private UguiRootInstaller uiRootInstaller = default!;
        [SerializeField] private UguiScreenPrefabStorage uguiScreenPrefabStorage = default!;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<UguiScreenContainer>().AsSingle();
            Container.Bind<UguiScreenPrefabStorage>().FromInstance(uguiScreenPrefabStorage).AsSingle();
            Container.CreateInstallerFromPrefab(uiRootInstaller);
        }
    }
}