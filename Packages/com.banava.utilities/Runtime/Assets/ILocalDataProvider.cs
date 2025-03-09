using System.Diagnostics.CodeAnalysis;

namespace Bananva.Utilities.Assets
{
    public interface ILocalDataProvider
    {
        bool TryLoad<T>(string path, [NotNullWhen(true)] out T? data);
    }
}