using System.Diagnostics.CodeAnalysis;

namespace Bananva.Utilities.Assets
{
    public interface ICachedDataProvider
    {
        bool TryLoad<T>(string key, [NotNullWhen(true)] out T? data);

        void Save<T>(string key, T data);
    }
}