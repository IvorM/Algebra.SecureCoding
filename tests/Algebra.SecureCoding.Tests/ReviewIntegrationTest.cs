using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Models;
using Algebra.SecureCoding.SonarCube.API.Services;
using Azure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algebra.SecureCoding.Tests
{
    public class ReviewIntegrationTest
    {
        [Fact]
        public async Task GetAllReviews_RetutrnsOneReview()
        {
            var reviewService = Substitute.For<IReviewService>();
            reviewService.GetAllReviews().Returns(new List<ReviewDto>() { new ReviewDto { Rating = 1, Text = "Test", Title = "test" } });
            var webAppFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(sevices =>
                {
                    sevices.AddScoped<IReviewService>(r => reviewService);
                });
            });
            var client= webAppFactory.CreateClient();

            var response = await client.GetAsync("/api/Review/GetAllReviews");
            response.EnsureSuccessStatusCode();
            var responseDto=JsonConvert.DeserializeObject<List<ReviewDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(responseDto);
            Assert.Equal(responseDto.Count, 1);
        }

        [Fact]
        public async Task GetByCode_ReturnsOneReview()
        {
            var reviewService = Substitute.For<IReviewService>();
            reviewService.FindAllByHardwareCode("123").Returns(new List<ReviewDto>() { new ReviewDto { Rating = 1, Text = "Test", Title = "test" } });
            var webAppFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(sevices =>
                {
                    sevices.AddScoped<IReviewService>(r => reviewService);
                });
            });
            var client = webAppFactory.CreateClient();

            var response = await client.GetAsync("/api/Review/123");
            response.EnsureSuccessStatusCode();
            var responseDto = JsonConvert.DeserializeObject<List<ReviewDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(responseDto);
            Assert.Equal(responseDto.Count, 1);
        }
    }
}
