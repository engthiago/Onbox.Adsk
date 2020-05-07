using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement.Core
{
    public partial class BIM360ProjectUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AutodeskId { get; set; }
        public string AnaylticsId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public long PostalCode { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public Phone Phone { get; set; }
        public string JobTitle { get; set; }
        public string Industry { get; set; }
        public string AboutMe { get; set; }
        public AccessLevels AccessLevels { get; set; }
        public string CompanyId { get; set; }
        public List<string> RoleIds { get; set; }
        public List<Service> Services { get; set; }
    }

    public partial class AccessLevels
    {
        public bool AccountAdmin { get; set; }
        public bool ProjectAdmin { get; set; }
        public bool Executive { get; set; }
    }

    public partial class Phone
    {
        public string Number { get; set; }
        public string PhoneType { get; set; }
        public long Extension { get; set; }
    }

    public partial class Service
    {
        public string ServiceName { get; set; }
        public string Access { get; set; }
    }

}
