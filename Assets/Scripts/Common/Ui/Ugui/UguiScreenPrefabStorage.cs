using System.Collections.Generic;
using System.Linq;
using SpaceStellar.Common.Ui.Abstraction;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Ugui
{
    [CreateAssetMenu(fileName = "UguiScreenPrefabStorage", menuName = "Stellar/UI/UguiScreenPrefabStorage", order = 1)]
    public class UguiScreenPrefabStorage : ScriptableObject
    {
        [SerializeField] private List<UguiScreenView> screenPrefabs = new();

        public T? GetScreenPrefab<T>() where T : IView
        {
            IView? prefab = screenPrefabs.FirstOrDefault(view => view is T);
            
            if (prefab == null)
            {
                return default!;
            }
            return (T)prefab;
        }
    }
}