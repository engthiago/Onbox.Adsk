using Onbox.Adsk.DataManagement.Core;
using Onbox.Standard.Core.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class BIM360RoleService
    {
        private readonly IHttpService httpService;
        private readonly AdskIdConversionService idConversionService;
        private readonly BIM360RegionsService regionsService;
        private readonly IAdskForgeConfigService forgeConfig;

        public BIM360RoleService(IHttpService httpService, AdskIdConversionService idConversionService, BIM360RegionsService regionsService, IAdskForgeConfigService forgeConfig)
        {
            this.httpService = httpService;
            this.idConversionService = idConversionService;
            this.regionsService = regionsService;
            this.forgeConfig = forgeConfig;
        }

        public async Task<List<Bim360Role>> GetProjectRoles(string hubId, string projectId, string region, string token)
        {
            var accountId = this.idConversionService.RemoveBIMPrefix(hubId);
            projectId = this.idConversionService.RemoveBIMPrefix(projectId);

            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"hq/v2/{regionUrl}/accounts/{accountId}/projects/{projectId}/industry_roles";

            var project = await this.httpService.GetAsync<List<Bim360Role>>(endpoint, token);
            return project;
        }
    }
}
