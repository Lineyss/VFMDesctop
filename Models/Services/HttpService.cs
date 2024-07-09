using System.Net.Http;
using System.Threading.Tasks;
using VFMDesctop.Models.Interfaces;

namespace VFMDesctop.Models.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> PostRequest(string url, string data)
        {
            HttpContent content = new StringContent(data);
            HttpResponseMessage response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
