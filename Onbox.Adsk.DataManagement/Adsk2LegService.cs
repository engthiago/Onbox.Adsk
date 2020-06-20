using Onbox.Adsk.DataManagement.Core;
using Onbox.Standard.Core.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class Adsk2LegService
    {
        private readonly IHttpService httpService;
        private readonly IAdskForgeConfigService forgeConfig;
        private readonly IAdsk2LegStore adsk2LegStore;

        public Adsk2LegService(IHttpService httpService, IAdskForgeConfigService forgeConfig, IAdsk2LegStore adsk2LegStore)
        {
            this.httpService = httpService;
            this.forgeConfig = forgeConfig;
            this.adsk2LegStore = adsk2LegStore;
        }

        /// <summary>
        /// Checks if is internally authenticated with Adsk, internal Token is used for reading and writting data and should NOT be exposed for client agents
        /// </summary>
        public async Task<bool> IsInternalAuthenticated()
        {
            var internalAuth = await this.adsk2LegStore.GetInternalAuthorization();
            return internalAuth != null 
                && !string.IsNullOrWhiteSpace(internalAuth.AccessToken) 
                && internalAuth.Expiration > DateTime.UtcNow.AddMilliseconds(10000) ?
                    true : false;
        }

        /// <summary>
        /// Gets the internal Adsk token, internal Token is used for reading and writting data and should NOT be exposed for client agents
        /// </summary>
        public async Task<AdskAuth> GetInternalAuthorization()
        {
            var isAuth = await this.IsInternalAuthenticated();
            if (!isAuth)
            {
                var internalAuth = await this.InternalAuthorize();
                return internalAuth;
            }
            else
            {
                return await this.adsk2LegStore.GetInternalAuthorization();
            }
        }

        private async Task<AdskAuth> InternalAuthorize()
        {
            var scope = "data:search data:read bucket:read bucket:create data:write bucket:delete account:read account:write";
            return await Authorize(scope);
        }

        /// <summary>
        /// Checks if is external authenticated with Adsk, external Token is used ONLY for reading data and CAN be exposed for client agents
        /// </summary>
        public async Task<bool> IsExternalAuthenticated()
        {
            var externalAuth = await this.adsk2LegStore.GetExternalAuthorization();
            return externalAuth != null
                && !string.IsNullOrWhiteSpace(externalAuth.AccessToken)
                && externalAuth.Expiration > DateTime.UtcNow.AddMilliseconds(10000) ?
                    true : false;
        }

        /// <summary>
        /// Gets the external Adsk token, external Token is used ONLY for reading data and CAN be exposed for client agents
        /// </summary>
        public async Task<AdskAuth> GetExternalAuthorization()
        {
            var isAuth = await this.IsExternalAuthenticated();
            if (!isAuth)
            {
                var externalAuth = await this.ExternalAuthorize();
                return externalAuth;
            }
            else
            {
                return await this.adsk2LegStore.GetExternalAuthorization();
            }
        }

        private async Task<AdskAuth> ExternalAuthorize()
        {
            var scope = "data:read";
            return await Authorize(scope);
        }

        private async Task<AdskAuth> Authorize(string scope)
        {
            var encodedScope = WebUtility.UrlEncode(scope);

            var form = new Dictionary<string, string>();
            form.Add("client_id", this.forgeConfig.GetClientId());
            form.Add("client_secret", this.forgeConfig.GetClientSecret());
            form.Add("grant_type", "client_credentials");
            form.Add("scope", encodedScope);

            string endpoint = this.forgeConfig.GetBaseUrl()
                                + $"authentication/v1/authenticate";

            var auth = await this.httpService.PostFormAsync<AdskAuth>(endpoint, form);
            auth.Expiration = DateTime.UtcNow.AddSeconds(auth.ExpiresIn);
            return auth;
        }
    }
}
