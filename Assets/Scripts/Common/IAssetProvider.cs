using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SpaceStellar.Common
{
    public interface IAssetProvider
    {
        UniTask<T> Load<T>(string name, CancellationToken token) where T : Object;
    }
}