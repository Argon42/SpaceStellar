using System.Collections.Generic;
using System.Linq;
using Bananva.UI.Dispatching.Api;
using Bananva.UI.Dispatching.Views;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Ugui
{
    [CreateAssetMenu(fileName = "UguiScreenPrefabStorage", menuName = "Stellar/UI/UguiScreenPrefabStorage", order = 1)]
    public class UguiScreenPrefabStorage : ScriptableObject
    {
        [SerializeField] private List<UguiView> screenPrefabs = new();

        public T? GetViewPrefab<T>() where T : IView
        {
            IView? prefab = screenPrefabs.FirstOrDefault(view => view is T);

            if (prefab == null)
            {
                return default;
            }

            return (T)prefab;
        }
    }
}