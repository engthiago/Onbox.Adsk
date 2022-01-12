using Onbox.Abstractions.V9;
using Onbox.Adsk.DataManagement.Core;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class AdskDataService
    {
        private readonly IHttpService httpService;
        private readonly IAdskForgeConfigService forgeConfig;

        public AdskDataService(IHttpService httpService, IAdskForgeConfigService forgeConfig)
        {
            this.httpService = httpService;
            this.forgeConfig = forgeConfig;
        }

        public async Task<Hubs> GetHubsAsync(string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + "project/v1/hubs";

            var hubs = await this.httpService.GetAsync<Hubs>(endpoint, token);
            return hubs;
        }

        public async Task<Projects> GetProjectsAsync(string hubId, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"project/v1/hubs/{hubId}/projects";

            var projects = await this.httpService.GetAsync<Projects>(endpoint, token);
            return projects;
        }

        public async Task<Projects> GetActiveProjectsAsync(string hubId, int page, int pageSize, string token)
        {
            page = page >= 0 ? page : 1;
            pageSize = pageSize > 0 ? pageSize : 1;

            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"project/v1/hubs/{hubId}/projects?page[number]={page}&page[limit]={pageSize}";

            var projects = await this.httpService.GetAsync<Projects>(endpoint, token);
            return projects;
        }

        public async Task<Project> SearchAtiveProjectAsync(string hubId, string projectName, string token)
        {
            int page = 0;
            int maxCalls = 10;
            int currentCall = 0;
            while (true)
            {
                currentCall++;

                var projects = await GetActiveProjectsAsync(hubId, page, 100, token);

                if (projects.Data.Count == 0)
                {
                    return null;
                }

                var projectData = projects.Data.FirstOrDefault(p => p.Attributes.Name == projectName);

                if (projectData != null)
                {
                    return new Project
                    {
                        Jsonapi = new Jsonapi {  Version = "1.0"},
                        Data = projectData
                    };
                }

                if (currentCall >= maxCalls)
                {
                    return null;
                }

                page++;
            }
        }

        public async Task<TopFolders> GetTopFoldersAsync(string hubId, string projectId, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"project/v1/hubs/{hubId}/projects/{projectId}/topFolders";

            var folders = await this.httpService.GetAsync<TopFolders>(endpoint, token);
            return folders;
        }

        public async Task<SearchResults> SearchItemAsync(string projectId, string folderId, string partialName, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"data/v1/projects/{projectId}/folders/{folderId}/search?filter[attributes.name]-contains={partialName}";

            var itemSearch = await this.httpService.GetAsync<SearchResults>(endpoint, token);
            return itemSearch;
        }

        public async Task<SearchResults> SearchItemByC4RGuidsAsync(string projectId, string folderId, Guid modelGuid, Guid projectGuid, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"data/v1/projects/{projectId}/folders/{folderId}/search?"
                + $"filter[attributes.extension.data.projectGuid]={projectGuid.ToString()}"
                + $"&"
                + $"filter[attributes.extension.data.modelGuid]={modelGuid.ToString()}";

            var itemSearch = await this.httpService.GetAsync<SearchResults>(endpoint, token);
            return itemSearch;
        }

        public async Task<Item> GetForgeItemAsync(string projectId, string itemId, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"data/v1/projects/{projectId}/items/{itemId}?includePathInProject=true";

            var item = await this.httpService.GetAsync<Item>(endpoint, token);
            return item;
        }

        public async Task<Folder> GetFolderAsync(string projectId, string folderId, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"data/v1/projects/{projectId}/folders/{folderId}";

            var folder = await this.httpService.GetAsync<Folder>(endpoint, token);
            return folder;
        }

        public async Task<FolderContents> GetFolderContentsAsync(string projectId, string folderId, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"data/v1/projects/{projectId}/folders/{folderId}/contents";

            var forgeItems = await this.httpService.GetAsync<FolderContents>(endpoint, token);
            return forgeItems;
        }

        public async Task<Storage> PrepareStorageObjectAsync(string projectId, string fileName, string folderId, string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"data/v1/projects/{projectId}/storage";

            var req = new
            {
                jsonapi = new
                {
                    version = "1.0"
                },
                data = new
                {
                    type = "objects",
                    attributes = new
                    {
                        name = fileName,
                    },
                    relationships = new
                    {
                        target = new
                        {
                            data = new
                            {
                                type = "folders",
                                id = folderId
                            }
                        }
                    }
                }
            };

            var forgeStorageResult = await this.httpService.PostAsync<Storage>(endpoint, req, token);
            return forgeStorageResult;
        }

        public async Task<ObjectReference> UploadFileAsync(Storage storage, Stream payload, string token)
        {
            var fileRef = storage.Data.Id.Remove(0, "urn:adsk.objects:os.object:wip.dm.prod/".Length);

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"oss/v2/buckets/wip.dm.prod/objects/{fileRef}";

            var forgeItems = await this.httpService.PutStreamAsync<ObjectReference>(endpoint, payload, token);
            return forgeItems;
        }


    }
}
