using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement.Core.Requests
{
    public class BIM360PatchProjectUserPatchReq
    {
        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("industry_roles")]
        public List<string> IndustryRoles { get; set; }
    }
}
