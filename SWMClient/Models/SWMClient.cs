using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.Models
{
    public class SWMClient
    {
        private HttpClient _httpClient;
        private Dictionary<string, object> RequestData;

        public SWMClient()
        {    
            RequestData = new Dictionary<string, object>();
            if (Device.RuntimePlatform == Device.Android)
                SWMReqwest._uriBase = "https://10.0.2.2:5001/api/";
            else if(Device.RuntimePlatform == Device.WinUI)
                SWMReqwest._uriBase = "https://localhost:5001/api/";
        }
        

        public async Task<string> RegistrationAsync(string login, string password, string FullName)
        {
           using(_httpClient = new HttpClient())
           {
                using (_httpClient = new HttpClient())
                {
                    RequestData.Add("login", login);
                    RequestData.Add("password", password);
                    RequestData.Add("fullName", FullName);

                    var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                        "Authorization/registation",
                        HttpMethod.Post,
                        RequestData));

                    return await GetResponse(respons);
                }
            }
        }
        public async Task<string> LogInAsync(string login, string pasword)
        {
            using (_httpClient = new HttpClient(new SWMHttpClientHandler()))
            {
                RequestData.Add("login", login);
                RequestData.Add("password", pasword);

                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                    "Authorization/login",
                    HttpMethod.Post,
                    RequestData));

                return await GetResponse(respons);
            }
        }
        public async Task<string> GetDwarfsAsync()
        {
            using (_httpClient = new HttpClient(new SWMHttpClientHandler()))
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                    "SnowWhite/dwarfs",
                    HttpMethod.Get));

                return await GetResponse(respons);
            }
        }
        public async Task<string> GetDwarfsTypesAsync()
        {
            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                    "Dwarfs/types",
                    HttpMethod.Get));

                return await GetResponse(respons);
            }
        }
        public async Task<string> CreateDwarfsAsync()
        {
            RequestData.Add("name", " ");
            RequestData.Add("class", 0);

            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                    "Dwarfs",
                    HttpMethod.Post,
                    RequestData));

                return await GetResponse(respons);
            }
        }
        public async Task<string> EditDwarfsAsync(string name, int _class ,int id)
        {
            RequestData.Add("name", name);
            RequestData.Add("class", _class);

            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                     $"Dwarfs/{id}",
                    HttpMethod.Put,
                    RequestData));

                return await GetResponse(respons);
            }
        }
        public async Task<string> DeleteDwarfsAsync(int id)
        {
            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                     $"Dwarfs/{id}",
                    HttpMethod.Delete));

                return await GetResponse(respons);
            }
        }
        public async Task<string> GetSnowWhiteAsync(int snowWhiteId)
        {
            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                     $"SnowWhite/{snowWhiteId}",
                    HttpMethod.Get));

                return await GetResponse(respons);
            }
        }
        public async Task<string> GetSnowWhiteAsync()
        {
            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                     $"SnowWhite",
                    HttpMethod.Get));

                return await GetResponse(respons);
            }
        }
        public async Task<string> EditNameSnowWhiteAsync(string fullName)
        {
            using (_httpClient = new HttpClient())
            {
                RequestData.Add("fullName", fullName);

                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                     $"SnowWhite",
                    HttpMethod.Put,
                    RequestData));

                return await GetResponse(respons);
            }
        }
        public async Task<string> GetRequestsAsync()
        {
            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                     $"SnowWhite/requests",
                    HttpMethod.Get));

                return await GetResponse(respons);
            }
        }
        public async Task<string> SendAnswerAsync(int requestId, bool accept)
        {
            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                     $"SnowWhite/requests/{requestId}/answer?accept={accept}",
                    HttpMethod.Head));

                return await GetResponse(respons);
            }
        }
        public async Task<string> SendRequestsAsync(int id)
        {
            using (_httpClient = new HttpClient())
            {
                var respons = await _httpClient.SendAsync(SWMReqwest.GetReqwest(
                     $"Dwarfs/{id}/move-to",
                    HttpMethod.Head));

                return await GetResponse(respons);
            }
        }


        private async Task<string> GetResponse(HttpResponseMessage respons)
        {
            RequestData.Clear();

            if (respons.IsSuccessStatusCode)
                return await respons.Content.ReadAsStringAsync();
            else
                return null;
        }

    }
}
