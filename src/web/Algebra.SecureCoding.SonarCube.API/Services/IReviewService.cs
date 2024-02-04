using Algebra.SecureCoding.SonarCube.API.Models;

namespace Algebra.SecureCoding.SonarCube.API.Services
{
    public interface IReviewService
    {
        Task<List<ReviewDto>?> GetAllReviews();
        Task<List<ReviewDto>?> FindAllByHardwareCode(string hardwareCode);
    }
}
