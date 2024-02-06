using Algebra.SecureCoding.BestPractices.MvcClient.Models;
using System.Net.Http;

namespace Algebra.SecureCoding.BestPractices.MvcClient.Services
{
    public class ApiService:IApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<WeatherData>> GetSensitiveData()
        {
            var response = await _httpClient.GetAsync("/WeatherForecast");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<List<WeatherData>>();
            return content;
        }
    }
}
