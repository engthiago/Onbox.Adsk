using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement
{
    public class AdskIdConversionService
    {
        public AdskIdConversionService()
        {
        }

        public string AddBIMPrefix(string hubIdOrProjectId)
        {
            var normalized = hubIdOrProjectId;
            if (!hubIdOrProjectId.StartsWith("b."))
            {
                normalized = $"b.{hubIdOrProjectId}";
            }

            return normalized;
        }        
        
        public string RemoveBIMPrefix(string hubIdOrProjectId)
        {
            var normalized = hubIdOrProjectId;
            if (hubIdOrProjectId.StartsWith("b."))
            {
                normalized = hubIdOrProjectId.Remove(0, "b.".Length);
            }

            return normalized;
        }
    }
}
