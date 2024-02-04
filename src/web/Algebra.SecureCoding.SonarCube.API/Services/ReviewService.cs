using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Mapper;
using Algebra.SecureCoding.SonarCube.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Algebra.SecureCoding.SonarCube.API.Services
{
    public class ReviewService : IReviewService
    {
        private readonly DataContext _context;
        public ReviewService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<ReviewDto>?> FindAllByHardwareCode(string hardwareCode)
        {
            return await _context.Reviews
                .Include(x => x.Hardware)
                .Where(x => x.Hardware.Code.Equals(hardwareCode))
                .Select(x=>x.ToReviewDto())
                .ToListAsync();
        }

        public async Task<List<ReviewDto>?> GetAllReviews()
        {
            return await _context.Reviews
                .Select(x =>x.ToReviewDto())
                .ToListAsync();
        }
    }
}
