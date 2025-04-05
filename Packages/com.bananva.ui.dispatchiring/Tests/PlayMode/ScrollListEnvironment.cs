using System.Linq;
using Bananva.UI.Dispatching.Presenters.Lists;
using Bananva.UI.Dispatching.Tests.PlayMode.Common;
using Bananva.UI.Dispatching.Views;
using Bananva.Utilities.Tests;
using UnityEngine;

namespace Bananva.UI.Dispatching.Tests.PlayMode
{
    internal class ScrollListEnvironment
    {
        public UguiPoolListView PoolList { get; }
        public IReactiveListPresenter<Data> Presenter { get; }

        public ScrollListEnvironment(UguiPoolListView poolList, IReactiveListPresenter<Data> presenter)
        {
            PoolList = poolList;
            Presenter = presenter;
        }

        public static ScrollListEnvironment Create() => EnvironmentBuilder<ScrollListEnvironment>
            .Create()
            .AddInstallerFromResources<ScrollListInstaller>("ScrollListInstaller")
            .AddInstallerMethod(container =>
            {
                container.BindReactiveListPresenter<Data>(
                    typeof(ImagePresenter),
                    typeof(TextPresenter));
            })
            .Build();

        public int CountOfChildrenInPoolList() => PoolList.transform
            .Cast<Transform>()
            .Count(transform => transform.gameObject.activeSelf);

        public Transform GetItem(int i) => PoolList
            .transform
            .Cast<Transform>()
            .Where(transform => transform.gameObject.activeSelf)
            .Select((transform, index) => (transform, index))
            .First(tuple => tuple.index == i)
            .transform;
    }
}