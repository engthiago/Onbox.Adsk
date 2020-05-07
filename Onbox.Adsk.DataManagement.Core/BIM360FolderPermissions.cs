using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement.Core
{
    public partial class BIM360FolderPermissions
    {
        public string SubjectId { get; set; }
        public string AutodeskId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string SubjectType { get; set; }
        public string SubjectStatus { get; set; }
        public List<string> Actions { get; set; }
        public List<string> InheritActions { get; set; }
    }
}
