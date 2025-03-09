using System;
using Bananva.Utilities.DataSource;

namespace SpaceStellar.Common.Data
{
    public class ClientProfileDataSource : IAsyncDataSource
    {
        public ClientProfile? ClientProfile { get; private set; }

        public DataSourceBehaviour DataSourceBehaviour { get; }

        public ClientProfileDataSource()
        {
            DataSourceBehaviour = new DataSourceBehaviour(Reset, false);
        }

        public void Reset()
        {
            ClientProfile = null;
        }

        public void SetClientProfile(ClientProfile clientProfile)
        {
            ClientProfile = clientProfile ?? throw new ArgumentNullException(nameof(clientProfile));
            DataSourceBehaviour.SetReady();
        }
    }
}