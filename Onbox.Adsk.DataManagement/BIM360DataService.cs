using Onbox.Abstractions.V9;
using Onbox.Adsk.DataManagement.Core;
using Onbox.Adsk.DataManagement.Core.Requests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onbox.Adsk.DataManagement
{
    public class BIM360DataService
    {
        private readonly IHttpService httpService;
        private readonly AdskIdConversionService idConversionService;
        private readonly BIM360RegionsService regionsService;
        private readonly IAdskForgeConfigService forgeConfig;

        public BIM360DataService(IHttpService httpService, AdskIdConversionService idConversionService, BIM360RegionsService regionsService, IAdskForgeConfigService forgeConfig)
        {
            this.httpService = httpService;
            this.idConversionService = idConversionService;
            this.regionsService = regionsService;
            this.forgeConfig = forgeConfig;
        }

        public async Task<Hubs> GetHubsAsync(string token)
        {
            string endpoint = this.forgeConfig.GetBaseUrl()
                        + "project/v1/hubs?filter[extension.type]=hubs:autodesk.bim360:Account";

            var hubs = await this.httpService.GetAsync<Hubs>(endpoint, token);
            return hubs;
        }

        public async Task<BIM360Project> GetProjectAsync(string accountId, string projectId, string region, string token)
        {
            accountId = this.idConversionService.RemoveBIMPrefix(accountId);
            projectId = this.idConversionService.RemoveBIMPrefix(projectId);

            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"hq/v1/{regionUrl}/accounts/{accountId}/projects/{projectId}";

            var project = await this.httpService.GetAsync<BIM360Project>(endpoint, token);
            return project;
        }

        public async Task<BIM360Project> PostProjectAsync(string accountId, BIM360CreateProjectReq request, string region, string token)
        {
            accountId = this.idConversionService.RemoveBIMPrefix(accountId);

            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"hq/v1/{regionUrl}/accounts/{accountId}/projects";

            var projects = await this.httpService.PostAsync<BIM360Project>(endpoint, request, token);
            return projects;
        }

        public async Task<List<BIM360Project>> GetProjectsAsync(string accountId, string region, string token, int offset = 0, int limit = 30, string sortField = "-start_date")
        {
            offset = offset > 0 ? offset : 0;
            limit = limit > 0 ? limit : 0;
            limit = limit <= 100 ? limit : 100;

            accountId = this.idConversionService.RemoveBIMPrefix(accountId);
            var regionUrl = regionsService.GetRegionsUrl(region);

            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"hq/v1/{regionUrl}accounts/{accountId}/projects?limit={limit}&offset={offset}&sort={sortField}";

            var projects = await this.httpService.GetAsync<List<BIM360Project>>(endpoint, token);
            return projects;
        }

        public async Task<BIM360Project> SearchProjectsAsync(string accountId, string region, string projectName, string token)
        {
            int offset = 0;
            int maxCalls = 20;
            int currentCall = 0;
            int limit = 30;

            projectName = projectName.ToLower();

            while (true)
            {
                currentCall++;

                var projects = await GetProjectsAsync(accountId, region, token, offset, limit);

                if (projects.Count == 0)
                {
                    return null;
                }

                var project = projects.FirstOrDefault(p => p.Name.ToLower() == projectName);
                if (project != null)
                {
                    return project;
                }

                if (currentCall >= maxCalls)
                {
                    return null;
                }

                offset += limit;
            }
        }

        public async Task<TopFolders> GetTopFoldersAsync(string accountId, string projectId, string token)
        {
            accountId = this.idConversionService.AddBIMPrefix(accountId);
            projectId = this.idConversionService.AddBIMPrefix(projectId);

            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"project/v1/hubs/{accountId}/projects/{projectId}/topFolders";

            var folders = await this.httpService.GetAsync<TopFolders>(endpoint, token);
            return folders;
        }

        public async Task<FolderContents> GetFolderContentsAsync(string projectId, string folderId, string token)
        {
            projectId = this.idConversionService.AddBIMPrefix(projectId);

            string endpoint = this.forgeConfig.GetBaseUrl()
                + $"data/v1/projects/{projectId}/folders/{folderId}/contents";

            var forgeItems = await this.httpService.GetAsync<FolderContents>(endpoint, token);
            return forgeItems;
        }

        public async Task<Folder> CreateFolderAsync(string folderName, string projectId, string parentFolderId, string token)
        {
            projectId = this.idConversionService.AddBIMPrefix(projectId);

            string endpoint = this.forgeConfig.GetBaseUrl()
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
                        name = folderName,
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

        public async Task<Storage> PrepareStorageObjectAsync(string projectId, string fileName, string folderId, string token)
        {
            projectId = this.idConversionService.AddBIMPrefix(projectId);

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

        public async Task<Item> CreateFirstItemVersionAsync(string projectId, string fileName, string folderId, Storage storage, string token)
        {
            projectId = this.idConversionService.AddBIMPrefix(projectId);

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

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"data/v1/projects/{projectId}/items";

            var versionPayloadResult = await this.httpService.PostAsync<Item>(endpoint, createVersionReq, token);
            return versionPayloadResult;
        }

        public async Task<Item> UpdateItemVersionAsync(string projectId, string fileName, string existingItemId, Storage storage, string region, string token)
        {
            projectId = this.idConversionService.AddBIMPrefix(projectId);

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

            string endpoint = this.forgeConfig.GetBaseUrl()
                    + $"data/v1/projects/{projectId}/versions";

            var versionPayloadResult = await this.httpService.PostAsync<Item>(endpoint, updateVersionReq, token);
            return versionPayloadResult;
        }

        public async Task<FolderData> GetPlansFolderAsync(string accountId, string projectId, string token)
        {
            var topFolders = await this.GetTopFoldersAsync(accountId, projectId, token);

            return GetPlansFolder(topFolders);
        }

        public FolderData GetPlansFolder(TopFolders topFolders)
        {
            var plansFolder = topFolders.Data.FirstOrDefault(f => f.Attributes.Name == "Plans");

            // If plans folder not there, maybe it has been renamed?
            // Looks like different languages do not affect the API, only the UI itself, because it is a browser based translation as per link: (Tested and worked)
            // https://knowledge.autodesk.com/support/bim-360/learn-explore/caas/CloudHelp/cloudhelp/ENU/BIM-360-Docs/files/GUID-0F437870-94AE-46AC-BDFE-F3994B3A98D0-htm.html
            if (plansFolder == null)
            {
                // Try to check for a folder that meets the requirements and have been edited by a user
                plansFolder = topFolders.Data
                                .FirstOrDefault(f => f.Attributes.Hidden == false
                                && f.Attributes.Extension != null
                                && f.Attributes.Extension.Data != null
                                && f.Attributes.Extension.Data.Actions != null
                                && f.Attributes.Extension.Data.Actions.Contains("CONVERT")
                                && f.Attributes.Extension.Data.Actions.Contains("SPLIT")
                                && f.Attributes.Extension.Data.Actions.Contains("OCR")
                                && !string.IsNullOrWhiteSpace(f.Attributes.LastModifiedUserId));

                // If the above fails... the last hope is to grab any folder that meets the requirements...
                if (plansFolder == null)
                {
                    plansFolder = topFolders.Data
                                    .FirstOrDefault(f => f.Attributes.Hidden == false
                                    && f.Attributes.Extension != null
                                    && f.Attributes.Extension.Data != null
                                    && f.Attributes.Extension.Data.Actions != null
                                    && f.Attributes.Extension.Data.Actions.Contains("CONVERT")
                                    && f.Attributes.Extension.Data.Actions.Contains("SPLIT")
                                    && f.Attributes.Extension.Data.Actions.Contains("OCR"));
                }
            }

            return plansFolder;
        }

        public async Task<FolderData> GetProjectFilesFolderAsync(string accountId, string projectId, string token)
        {
            var topFolders = await this.GetTopFoldersAsync(accountId, projectId, token);

            return GetProjectFilesFolder(topFolders);
        }

        public FolderData GetProjectFilesFolder(TopFolders topFolders)
        {
            var projectFolder = topFolders.Data.FirstOrDefault(f => f.Attributes.Name == "Project Files");

            // If project files folder not there, maybe it has been renamed?
            // Looks like different languages do not affect the API, only the UI itself, because it is a browser based translation as per link: (Tested and worked)
            // https://knowledge.autodesk.com/support/bim-360/learn-explore/caas/CloudHelp/cloudhelp/ENU/BIM-360-Docs/files/GUID-0F437870-94AE-46AC-BDFE-F3994B3A98D0-htm.html
            if (projectFolder == null)
            {
                // Try to check for a folder that meets the requirements and have been edited by a user
                projectFolder = topFolders.Data
                                .FirstOrDefault(f => f.Attributes.Hidden == false
                                && f.Attributes.Extension != null
                                && f.Attributes.Extension.Data != null
                                && f.Attributes.Extension.Data.Actions != null
                                && f.Attributes.Extension.Data.Actions.Count == 1
                                && f.Attributes.Extension.Data.Actions.Contains("CONVERT")
                                && !string.IsNullOrWhiteSpace(f.Attributes.LastModifiedUserId));

                // If the above fails... the last hope is to grab any folder that meets the requirements...
                if (projectFolder == null)
                {
                    projectFolder = topFolders.Data
                                    .FirstOrDefault(f => f.Attributes.Hidden == false
                                    && f.Attributes.Extension != null
                                    && f.Attributes.Extension.Data != null
                                    && f.Attributes.Extension.Data.Actions != null
                                    && f.Attributes.Extension.Data.Actions.Count == 1
                                    && f.Attributes.Extension.Data.Actions.Contains("CONVERT"));
                }
            }

            return projectFolder;
        }

        public string GetBIM360ItemUrl(string projectId, string folderId, string itemId)
        {
            var url = $"https://docs.b360.autodesk.com/projects/{projectId}/folders/{folderId}/thumbnail/viewer/items/{itemId}";
            return url;
        }
    }
}
