using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement
{
    public class AdskPermissionRequirementsBuilder
    {
        List<string> requirements;
        List<KeyValuePair<string, string>> items;

        public AdskPermissionRequirementsBuilder()
        {
        }

        public AdskPermissionRequirementsBuilder CheckDownloadPermission()
        {
            AddRequirement("download");
            return this;
        }

        public AdskPermissionRequirementsBuilder CheckViewPermission()
        {
            AddRequirement("view");
            return this;
        }

        public AdskPermissionRequirementsBuilder CheckWritePermissions()
        {
            AddRequirement("write");
            return this;
        }

        public AdskPermissionRequirementsBuilder AddItem(string id, string type)
        {
            if (this.items == null)
            {
                this.items = new List<KeyValuePair<string, string>>();
            }

            this.items.Add(new KeyValuePair<string, string>(id, type));

            return this;
        }

        public  AdskPermissionRequirementsBuilder Initialize()
        {
            this.requirements = new List<string>();
            this.items = new List<KeyValuePair<string, string>>();
            return this;
        }

        private void AddRequirement(string requirement)
        {
            if (this.requirements == null)
            {
                this.requirements = new List<string>();
            }

            if (!requirements.Contains(requirement))
            {
                requirements.Add(requirement);
            }
        }


        internal List<string> GetRequirements()
        {
            return this.requirements;
        }

        internal List<KeyValuePair<string, string>> GetItems()
        {
            return this.items;
        }
    }
}
