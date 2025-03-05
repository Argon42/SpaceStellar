using System.IO;
using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Data;
using SpaceStellar.Utility;
using UnityEngine;

namespace SpaceStellar.Bootstrap.LoadUnits
{
    public class ClientConfigurationLoadUnit : ILoadUnit
    {
        private readonly ClientConfiguration _clientConfiguration;

        public ClientConfigurationLoadUnit(ClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration;
        }

        public UniTask Load()
        {
            if (!File.Exists("ClientConfiguration.json"))
            {
                return UniTask.CompletedTask;
            }

            var json = File.ReadAllText("ClientConfiguration.json");
            var clientConfiguration = JsonUtility.FromJson<ClientConfiguration>(json);
            _clientConfiguration.ClientOffsetId = clientConfiguration.ClientOffsetId;
            return UniTask.CompletedTask;
        }
    }
}