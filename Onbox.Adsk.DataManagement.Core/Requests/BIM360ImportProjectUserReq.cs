using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement.Core.Requests
{
    public partial class BIM360ImportProjectUserReq
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("services")]
        public BIM360Services Services { get; set; }

        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("industry_roles")]
        public List<string> IndustryRoles { get; set; } = new List<string>();
    }
}
