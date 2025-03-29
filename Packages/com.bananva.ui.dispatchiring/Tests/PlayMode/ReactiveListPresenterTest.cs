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
        public IEnumerator ReactiveListPresenterTestSimplePasses()
        {
            var env = ScrollListEnvironment.Create();
            var data = new ObservableList<Data>();

            env.Presenter.SetView(env.PoolList);
            env.Presenter.SetModel(data);
            env.Presenter.Open();

            yield return new WaitForEndOfFrame();
            Assert.AreEqual(0, env.CountOfChildrenInPoolList());

            data.Add(new TextData() { Id = 1, Prefixfix = "v_" });
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(1, env.CountOfChildrenInPoolList());

            data.AddRange(Enumerable.Range(2, 10).Select(i => new ImageData() { Id = i, Color = Color.red / i }));
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(11, env.CountOfChildrenInPoolList());

            data.Insert(0, new TextData() { Id = 0, Prefixfix = "m_" });
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(12, env.CountOfChildrenInPoolList());

            data.RemoveAt(0);
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(11, env.CountOfChildrenInPoolList());

            data.RemoveRange(0, 6);
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(5, env.CountOfChildrenInPoolList());

            data.Clear();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(0, env.CountOfChildrenInPoolList());

            env.Presenter.Close();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(0, env.CountOfChildrenInPoolList());

            data.Add(new TextData() { Id = 1, Prefixfix = "v_" });
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(0, env.CountOfChildrenInPoolList());

            env.Presenter.Open();
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(1, env.CountOfChildrenInPoolList());
        }
        
        [UnityTest]
        public IEnumerator AddItemAfterDeletedItems() 
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

            data.RemoveAt(1);
            data.Add(new TextData() { Id = 4, Prefixfix = "r_" });
            yield return new WaitForEndOfFrame();
            
            Assert.AreEqual(3, env.CountOfChildrenInPoolList());
        }

        [UnityTest]
        public IEnumerator SwapElementsChangeItemsToCorrectViewTypes()
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

            data.Move(0, 1);
            yield return new WaitForEndOfFrame();

            Assert.True(env.GetItem(0).GetComponent<ImageView>());
            Assert.True(env.GetItem(1).GetComponent<TextView>());
        }
        
        [UnityTest]
        public IEnumerator ReplaceElementChangeItemToCorrectViewType()
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

            data[0] = new ImageData() { Id = 2, Color = Color.green };
            yield return new WaitForEndOfFrame();
            Assert.True(env.GetItem(0).GetComponent<ImageView>());
            Assert.True(env.GetItem(1).GetComponent<ImageView>());
        }
        
        [UnityTest]
        public IEnumerator RemoveElementChangeItemToCorrectViewType()
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

            data.RemoveAt(0);
            yield return new WaitForEndOfFrame();
            Assert.AreEqual(1, env.CountOfChildrenInPoolList());
            Assert.True(env.GetItem(0).GetComponent<ImageView>());
        }
        
        
        [UnityTest]
        public IEnumerator RemoveElementsChangeItemsToCorrectViewType()
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

            data.RemoveRange(0, 5);
            yield return new WaitForEndOfFrame();

            Assert.AreEqual(1, env.CountOfChildrenInPoolList());
            Assert.True(env.GetItem(0).GetComponent<ImageView>());
        }
    }
}