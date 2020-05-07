using System;
using System.Collections.Generic;
using System.Text;

namespace Onbox.Adsk.DataManagement
{
    public abstract class AdskServiceBase
    {
        protected static string forgeBaseUrl = "https://developer.api.autodesk.com/";

        public static void OverrideForgeEndpoint(string endpoint)
        {
            forgeBaseUrl = endpoint;
        }
    }
}
