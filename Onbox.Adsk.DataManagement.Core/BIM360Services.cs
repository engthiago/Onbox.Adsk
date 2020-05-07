using Newtonsoft.Json;

namespace Onbox.Adsk.DataManagement.Core
{
    public partial class BIM360Services
    {
        [JsonProperty("document_management")]
        public BIM360AccessLevel DocumentManagement { get; set; }

        [JsonProperty("project_administration")]
        public BIM360AccessLevel ProjectAdministration  { get; set; }
    }
}
