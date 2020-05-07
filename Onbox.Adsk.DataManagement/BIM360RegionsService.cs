using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement
{
    public class BIM360RegionsService
    {
        public string GetRegionsUrl(string region)
        {
            if (region != "US")
            {
                return "regions/eu";
            }
            else
            {
                return "";
            }
        }
    }
}
