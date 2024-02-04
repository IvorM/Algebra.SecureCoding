using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Models;
using Algebra.SecureCoding.SonarCube.API.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algebra.SecureCoding.Tests
{
    public class HardwareUnitTest
    {
        [Fact]
        public async Task FindByHardwareCode_ReturnsHardware()
        {
            var dbOption = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}").Options;


            using var context = new DataContext(dbOption);
            context.Hardwares.AddRange(
                new Hardware { Id = 1, Code = "1234561", Name = "Asus TUF RTX 3080", Type = HardwareType.GPU, Stock = 0, Price = 1599.00M },
                new Hardware { Id = 2, Code = "1234562", Name = "EVGA XC3 RTX 3070 Ti", Type = HardwareType.GPU, Stock = 3, Price = 1299.00M },
                new Hardware { Id = 3, Code = "1234563", Name = "AMD Ryzen 5950X", Type = HardwareType.CPU, Stock = 15, Price = 899.00M },
                new Hardware { Id = 4, Code = "1234564", Name = "Samsung 980 PRO SSD 1TB", Type = HardwareType.STORAGE, Stock = 30, Price = 299.00M },
                new Hardware { Id = 5, Code = "1234565", Name = "Kingston FURY Beast DDR5 32GB", Type = HardwareType.RAM, Stock = 0, Price = 699.00M }
                );


            context.SaveChanges();



            var reviewService = new HardwareServices(context);
            var result = await reviewService.GetByCode("1234561");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task FindAllHardware_Returns5Hardware()
        {
            var dbOption = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}").Options;


            using var context = new DataContext(dbOption);
            context.Hardwares.AddRange(
                new Hardware { Id = 1, Code = "1234561", Name = "Asus TUF RTX 3080", Type = HardwareType.GPU, Stock = 0, Price = 1599.00M },
                new Hardware { Id = 2, Code = "1234562", Name = "EVGA XC3 RTX 3070 Ti", Type = HardwareType.GPU, Stock = 3, Price = 1299.00M },
                new Hardware { Id = 3, Code = "1234563", Name = "AMD Ryzen 5950X", Type = HardwareType.CPU, Stock = 15, Price = 899.00M },
                new Hardware { Id = 4, Code = "1234564", Name = "Samsung 980 PRO SSD 1TB", Type = HardwareType.STORAGE, Stock = 30, Price = 299.00M },
                new Hardware { Id = 5, Code = "1234565", Name = "Kingston FURY Beast DDR5 32GB", Type = HardwareType.RAM, Stock = 0, Price = 699.00M }
                );


            context.SaveChanges();



            var reviewService = new HardwareServices(context);
            var result = await reviewService.GetAll();

            Assert.NotNull(result);
            Assert.Equal(result.Count,5);
        }

        [Fact]
        public async Task SaveHardware_ReturnsHardwartDto()
        {
            var dbOption = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}").Options;


            using var context = new DataContext(dbOption);
            context.Hardwares.AddRange(
                new Hardware { Id = 1, Code = "1234561", Name = "Asus TUF RTX 3080", Type = HardwareType.GPU, Stock = 0, Price = 1599.00M },
                new Hardware { Id = 2, Code = "1234562", Name = "EVGA XC3 RTX 3070 Ti", Type = HardwareType.GPU, Stock = 3, Price = 1299.00M },
                new Hardware { Id = 3, Code = "1234563", Name = "AMD Ryzen 5950X", Type = HardwareType.CPU, Stock = 15, Price = 899.00M },
                new Hardware { Id = 4, Code = "1234564", Name = "Samsung 980 PRO SSD 1TB", Type = HardwareType.STORAGE, Stock = 30, Price = 299.00M },
                new Hardware { Id = 5, Code = "1234565", Name = "Kingston FURY Beast DDR5 32GB", Type = HardwareType.RAM, Stock = 0, Price = 699.00M }
                );

            context.SaveChanges();



            var reviewService = new HardwareServices(context);
            var result = await reviewService.Save(new NewHardwareDto { Code="1111111",Name="Test",Price=100m,Stock=10, Type= HardwareType.STORAGE});

            Assert.NotNull(result);
            Assert.IsType<HardwareDto>(result);
            Assert.Equal(result.Code, "1111111");
        }

        [Fact]
        public async Task UpdateHardware_ReturnsHardwartDto()
        {
            var dbOption = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase($"TestDatabase_{Guid.NewGuid()}").Options;


            using var context = new DataContext(dbOption);
            context.Hardwares.AddRange(
                new Hardware { Id = 1, Code = "1234561", Name = "Asus TUF RTX 3080", Type = HardwareType.GPU, Stock = 0, Price = 1599.00M },
                new Hardware { Id = 2, Code = "1234562", Name = "EVGA XC3 RTX 3070 Ti", Type = HardwareType.GPU, Stock = 3, Price = 1299.00M },
                new Hardware { Id = 3, Code = "1234563", Name = "AMD Ryzen 5950X", Type = HardwareType.CPU, Stock = 15, Price = 899.00M },
                new Hardware { Id = 4, Code = "1234564", Name = "Samsung 980 PRO SSD 1TB", Type = HardwareType.STORAGE, Stock = 30, Price = 299.00M },
                new Hardware { Id = 5, Code = "1234565", Name = "Kingston FURY Beast DDR5 32GB", Type = HardwareType.RAM, Stock = 0, Price = 699.00M }
                );




            context.SaveChanges();



            var reviewService = new HardwareServices(context);
            var result = await reviewService.Update("1234561", new UpdateHardwareDto { Code = "1111111", Name = "Test", Price = 100m, Stock = 10, Type = HardwareType.STORAGE });

            Assert.NotNull(result);
            Assert.IsType<HardwareDto>(result);
            Assert.Equal(result.Code, "1111111");
        }
    }
}
