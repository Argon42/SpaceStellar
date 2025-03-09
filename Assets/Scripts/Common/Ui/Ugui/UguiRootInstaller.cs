using UnityEngine;
using Zenject;

namespace Bananva.UI.Dispatchiring.Ugui
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