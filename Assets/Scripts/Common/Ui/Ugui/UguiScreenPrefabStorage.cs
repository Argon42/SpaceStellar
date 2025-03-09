using System.Collections.Generic;
using System.Linq;
using Bananva.UI.Dispatchiring.Api;
using UnityEngine;

namespace Bananva.UI.Dispatchiring.Ugui
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