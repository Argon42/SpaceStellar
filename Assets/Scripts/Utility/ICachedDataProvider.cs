using System.Diagnostics.CodeAnalysis;

namespace SpaceStellar.Utility
{
    public interface ICachedDataProvider
    {
        bool TryLoad<T>(string key, [NotNullWhen(true)] out T? data);

        void Save<T>(string key, T data);
    }
}