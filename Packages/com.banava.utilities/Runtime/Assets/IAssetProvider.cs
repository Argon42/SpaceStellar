using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Bananva.Utilities.Assets
{
    public interface IAssetProvider
    {
        UniTask<T> Load<T>(string name, CancellationToken token) where T : Object;
    }
}