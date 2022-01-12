using Onbox.Abstractions.V9;
using Onbox.Adsk.DataManagement.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class AdskPermissionService
    {
        private readonly IHttpService httpService;
        private readonly IAdskForgeConfigService forgeConfig;

        public AdskPermissionService(IHttpService httpService, IAdskForgeConfigService forgeConfig)
        {
            this.httpService = httpService;
            this.forgeConfig = forgeConfig;
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

            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"data/v1/projects/{projectId}/commands";

            return await this.httpService.PostAsync<BIM360PermissionCommandResults>(endpoint, commandReq, token);
        }

        public async Task<BIM360FolderPermissions> GetBIM360FolderPermissionsAsync(string projectId, string folderId, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"bim360/docs/v1/projects/{projectId}/folders/{folderId}/permissions";

            return await this.httpService.PostAsync<BIM360FolderPermissions>(endpoint, token);
        }
    }
}
