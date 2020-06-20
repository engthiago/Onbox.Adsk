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
        private AdskAuth internalAuth;
        private AdskAuth publicAuth;

        private readonly IHttpService httpService;
        private readonly IAdskForgeConfigService forgeConfig;

        public Adsk2LegService(IHttpService httpService, IAdskForgeConfigService forgeConfig)
        {
            this.httpService = httpService;
            this.forgeConfig = forgeConfig;
        }

        /// <summary>
        /// Checks if is internally authenticated with Adsk, internal Token is used for reading and writting data and should NOT be exposed for client agents
        /// </summary>
        public bool IsInternalAuthenticated()
        {
            return this.internalAuth != null 
                && !string.IsNullOrWhiteSpace(internalAuth.AccessToken) 
                && internalAuth.Expiration > DateTime.UtcNow.AddMilliseconds(10000) ?
                    true : false;
        }

        /// <summary>
        /// Gets the internal Adsk token, internal Token is used for reading and writting data and should NOT be exposed for client agents
        /// </summary>
        public async Task<AdskAuth> GetInternalAuthorization()
        {
            if (!this.IsInternalAuthenticated())
            {
                internalAuth = await this.InternalAuthorize();
                return internalAuth;
            }
            else
            {
                return internalAuth;
            }
        }

        private async Task<AdskAuth> InternalAuthorize()
        {
            var scope = "data:search data:read bucket:read bucket:create data:write bucket:delete account:read account:write";
            var encodedScope = WebUtility.UrlEncode(scope);

            return await Authorize(encodedScope);
        }

        /// <summary>
        /// Checks if is public authenticated with Adsk, public Token is used ONLY for reading data and CAN be exposed for client agents
        /// </summary>
        public bool IsPublicAuthenticated()
        {
            return this.publicAuth != null
                && !string.IsNullOrWhiteSpace(publicAuth.AccessToken)
                && publicAuth.Expiration > DateTime.UtcNow.AddMilliseconds(10000) ?
                    true : false;
        }

        /// <summary>
        /// Gets the public Adsk token, public Token is used ONLY for reading data and CAN be exposed for client agents
        /// </summary>
        public async Task<AdskAuth> GetPublicAuthorization()
        {
            if (!this.IsPublicAuthenticated())
            {
                publicAuth = await this.PublicAuthorize();
                return publicAuth;
            }
            else
            {
                return publicAuth;
            }
        }

        private async Task<AdskAuth> PublicAuthorize()
        {
            var scope = "data:read";
            var encodedScope = WebUtility.UrlEncode(scope);

            return await Authorize(encodedScope);
        }

        private async Task<AdskAuth> Authorize(string encodedScope)
        {
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
