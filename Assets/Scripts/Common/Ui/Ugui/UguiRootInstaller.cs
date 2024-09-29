using UnityEngine;
using Zenject;

namespace SpaceStellar.Common.Ui.Ugui
{
    public class UguiRootInstaller : MonoInstaller
    {
        [SerializeField] private Canvas canvas = default!;

        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromInstance(canvas).AsSingle();
        }
    }
}