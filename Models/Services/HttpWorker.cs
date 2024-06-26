using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VFMDesctop.Models.Services
{
    internal class HttpWorker
    {
        private readonly HttpClient httpClient;
        private readonly JsonService jsonService;

        public HttpWorker()
        {
            httpClient = new HttpClient();
            jsonService = new JsonService();
        }

        public async Task<string> sendPostRequest<T>(string url, T jsonData)
        {
            string data = jsonService.Serialize<T>(jsonData);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content);
            string respone = await response.Content.ReadAsStringAsync();
            return respone;
        }
    }
}
