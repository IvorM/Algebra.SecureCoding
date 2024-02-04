using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Algebra.SecureCoding.SonarCube.API.Services
{
    public class ReviewService : IReviewService
    {
        public DataContext _context;
        public ReviewService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<ReviewDto>?> FindAllByHardwareCode(string hardwareCode)
        {
            return  _context.Reviews
                .Include(x => x.Hardware)
                .Where(x => x.Hardware.Code.Equals(hardwareCode))
                .Select(x => new ReviewDto
                {
                    Rating = x.Rating,
                    Text = x.Text,
                    Title= x.Title
                })
                .ToList();
        }

        public async Task<List<ReviewDto>> GetAllReviews()
        {
            return await _context.Reviews
                .Select(x => new ReviewDto()
                {
                    Rating = x.Rating,
                    Text = x.Text,
                    Title = x.Title
                })
                .ToListAsync();
        }
    }
}
