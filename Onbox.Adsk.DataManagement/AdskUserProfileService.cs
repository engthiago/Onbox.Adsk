using Onbox.Abstractions.V9;
using Onbox.Adsk.DataManagement.Core;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class AdskUserProfileService
    {
        private readonly IHttpService httpService;
        private readonly IAdskForgeConfigService forgeConfig;

        public AdskUserProfileService(IHttpService httpService, IAdskForgeConfigService forgeConfig)
        {
            this.httpService = httpService;
            this.forgeConfig = forgeConfig;
        }

        /// <summary>
        /// Gets the User Profile using a 3leg Access Token
        /// </summary>
        public async Task<AdskUserProfile> GetAdskUserProfile(string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                            + $"userprofile/v1/users/@me";

            var userProfile = await this.httpService.GetAsync<AdskUserProfile>(endpoint, token);
            return userProfile;
        }
    }
}
