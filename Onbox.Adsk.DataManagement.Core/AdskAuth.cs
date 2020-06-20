using Newtonsoft.Json;
using System;
namespace Onbox.Adsk.DataManagement.Core
{
    public class AdskAuth
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        public DateTime Expiration { get; set; }
    }
}