using ClientPortal.Core.Application.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ClientPortal.Core.Application.External.Sharepoint
{
    public class SharepointService: ISharepointService
    {  
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        public SharepointService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }
        public async Task<List<SharepointProjectData>> GetProjectsFromSharePoint()
        {
            var sharepointBaseUrl = _config.GetValue<string>("Sharepoint:BaseUrl");
            int firmId = _config.GetValue<int>("Sharepoint:FirmId"); 
            int clientId = _config.GetValue<int>("Sharepoint:ClientId");

            var token = await GetTokenFromSharepoint();
            string subscriptionKey = _config.GetValue<string>("Sharepoint:Subscription-Key");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{sharepointBaseUrl}Site/GetAllProjects?memberFirmId={firmId}&ClientId={clientId}")
            {
                Headers =
                {
                    {"Authorization", token },
                    {"Ocp-Apim-Subscription-Key", $"{subscriptionKey}" }
                }
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(requestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // convert the response to a DTO
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SharepointProjectReponseDto>(content);
                return data.responseData;
            }
            else
            {
                // return appropriate error
            }

            return new List<SharepointProjectData>();
        }


        public async Task<MemberFirmUsersDto> GetMemberFirmUsersSharePoint()
        {
            var sharepointBaseUrl = _config.GetValue<string>("Sharepoint:BaseUrl");
            int firmId = _config.GetValue<int>("Sharepoint:FirmId");
            int clientId = _config.GetValue<int>("Sharepoint:ClientId");
            int userRoleType = _config.GetValue<int>("Sharepoint:userRoleType");

            var token = await GetTokenFromSharepoint();
            string subscriptionKey = _config.GetValue<string>("Sharepoint:Subscription-Key");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{sharepointBaseUrl}User/GetMemberFirmUsers?memberFirmId={firmId}&userRoleTypes={userRoleType}")
            {
                Headers =
                {
                    {"Authorization", token },
                    {"Ocp-Apim-Subscription-Key", $"{subscriptionKey}" }
                }
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(requestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // convert the response to a DTO
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MemberFirmUsersDto>(content);
                return data;
            }
            else
            {
                // return appropriate error
            }

            return new MemberFirmUsersDto();
        }


        public async Task<List<SharepointProjectData>> GetProjectsFromSharePoint(int clientId)
        {
            var sharepointBaseUrl = _config.GetValue<string>("Sharepoint:BaseUrl");
            int firmId = _config.GetValue<int>("Sharepoint:FirmId");
            //int clientId = _config.GetValue<int>("Sharepoint:ClientId");

            var token = await GetTokenFromSharepoint();
            string subscriptionKey = _config.GetValue<string>("Sharepoint:Subscription-Key");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{sharepointBaseUrl}Site/GetAllProjects?memberFirmId={firmId}&ClientId={clientId}")
            {
                Headers =
                {
                    {"Authorization", token },
                    {"Ocp-Apim-Subscription-Key", $"{subscriptionKey}" }
                }
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(requestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // convert the response to a DTO
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SharepointProjectReponseDto>(content);
                return data.responseData;
            }
            else
            {
                // return appropriate error
            }

            return new List<SharepointProjectData>();
        }



        public async Task<List<SharepointProjectData>> GetAllProjectsFromSharePoint()
        {
            
            List<SharepointProjectData> resultsList = new List<SharepointProjectData>();
            // get all clients
            var clients = await GetClientsFromSharePoint();
            foreach(var client in clients)
            {
                var projects = await GetProjectsFromSharePoint(client.clientId);
                resultsList.AddRange(projects);
            }

            return resultsList;
        }


        public async Task<List<SharepointClientData>> GetClientsFromSharePoint()
        {
            var sharepointBaseUrl = _config.GetValue<string>("Sharepoint:BaseUrl");
            int firmId = _config.GetValue<int>("Sharepoint:FirmId");
            int clientId = _config.GetValue<int>("Sharepoint:ClientId");

            var token = await GetTokenFromSharepoint();
            string subscriptionKey = _config.GetValue<string>("Sharepoint:Subscription-Key");


            // get all clients


            var requestMessage = new HttpRequestMessage(HttpMethod.Get,
                $"{sharepointBaseUrl}Client/GetClients?memberFirmId={firmId}")
            {
                Headers =
                {
                    {"Authorization", token },
                    {"Ocp-Apim-Subscription-Key", $"{subscriptionKey}" }
                }
            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(requestMessage);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // convert the response to a DTO
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SharepointClientReponseDto>(content);
                return data.responseData;
            }
            else
            {
                // return appropriate error
            }

            return new List<SharepointClientData>();
        }

        private async Task<string> GetTokenFromSharepoint()
        {
            var tenantId = "44f4e7a6-4821-44d7-b286-cd90436c6975";
            var requestMessage = new HttpRequestMessage(HttpMethod.Post,
               $"https://login.microsoftonline.com/{tenantId}/oauth2/token");
            requestMessage.Content = new MultipartFormDataContent()
            {
                {new StringContent("client_credentials"), "grant_type" },
                {new StringContent("5d5ac884-7464-4f6b-8a78-aee64c0a329d"), "client_id" },
                {new StringContent("3wQ8Q~_kS4TSDIU8LKTUA0JEEFgBkyFpYbw~Wacs"), "client_secret" },
                {new StringContent("https://bdoapoutlook.onmicrosoft.com/bdo-web-prvapi-acc"), "resource" }

            };
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(requestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<SharepointTokenDto>(content);
                return $"Bearer {data.access_token}";
            }

            return null;
        }



        public async Task<HttpResponseMessage> AddProjectUserSharePoint(SharepointUserProjectData data)
        {
            var sharepointBaseUrl = _config.GetValue<string>("Sharepoint:BaseUrl");
            // int firmId = _config.GetValue<int>("Sharepoint:FirmId");
            //int clientId = _config.GetValue<int>("Sharepoint:ClientId");

            var token = await GetTokenFromSharepoint();
            string subscriptionKey = _config.GetValue<string>("Sharepoint:Subscription-Key");


            // get all clients


            var requestMessage = new HttpRequestMessage(HttpMethod.Post,
                $"{sharepointBaseUrl}User/AddProjectUsers")
            {
                Headers =
                {
                    {"Authorization", token },
                    {"Ocp-Apim-Subscription-Key", $"{subscriptionKey}" }
                }
            };

            requestMessage.Content = JsonContent.Create(data);
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(requestMessage);
            
            return httpResponseMessage;
       

        }



        public async Task<List<HttpResponseMessage>> AddProjectsUserSharePoint(SharepointUserProjectsData data)
        {

            if (data != null && data.listProjects !=null)
            {
                List<HttpResponseMessage> listOfResults = new List<HttpResponseMessage>();
                foreach (var project in data.listProjects)
                {
                    var element = new SharepointUserProjectData();
                    element.clientId = project.clientId;
                    element.projectId = project.projectId;
                    element.memberFirmId = data.memberFirmId;
                    element.usersData = data.usersData;
                    var result = await AddProjectUserSharePoint(element);
                   
                    listOfResults.Add(result);
                }

                return listOfResults;
            }
            else
            {
                return null;
            }
            
        }
    }
}
