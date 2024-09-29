using SpaceStellar.Utility.DataSource;

namespace SpaceStellar.Common.Data
{
    public class ClientProfileDataSource : IAsyncDataSource
    {
        public DataSourceBehaviour DataSourceBehaviour { get; } = new(true);
    }
}