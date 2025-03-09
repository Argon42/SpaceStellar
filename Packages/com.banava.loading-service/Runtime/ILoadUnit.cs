using Cysharp.Threading.Tasks;

namespace Bananva.LoadingService
{
    public interface ILoadUnit
    {
        UniTask Load();
    }

    public interface ILoadUnit<in T>
    {
        UniTask Load(T param);
    }
}