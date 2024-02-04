using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Models;

namespace Algebra.SecureCoding.SonarCube.API.Mapper
{
    public static class ReviewMapper
    {
        public static ReviewDto ToReviewDto(this Review review)
        {
            return new ReviewDto
            {
                Rating = review.Rating,
                Text = review.Text,
                Title = review.Title
            };
        }
    }
}
