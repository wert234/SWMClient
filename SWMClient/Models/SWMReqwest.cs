using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SWMClient.Models
{
    public static class SWMReqwest
    {
        public static string _uriBase;

        public static HttpRequestMessage GetReqwest(string action, HttpMethod method, Dictionary<string, object> data = null)
        {
            var reqwest = new HttpRequestMessage(); 
            reqwest.Method = method;
            reqwest.RequestUri = new Uri(_uriBase + action);
            if(data != null)
                reqwest.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            if (Preferences.Get("authorization", "null") != "null")
                reqwest.Headers.Add("authorization", Preferences.Get("authorization", "null"));

           return reqwest;
        }
    }
}
