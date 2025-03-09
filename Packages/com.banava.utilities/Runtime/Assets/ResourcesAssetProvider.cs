using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Bananva.Utilities.Assets
{
    public class ResourcesAssetProvider : IAssetProvider
    {
        public UniTask<T> Load<T>(string name, CancellationToken token) where T : Object => UniTask.FromResult(Resources.Load<T>(name));
    }
}