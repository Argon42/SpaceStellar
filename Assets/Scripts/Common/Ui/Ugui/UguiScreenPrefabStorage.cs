using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SpaceStellar.Common.Ui.Ugui
{
    [CreateAssetMenu(fileName = "UguiScreenPrefabStorage", menuName = "Stellar/UI/UguiScreenPrefabStorage", order = 1)]
    public class UguiScreenPrefabStorage : ScriptableObject
    {
        [SerializeField] private List<UguiScreenView> screenPrefabs = new();

        public T GetScreenPrefab<T>() where T : IScreenView
        {
            IScreenView prefab = screenPrefabs.First(view => view is T);
            return (T)prefab;
        }
    }
}