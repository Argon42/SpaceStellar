using Bananva.UI.Dispatching.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Bananva.UI.Dispatching.Tests.PlayMode
{
    internal class ScrollListInstaller : MonoInstaller<ScrollListInstaller>
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private EventSystem eventSystem;
        [SerializeField] private UguiPoolListView poolList;

        public override void InstallBindings()
        {
            Container.Bind<Canvas>().FromComponentInNewPrefab(canvas).AsSingle();
            Container.Bind<EventSystem>().FromComponentInNewPrefab(eventSystem).AsSingle();
            Container.Bind<UguiPoolListView>().FromMethod(context =>
                    Container.InstantiatePrefabForComponent<UguiPoolListView>(
                        poolList,
                        context.Container
                            .Resolve<Canvas>()
                            .transform))
                .AsSingle();
        }
    }
}