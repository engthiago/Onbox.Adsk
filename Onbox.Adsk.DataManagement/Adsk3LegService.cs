using Onbox.Adsk.DataManagement.Core;
using Onbox.Standard.Core.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class Adsk3LegService
    {
        private readonly IHttpService httpService;
        private readonly IAdskForgeConfigService forgeConfig;

        public Adsk3LegService(IHttpService httpService, IAdskForgeConfigService forgeConfig)
        {
            this.httpService = httpService;
            this.forgeConfig = forgeConfig;
        }

        /// <summary>
        /// Gets the URL so users can log in to Adsk
        /// </summary>
        public AdskOathUrl GetOathUrl(string scope = "data:search data:read bucket:read bucket:create data:write bucket:delete account:read account:write")
        {
            var baseUrl = this.forgeConfig.GetBaseUrl();
            var redirect = this.forgeConfig.GetCallbackUrl();
            var grant = "code";
            var clientId = this.forgeConfig.GetClientId();
            var encodedScope = WebUtility.UrlEncode(scope);

            var fullUrl = $"{baseUrl}authentication/v1/authorize?redirect_uri={redirect}&response_type={grant}&client_id={clientId}&scope={encodedScope}";

            return new AdskOathUrl
            {
                FullUrl = fullUrl
            };
        }

        /// <summary>
        /// Performs the 3 Leg Authentication using the resulting code of user authentication
        /// </summary>
        public async Task<AdskAuth> Get3LegToken(string code)
        {
            var form = new Dictionary<string, string>();
            form.Add("client_id", this.forgeConfig.GetClientId());
            form.Add("client_secret", this.forgeConfig.GetClientSecret());
            form.Add("grant_type", "authorization_code");
            form.Add("code", code);
            form.Add("redirect_uri", this.forgeConfig.GetCallbackUrl());

            string endpoint = this.forgeConfig.GetBaseUrl()
                                + $"authentication/v1/gettoken";

            var auth = await this.httpService.PostFormAsync<AdskAuth>(endpoint, form);
            auth.Expiration = DateTime.UtcNow.AddSeconds(auth.ExpiresIn);
            return auth;
        }

        /// <summary>
        /// Refreshes 3 Leg Authentication with Adsk
        /// </summary>
        public async Task<AdskAuth> RefreshToken(string refreshToken)
        {
            var form = new Dictionary<string, string>();
            form.Add("client_id", this.forgeConfig.GetClientId());
            form.Add("client_secret", this.forgeConfig.GetClientSecret());
            form.Add("grant_type", "refresh_token");
            form.Add("refresh_token", refreshToken);

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"authentication/v1/refreshtoken";

            var auth = await this.httpService.PostFormAsync<AdskAuth>(endpoint, form);
            auth.Expiration = DateTime.UtcNow.AddSeconds(auth.ExpiresIn);
            return auth;
        }

    }
}
