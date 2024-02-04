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
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Algebra.SecureCoding.Tests
{
    public class HardwareIntegrationTest
    {
        [Fact]
        public async Task GetAllHardware_ReturnsOneHardware()
        {
            var hardwareService = Substitute.For<IHardwareService>();
            hardwareService.GetAll().Returns(new List<HardwareDto>() { new HardwareDto { Code="1111111", Name="Test", Price=100m } });
            var webAppFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(sevices =>
                {
                    sevices.AddScoped<IHardwareService>(r => hardwareService);
                });
            });
            var client = webAppFactory.CreateClient();

            var response = await client.GetAsync("/api/Hardware/GetAll");
            response.EnsureSuccessStatusCode();
            var responseDto = JsonConvert.DeserializeObject<List<HardwareDto>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(responseDto);
            Assert.Equal(responseDto.Count, 1);
        }

        [Fact]
        public async Task GetByCode_ReturnsOneHardware()
        {
            var hardwareService = Substitute.For<IHardwareService>();
            hardwareService.GetByCode("1111111").Returns( new HardwareDto { Code = "1111111", Name = "Test", Price = 100m } );
            var webAppFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(sevices =>
                {
                    sevices.AddScoped<IHardwareService>(r => hardwareService);
                });
            });
            var client = webAppFactory.CreateClient();

            var response = await client.GetAsync("/api/Hardware/1111111");
            response.EnsureSuccessStatusCode();
            var responseDto = JsonConvert.DeserializeObject<HardwareDto>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(responseDto);
            Assert.Equal(responseDto.Code, "1111111");
        }

        [Fact]
        public async Task SaveHardware_ReturnsOneHardware()
        {
            var dto = new NewHardwareDto { Code = "1111111", Name = "Test", Price = 100m, Stock = 100, Type = HardwareType.CPU };
            var hardwareService = Substitute.For<IHardwareService>();
            hardwareService.Save(dto).ReturnsForAnyArgs(new HardwareDto { Code = "1111111", Name = "Test", Price = 100m });
            var webAppFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(sevices =>
                {
                    sevices.AddScoped<IHardwareService>(r => hardwareService);
                });
            });
            var client = webAppFactory.CreateClient();

            var response = await client.PostAsJsonAsync("/api/Hardware/Save", dto);
            response.EnsureSuccessStatusCode();
            var responseDto = JsonConvert.DeserializeObject<HardwareDto>(await response.Content.ReadAsStringAsync());
            Assert.Equal(System.Net.HttpStatusCode.OK,response.StatusCode);
            Assert.NotNull(responseDto);
            Assert.Equal(responseDto.Code, dto.Code);
        }

        [Fact]
        public async Task UpdateHardware_ReturnsOneHardware()
        {
            var dto = new UpdateHardwareDto { Code = "1111111", Name = "Test", Price = 100m, Stock = 100, Type = HardwareType.CPU };
            var hardwareService = Substitute.For<IHardwareService>();
            hardwareService.Update("1111111",dto).ReturnsForAnyArgs(new HardwareDto { Code = "1111111", Name = "Test", Price = 100m });
            var webAppFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(sevices =>
                {
                    sevices.AddScoped<IHardwareService>(r => hardwareService);
                });
            });
            var client = webAppFactory.CreateClient();

            var response = await client.PutAsJsonAsync($"/api/Hardware/{dto.Code}", dto);
            response.EnsureSuccessStatusCode();
            var responseDto = JsonConvert.DeserializeObject<HardwareDto>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(responseDto);
            Assert.Equal(responseDto.Code, "1111111");
        }

        [Fact]
        public async Task DeleteHardware_ReturnsNoContent()
        {
            var code = "1111111";
            var hardwareService = Substitute.For<IHardwareService>();
            hardwareService.Delete(code).Returns(Task.FromResult(true));
            var webAppFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(sevices =>
                {
                    sevices.AddScoped<IHardwareService>(r => hardwareService);
                });
            });
            var client = webAppFactory.CreateClient();

            var response = await client.DeleteAsync($"/api/Hardware/{code}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
