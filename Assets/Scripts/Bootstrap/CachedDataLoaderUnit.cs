using Cysharp.Threading.Tasks;
using SpaceStellar.Common.Data;
using SpaceStellar.Utility;

namespace SpaceStellar.Bootstrap
{
    public class CachedDataLoaderUnit : ILoadUnit
    {
        private readonly ClientProfileDataSource _profileDataSource;
        private readonly ClientConfiguration _configuration;
        private readonly ICachedDataProvider _cachedDataProvider;

        public CachedDataLoaderUnit(
            ClientProfileDataSource profileDataSource,
            ClientConfiguration configuration,
            ICachedDataProvider cachedDataProvider)
        {
            _profileDataSource = profileDataSource;
            _configuration = configuration;
            _cachedDataProvider = cachedDataProvider;
        }

        public UniTask Load()
        {
            if (!_cachedDataProvider.TryLoad($"Client{_configuration.ClientOffsetId}", out ClientProfile? profile))
            {
                profile = CreateProfile();
            }

            _profileDataSource.SetClientProfile(profile);

            return UniTask.CompletedTask;
        }

        private ClientProfile CreateProfile()
        {
            var profile = new ClientProfile();
            return profile;
        }
    }
}