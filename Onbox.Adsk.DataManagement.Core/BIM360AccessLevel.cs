using Newtonsoft.Json;

namespace Onbox.Adsk.DataManagement.Core
{
    public partial class BIM360AccessLevel
    {
        [JsonProperty("access_level")]
        public string AccessLevel { get; set; }
    }
}
