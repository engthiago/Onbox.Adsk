using Onbox.Adsk.DataManagement.Core;
using Onbox.Adsk.DataManagement.Core.Requests;
using Onbox.Standard.Core.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class Bim360UserService
    {
        private readonly IHttpService httpService;
        private readonly AdskIdConversionService idConversionService;
        private readonly BIM360RegionsService regionsService;
        private readonly IAdskForgeConfigService forgeConfig;

        public Bim360UserService(IHttpService httpService, AdskIdConversionService idConversionService, BIM360RegionsService regionsService, IAdskForgeConfigService forgeConfig)
        {
            this.httpService = httpService;
            this.idConversionService = idConversionService;
            this.regionsService = regionsService;
            this.forgeConfig = forgeConfig;
        }

        public async Task<Paging<BIM360ProjectUser>> GetProjectUsersAsync(string projectId, string token)
        {
            projectId = this.idConversionService.RemoveBIMPrefix(projectId);
            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"bim360/admin/v1/projects/{projectId}/users";

            return await this.httpService
                .GetAsync<Paging<BIM360ProjectUser>>(endpoint, token);
        }

        public async Task<BIM360ProjectUser> GetProjectUserAsync(string projectId, string userId, string token)
        {
            projectId = this.idConversionService.RemoveBIMPrefix(projectId);

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"bim360/admin/v1/projects/{projectId}/users/{userId}";

            return await this.httpService
                .GetAsync<BIM360ProjectUser>(endpoint, token);
        }

        public async Task<BIM360AccountUser> SearchAccountUser(string userEmail, string accountId, string region, string token)
        {
            accountId = this.idConversionService.RemoveBIMPrefix(accountId);

            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"hq/v1/{regionUrl}/accounts/{accountId}/users/search?email={userEmail}&limit=1";

            var users = await this.httpService
                .GetAsync<List<BIM360AccountUser>>(endpoint, token);
            return users.FirstOrDefault();
        }

        public async Task<List<BIM360AccountUser>> SearchAccountUsers(string userEmail, string accountId, int limit, string region, string token)
        {
            accountId = this.idConversionService.RemoveBIMPrefix(accountId);
            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"hq/v1/{regionUrl}/accounts/{accountId}/users/search?email={userEmail}&limit={limit}";

            return await this.httpService
                .GetAsync<List<BIM360AccountUser>>(endpoint, token);
        }

        public async Task<BIM360ProjectUser> AddAdminToProjectAsync(BIM360AccountUser accountUser, string accountId, string projectId, string region, string token)
        {
            accountId = this.idConversionService.RemoveBIMPrefix(accountId);
            projectId = this.idConversionService.RemoveBIMPrefix(projectId);
            var regionUrl = regionsService.GetRegionsUrl(region);

            var addAdminReq = new
            {
                role = "project_admin",
                service_type = "doc_manager",
                company_id = accountUser.CompanyId,
                company_name = accountUser.CompanyName,
                email = accountUser.Email,
                name = accountUser.Name,
                nickname = accountUser.Nickname,
                first_name = accountUser.FirstName,
                last_name = accountUser.LastName,
                uid = accountUser.Uid,
                image_url = accountUser.ImageUrl,
                address_line_1 = accountUser.AddressLine1,
                address_line_2 = accountUser.AddressLine2,
                city = accountUser.City,
                state_or_province = accountUser.StateOrProvince,
                postal_code = accountUser.PostalCode,
                country = accountUser.Country,
                phone = accountUser.Phone,
                company = accountUser.Company,
                job_title = accountUser.JobTitle,
                industry = accountUser.Industry,
                about_me = accountUser.AboutMe
            };

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"hq/v1/{regionUrl}/accounts/{accountId}/projects/{projectId}/users";

            return await this.httpService
                .PostAsync<BIM360ProjectUser>(endpoint, addAdminReq, token);
        }

        public async Task<Bim360PatchUserResult> PatchProjectUser(BIM360PatchProjectUserPatchReq user, string adminId, string userId, string accountId, string projectId, string region, string token)
        {
            accountId = this.idConversionService.RemoveBIMPrefix(accountId);
            projectId = this.idConversionService.RemoveBIMPrefix(projectId);
            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"hq/v2/{regionUrl}/accounts/{accountId}/projects/{projectId}/users/{userId}";


            return await this.httpService
                .AddHeader("x-user-id", adminId)
                .PatchAsync<Bim360PatchUserResult>(endpoint, user, token);
        }

        public async Task<BIM360ImportUsersResult> ImportUsersToProjecAsync(List<BIM360ImportProjectUserReq> users, string adminId, string accountId, string projectId, string region, string token)
        {
            accountId = this.idConversionService.RemoveBIMPrefix(accountId);
            projectId = this.idConversionService.RemoveBIMPrefix(projectId);
            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"hq/v2/{regionUrl}/accounts/{accountId}/projects/{projectId}/users/import";

            return await this.httpService
                .AddHeader("x-user-id", adminId)
                .PostAsync<BIM360ImportUsersResult>(endpoint, users, token);
        }
    }
}
