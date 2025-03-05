using System.Diagnostics.CodeAnalysis;

namespace SpaceStellar.Utility
{
    public interface ILocalDataProvider
    {
        bool TryLoad<T>(string path, [NotNullWhen(true)] out T? data);
    }
}