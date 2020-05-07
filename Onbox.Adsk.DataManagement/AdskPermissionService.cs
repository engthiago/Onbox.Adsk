using Onbox.Adsk.DataManagement.Core;
using Onbox.Standard.Core.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class AdskPermissionService : AdskServiceBase
    {
        private readonly IHttpService httpService;

        public AdskPermissionService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<BIM360PermissionCommandResults> CheckPermissionsCommandAsync(AdskPermissionRequirementsBuilder permissionBuilder, string projectId, string token)
        {
            var requirements = permissionBuilder.GetRequirements();
            var items = permissionBuilder.GetItems();

            var commandReq = new
            {
                jsonapi = new { version = "1.0" },
                data = new
                {
                    type = "commands",
                    attributes = new
                    {
                        extension = new
                        {
                            type = "commands:autodesk.core:CheckPermission",
                            version = "version",
                            data = new
                            {
                                requiredActions = requirements
                            }
                        }
                    },
                    relationships = new
                    {
                        resources = new
                        {
                            data = items
                        }
                    }
                }
            };

            string endpoint = forgeBaseUrl
                + $"data/v1/projects/{projectId}/commands";

            return await this.httpService.PostAsync<BIM360PermissionCommandResults>(endpoint, commandReq, token);
        }

        public async Task<BIM360FolderPermissions> GetBIM360FolderPermissionsAsync(string projectId, string folderId, string token)
        {
            string endpoint = forgeBaseUrl
                + $"bim360/docs/v1/projects/{projectId}/folders/{folderId}/permissions";

            return await this.httpService.PostAsync<BIM360FolderPermissions>(endpoint, token);
        }
    }
}
