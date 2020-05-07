using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement.Core
{
    public partial class BIM360ImportUsersResult
    {
        [JsonProperty("success")]
        public long Success { get; set; }

        [JsonProperty("failure")]
        public long Failure { get; set; }

        [JsonProperty("success_items")]
        public List<SuccessItem> SuccessItems { get; set; }

        [JsonProperty("failure_items")]
        public List<FailureItem> FailureItems { get; set; }
    }

    public partial class FailureItem
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("services")]
        public FailureItemServices Services { get; set; }

        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("industry_roles")]
        public List<string> IndustryRoles { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public partial class FailureItemServices
    {
        [JsonProperty("project_administration")]
        public DocumentManagement ProjectAdministration { get; set; }

        [JsonProperty("document_management")]
        public DocumentManagement DocumentManagement { get; set; }
    }

    public partial class DocumentManagement
    {
        [JsonProperty("access_level")]
        public string AccessLevel { get; set; }
    }

    public partial class SuccessItem
    {
        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("services")]
        public SuccessItemServices Services { get; set; }

        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("industry_roles")]
        public List<string> IndustryRoles { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }

    public partial class SuccessItemServices
    {
        [JsonProperty("document_management")]
        public DocumentManagement DocumentManagement { get; set; }
    }
}
