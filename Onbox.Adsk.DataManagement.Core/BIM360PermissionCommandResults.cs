using System.Collections.Generic;

namespace Onbox.Adsk.DataManagement.Core
{
    public class BIM360PermissionCommandResults
    {
        public Jsonapi Jsonapi { get; set; }
        public PermissionResultsData Data { get; set; }
    }

    public partial class PermissionResultsData
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public PermissionsResultsAttributes Attributes { get; set; }
        public PermissionsRelationships Relationships { get; set; }
    }

    public partial class PermissionsResultsAttributes
    {
        public Extension Extension { get; set; }
    }

    public partial class Extension
    {
        public ExtensionData Data { get; set; }
        public string Version { get; set; }
        public string Type { get; set; }
        public Schema Schema { get; set; }
    }

    public partial class ExtensionData
    {
        public List<Permissions> Permissions { get; set; }
        public List<string> RequiredActions { get; set; }
    }

    public partial class Permissions
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public Dictionary<string, bool> Details { get; set; }
        public bool Permission { get; set; }
    }

    public partial class PermissionsRelationships
    {
        public PermissionResources Resources { get; set; }
    }

    public partial class PermissionResources
    {
        public List<PermissionData> Data { get; set; }
    }

    public partial class PermissionData
    {
        public string Type { get; set; }
        public string Id { get; set; }
    }


}
