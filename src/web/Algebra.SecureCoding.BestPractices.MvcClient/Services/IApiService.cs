using Algebra.SecureCoding.BestPractices.MvcClient.Models;

namespace Algebra.SecureCoding.BestPractices.MvcClient.Services
{
    public interface IApiService
    {
        Task<List<WeatherData>> GetSensitiveData();
    }
}
