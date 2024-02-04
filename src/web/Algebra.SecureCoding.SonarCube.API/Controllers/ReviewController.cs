using Algebra.SecureCoding.SonarCube.API.Models;
using Algebra.SecureCoding.SonarCube.API.Services;
using Microsoft.AspNetCore.Mvc;


namespace Algebra.SecureCoding.SonarCube.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet("GetAllReviews")]
        public async Task<List<ReviewDto>?> GetAllReviews()
        {
            return await _reviewService.GetAllReviews();
        }

        [HttpGet("{hardwareCode}",Name ="GetAllReviewsByHardwareCode")]
        public async Task<List<ReviewDto>?> GetAllReviewsByHardwareCode([FromRoute] string hardwareCode)
        {
            return await _reviewService.FindAllByHardwareCode(hardwareCode);
        }
    }
}
