using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement.Core
{
    public class AdskUserProfile
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("emailVerified")]
        public bool EmailVerified { get; set; }

        [JsonProperty("2FaEnabled")]
        public bool The2FaEnabled { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("optin")]
        public bool Optin { get; set; }

        [JsonProperty("lastModified")]
        public DateTimeOffset LastModified { get; set; }

        [JsonProperty("profileImages")]
        public AdskProfileImages ProfileImages { get; set; }

        [JsonProperty("websiteUrl")]
        public string WebsiteUrl { get; set; }
    }

    public class AdskProfileImages
    {
        [JsonProperty("sizeX20")]
        public string SizeX20 { get; set; }

        [JsonProperty("sizeX40")]
        public string SizeX40 { get; set; }

        [JsonProperty("sizeX50")]
        public string SizeX50 { get; set; }

        [JsonProperty("sizeX58")]
        public string SizeX58 { get; set; }

        [JsonProperty("sizeX80")]
        public string SizeX80 { get; set; }

        [JsonProperty("sizeX120")]
        public string SizeX120 { get; set; }

        [JsonProperty("sizeX160")]
        public string SizeX160 { get; set; }

        [JsonProperty("sizeX176")]
        public string SizeX176 { get; set; }

        [JsonProperty("sizeX240")]
        public string SizeX240 { get; set; }

        [JsonProperty("sizeX360")]
        public string SizeX360 { get; set; }
    }
}
