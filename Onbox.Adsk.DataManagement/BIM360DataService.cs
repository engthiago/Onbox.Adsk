using Onbox.Adsk.DataManagement.Core;
using Onbox.Adsk.DataManagement.Core.Requests;
using Onbox.Standard.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class BIM360DataService : AdskServiceBase
    {
        private readonly IHttpService httpService;
        private readonly AdskIdConversionService idConversionService;
        private readonly BIM360RegionsService regionsService;

        public BIM360DataService(IHttpService httpService, AdskIdConversionService idConversionService, BIM360RegionsService regionsService)
        {
            this.httpService = httpService;
            this.idConversionService = idConversionService;
            this.regionsService = regionsService;
        }

        public async Task<Hubs> GetHubsAsync(string token)
        {
            string endpoint = forgeBaseUrl
                        + "project/v1/hubs?filter[extension.type]=hubs:autodesk.bim360:Account";

            var hubs = await this.httpService.GetAsync<Hubs>(endpoint, token);
            return hubs;
        }

        public async Task<BIM360Project> GetProjectAsync(string hubId, string projectId, string region, string token)
        {
            var accountId = this.idConversionService.RemoveBIMPrefix(hubId);
            projectId = this.idConversionService.RemoveBIMPrefix(projectId);

            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = forgeBaseUrl
                    + $"hq/v1/{regionUrl}/accounts/{accountId}/projects/{projectId}";

            var project = await this.httpService.GetAsync<BIM360Project>(endpoint, token);
            return project;
        }

        public async Task<BIM360Project> PostProjectAsync(string hubId, BIM360CreateProjectReq request, string region, string token)
        {
            var accountId = this.idConversionService.RemoveBIMPrefix(hubId);

            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = forgeBaseUrl
                + $"hq/v1/{regionUrl}/accounts/{accountId}/projects";

            var projects = await this.httpService.PostAsync<BIM360Project>(endpoint, request, token);
            return projects;
        }

        public async Task<List<BIM360Project>> GetProjectsAsync(string hubId, string region, string token, int offset = 0, int limit = 100, string sortField = "-start_date")
        {
            offset = offset > 0 ? offset : 0;
            limit = limit > 0 ? limit : 0;
            limit = limit <= 100 ? limit : 100;

            var accountId = this.idConversionService.RemoveBIMPrefix(hubId);
            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = forgeBaseUrl
                + $"hq/v1/{regionUrl}/accounts/{accountId}/projects?limit={limit}&offset={offset}&sort={sortField}";

            var projects = await this.httpService.GetAsync<List<BIM360Project>>(endpoint, token);
            return projects;
        }

        public async Task<BIM360Project> SearchProjectsAsync(string hubId, string region, string token, string projectName)
        {
            int offset = 0;
            int maxCalls = 10;
            int currentCall = 0;
            int maxProjectQueryCount = 100;
            while (true)
            {
                currentCall++;

                var projects = await GetProjectsAsync(hubId, region, token, offset, maxProjectQueryCount);

                if (projects.Count == 0)
                {
                    return null;
                }

                var project = projects.FirstOrDefault(p => p.Name == projectName);
                if (project != null)
                {
                    return project;
                }

                if (currentCall >= maxCalls)
                {
                    return null;
                }

                offset += 100;
            }
        }

        public async Task<TopFolders> GetTopFoldersAsync(string hubId, string projectId, string token)
        {
            var accountId = this.idConversionService.AddBIMPrefix(hubId);
            projectId = this.idConversionService.AddBIMPrefix(projectId);

            string endpoint = forgeBaseUrl
                + $"project/v1/hubs/{accountId}/projects/{projectId}/topFolders";

            var folders = await this.httpService.GetAsync<TopFolders>(endpoint, token);
            return folders;
        }

        public async Task<Folder> CreateFolderAsync(string projectId, string parentFolderId, string token)
        {

            string endpoint = forgeBaseUrl
                + $"data/v1/projects/{projectId}/folders";

            object req = new
            {
                jsonapi = new
                {
                    version = "1.0"
                },
                data = new
                {
                    type = "folders",
                    attributes = new
                    {
                        name = "FabCenter",
                        extension = new
                        {
                            type = "folders:autodesk.bim360:Folder",
                            version = "1.0"
                        }
                    },
                    relationships = new
                    {
                        parent = new
                        {
                            data = new
                            {
                                type = "folders",
                                id = parentFolderId
                            }
                        }
                    }
                }
            };

            var folder = await this.httpService.PostAsync<Folder>(endpoint, req, token);
            return folder;
        }

        public async Task<Item> CreateFirstItemVersionAsync(string projectId, string fileName, string folderId, Storage storage, string token)
        {
            object createVersionReq = new
            {
                jsonapi = new { version = "1.0" },
                data = new
                {
                    type = "items",
                    attributes = new
                    {
                        displayName = fileName,
                        extension = new
                        {
                            type = "items:autodesk.bim360:File",
                            version = "1.0"
                        }
                    },
                    relationships = new
                    {
                        tip = new
                        {
                            data = new
                            {
                                type = "versions",
                                id = "1"
                            }
                        },
                        parent = new
                        {
                            data = new
                            {
                                type = "folders",
                                id = folderId
                            }
                        }
                    }
                },
                included = new List<object>
                {
                    new
                    {
                        type = "versions",
                        id = "1",
                        attributes = new
                        {
                            name = fileName,
                            extension = new
                            {
                                type = "versions:autodesk.bim360:File",
                                version = "1.0"
                            }
                        },
                        relationships = new
                        {
                            storage = new
                            {
                                data = new
                                {
                                    type = "objects",
                                    id = storage.Data.Id
                                }
                            }
                        }
                    }
                }
            };

            string endpoint = forgeBaseUrl
                    + $"data/v1/projects/{projectId}/items";

            var versionPayloadResult = await this.httpService.PostAsync<Item>(endpoint, createVersionReq, token);
            return versionPayloadResult;
        }

        public async Task<Item> UpdateItemVersionAsync(string projectId, string fileName, string existingItemId, Storage storage, string region, string token)
        {
            object updateVersionReq = new
            {
                jsonapi = new { version = "1.0" },
                data = new
                {
                    type = "versions",
                    attributes = new
                    {
                        name = fileName,
                        extension = new
                        {
                            type = "versions:autodesk.bim360:File",
                            version = "1.0"
                        }
                    },
                    relationships = new
                    {
                        item = new
                        {
                            data = new
                            {
                                type = "items",
                                id = existingItemId
                            }
                        },
                        storage = new
                        {
                            data = new
                            {
                                type = "objects",
                                id = storage.Data.Id
                            }
                        }
                    }
                }
            };

            string endpoint = forgeBaseUrl
                    + $"data/v1/projects/{projectId}/versions";

            var versionPayloadResult = await this.httpService.PostAsync<Item>(endpoint, updateVersionReq, token);
            return versionPayloadResult;
        }

        public string GetBIM360ItemUrl(string projectId, string folderId, string itemId)
        {
            var url = $"https://docs.b360.autodesk.com/projects/{projectId}/folders/{folderId}/thumbnail/viewer/items/{itemId}";
            return url;
        }
    }
}
