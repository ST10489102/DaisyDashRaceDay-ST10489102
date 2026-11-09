using System.Net.Http.Json;

namespace DaisyDashRaceDay.MVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        public ApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient();
            _http.BaseAddress = new Uri("https://localhost:7001/api/");
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            try { return await _http.GetFromJsonAsync<T>(url); }
            catch { return default; }
        }

        public async Task<bool> PostAsync<T>(string url, T data)
        {
            var resp = await _http.PostAsJsonAsync(url, data);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string url)
        {
            var resp = await _http.DeleteAsync(url);
            return resp.IsSuccessStatusCode;
        }
    }
}