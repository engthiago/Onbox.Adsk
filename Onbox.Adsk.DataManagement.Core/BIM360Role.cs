using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement.Core
{
    public partial class Bim360Role
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("services")]
        public BIM360Services Services { get; set; }

        [JsonProperty("member_group_id")]
        public long MemberGroupId { get; set; }
    }
}
