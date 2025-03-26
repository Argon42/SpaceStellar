using System.Collections;
using System.Linq;
using Bananva.Utilities.Tests;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using ObservableCollections;
using Tests.PlayMode.Common;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    [TestFixture]
    public class ReactiveListPresenterTest : BaseIntegrationTest
    {
        [UnityTest]
        public IEnumerator ReactiveListPresenterTestSimplePasses() => UniTask.ToCoroutine(async () =>
        {
            var env = ScrollListEnvironment.Create();
            var data = new ObservableList<Data>();

            env.Presenter.SetView(env.PoolList);
            env.Presenter.SetModel(data);
            env.Presenter.Open();

            Assert.AreEqual(0, env.CountOfChildrenInPoolList());

            data.Add(new TextData() { Id = 1, Prefixfix = "v_" });
            Assert.AreEqual(1, env.CountOfChildrenInPoolList());

            data.AddRange(Enumerable.Range(2, 10).Select(i => new ImageData() { Id = i, Color = Color.red / i }));
            Assert.AreEqual(11, env.CountOfChildrenInPoolList());

            data.Insert(0, new TextData() { Id = 0, Prefixfix = "m_" });
            Assert.AreEqual(12, env.CountOfChildrenInPoolList());

            data.RemoveAt(0);
            Assert.AreEqual(11, env.CountOfChildrenInPoolList());

            data.RemoveRange(0, 6);
            Assert.AreEqual(5, env.CountOfChildrenInPoolList());

            data.Clear();
            Assert.AreEqual(0, env.CountOfChildrenInPoolList());

            env.Presenter.Close();
            Assert.AreEqual(0, env.CountOfChildrenInPoolList());

            data.Add(new TextData() { Id = 1, Prefixfix = "v_" });
            Assert.AreEqual(0, env.CountOfChildrenInPoolList());

            env.Presenter.Open();
            Assert.AreEqual(1, env.CountOfChildrenInPoolList());
        });
        
        [UnityTest]
        public IEnumerator AddItemAfterDeletedItems() => UniTask.ToCoroutine(async () =>
        {
            var env = ScrollListEnvironment.Create();
            var data = new ObservableList<Data>()
            {
                new ImageData() { Id = 2, Color = Color.red },
                new TextData() { Id = 1, Prefixfix = "v_" },
                new ImageData() { Id = 2, Color = Color.red },
            };

            env.Presenter.SetView(env.PoolList);
            env.Presenter.SetModel(data);
            env.Presenter.Open();
            await UniTask.Delay(100);

            data.RemoveAt(1);
            data.Add(new TextData() { Id = 4, Prefixfix = "r_" });
            
            Assert.AreEqual(3, env.CountOfChildrenInPoolList());
        });

        [UnityTest]
        public IEnumerator SwapElementsChangeItemsToCorrectViewTypes() => UniTask.ToCoroutine(async () =>
        {
            var env = ScrollListEnvironment.Create();
            var data = new ObservableList<Data>()
            {
                new TextData() { Id = 1, Prefixfix = "v_" },
                new ImageData() { Id = 2, Color = Color.red },
            };

            env.Presenter.SetView(env.PoolList);
            env.Presenter.SetModel(data);
            env.Presenter.Open();
            await UniTask.Delay(100);

            data.Move(0, 1);

            Assert.True(env.GetItem(0).GetComponent<ImageView>());
            Assert.True(env.GetItem(1).GetComponent<TextView>());
        });
        
        [UnityTest]
        public IEnumerator ReplaceElementChangeItemToCorrectViewType() => UniTask.ToCoroutine(async () =>
        {
            var env = ScrollListEnvironment.Create();
            var data = new ObservableList<Data>()
            {
                new TextData() { Id = 1, Prefixfix = "v_" },
                new ImageData() { Id = 2, Color = Color.red },
            };

            env.Presenter.SetView(env.PoolList);
            env.Presenter.SetModel(data);
            env.Presenter.Open();
            await UniTask.Delay(100);

            data[0] = new ImageData() { Id = 2, Color = Color.green };
            Assert.True(env.GetItem(0).GetComponent<ImageView>());
            Assert.True(env.GetItem(1).GetComponent<ImageView>());
        });
        
        [UnityTest]
        public IEnumerator RemoveElementChangeItemToCorrectViewType() => UniTask.ToCoroutine(async () =>
        {
            var env = ScrollListEnvironment.Create();
            var data = new ObservableList<Data>()
            {
                new TextData() { Id = 1, Prefixfix = "v_" },
                new ImageData() { Id = 2, Color = Color.red },
            };

            env.Presenter.SetView(env.PoolList);
            env.Presenter.SetModel(data);
            env.Presenter.Open();
            await UniTask.Delay(100);

            data.RemoveAt(0);
            Assert.AreEqual(1, env.CountOfChildrenInPoolList());
            Assert.True(env.GetItem(0).GetComponent<ImageView>());
        });
        
        
        [UnityTest]
        public IEnumerator RemoveElementsChangeItemsToCorrectViewType() => UniTask.ToCoroutine(async () =>
        {
            var env = ScrollListEnvironment.Create();
            var data = new ObservableList<Data>()
            {
                new TextData() { Id = 1, Prefixfix = "v_" },
                new TextData() { Id = 1, Prefixfix = "v_" },
                new TextData() { Id = 1, Prefixfix = "v_" },
                new TextData() { Id = 1, Prefixfix = "v_" },
                new TextData() { Id = 1, Prefixfix = "v_" },
                new ImageData() { Id = 2, Color = Color.red },
            };

            env.Presenter.SetView(env.PoolList);
            env.Presenter.SetModel(data);
            env.Presenter.Open();
            await UniTask.Delay(100);

            data.RemoveRange(0, 5);
            Assert.AreEqual(1, env.CountOfChildrenInPoolList());
            Assert.True(env.GetItem(0).GetComponent<ImageView>());
        });
    }
}