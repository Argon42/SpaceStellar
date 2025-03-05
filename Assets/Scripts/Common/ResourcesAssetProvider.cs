using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpaceStellar.Common
{
    public class ResourcesAssetProvider : IAssetProvider
    {
        public UniTask<T> Load<T>(string name, CancellationToken token) where T : Object => UniTask.FromResult(Resources.Load<T>(name));
    }
}