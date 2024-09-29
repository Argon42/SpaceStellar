using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace SpaceStellar.Utility
{
    public class PlayerPrefsCachedDataProvider : ICachedDataProvider
    {
        public bool TryLoad<T>(string key, [NotNullWhen(true)] out T? clientProfile)
        {
            clientProfile = default!;

            if (!PlayerPrefs.HasKey(key))
                return false;

            clientProfile = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
            return clientProfile != null;
        }

        public void Save<T>(string key, T data)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }
    }
}