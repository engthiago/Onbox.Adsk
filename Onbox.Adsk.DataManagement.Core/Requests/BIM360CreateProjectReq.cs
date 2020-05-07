using Newtonsoft.Json;
using System;

namespace Onbox.Adsk.DataManagement.Core.Requests
{
    public class BIM360CreateProjectReq
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("service_types")]
        public string ServiceTypes { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("project_type")]
        public string ProjectType { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("job_number")]
        public string JobNumber { get; set; }

        [JsonProperty("address_line_1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("address_line_2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state_or_province")]
        public string StateOrProvince { get; set; }

        [JsonProperty("postal_code")]
        public long PostalCode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("business_unit_id")]
        public string BusinessUnitId { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("construction_type")]
        public string ConstructionType { get; set; }

        [JsonProperty("contract_type")]
        public string ContractType { get; set; }

        [JsonProperty("template_project_id")]
        public string TemplateProjectId { get; set; }

        [JsonProperty("include_companies")]
        public bool IncludeCompanies { get; set; }

        [JsonProperty("include_locations")]
        public bool IncludeLocations { get; set; }
    }
}
