using System.Diagnostics.CodeAnalysis;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Bananva.Utilities.Assets
{
    public class LocalLocalDataProvider : ILocalDataProvider
    {
        public bool TryLoad<T>(string path, [NotNullWhen(true)] out T? data)
        {
            data = default;
            var fullPath = Path.Join(Application.dataPath, path);
            var isExists = File.Exists(fullPath);
            if (!isExists)
            {
                return false;
            }

            var readAllText = File.ReadAllText(fullPath);
            data = JsonConvert.DeserializeObject<T>(readAllText);
            return isExists;
        }
    }
}