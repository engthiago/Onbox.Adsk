using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement.Core
{
    public partial class Bim360PatchUserResult
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("industry_roles")]
        public List<string> IndustryRoles { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
