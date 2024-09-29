using Zenject;

namespace SpaceStellar.Game
{
    public class GameInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameFlow>().AsSingle();
        }
    }
}