using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Algebra.SecureCoding.Tests
{
    public class ReviewUnitTest
    {
        public ReviewUnitTest()
        {
            
        }

        [Fact]
        public async Task FindAllByHardwareCode_ReturnsReviews()
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




            context.Reviews.AddRange(
                        new Review { Id = 1, Title = "Radi odlično", Text = "Radi odlično sa zahtjevnim aplikacijama.", Rating = 5, HardwareId = 1 },
                        new Review { Id = 2, Title = "Nisam impresioniran", Text = "Ne dobivam očekivane performanse.", Rating = 3, HardwareId = 2 },
                        new Review { Id = 3, Title = "Sretan kupac", Text = "Nemam nikakvih problema s procesorom. Mogao bi biti malo jeftiniji.", Rating = 5, HardwareId = 3 },
                        new Review { Id = 4, Title = "Nije vrijedan cijene", Text = "Poboljšanje performansi ne opravdava visoku cijenu.", Rating = 2, HardwareId = 4 },
                        new Review { Id = 5, Title = "Dobivam odlične brzine", Text = "Ne mogu vjerovati da radi tako brzo.", Rating = 5, HardwareId = 5 },
                        new Review { Id = 6, Title = "Najbolja grafička na tržištu", Text = "Ne isplati se uzimati 3090, ova je dovoljno dobra.", Rating = 5, HardwareId = 1 }
                        );
            context.SaveChanges();



            var reviewService = new ReviewService(context);
            var result = await reviewService.FindAllByHardwareCode("1234561");

            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public async Task FindAllByHardwareCode_ReturnsNoReviews()
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

            context.Reviews.AddRange(
                       new Review { Id = 1, Title = "Radi odlično", Text = "Radi odlično sa zahtjevnim aplikacijama.", Rating = 5, HardwareId = 1 },
                       new Review { Id = 2, Title = "Nisam impresioniran", Text = "Ne dobivam očekivane performanse.", Rating = 3, HardwareId = 2 },
                       new Review { Id = 3, Title = "Sretan kupac", Text = "Nemam nikakvih problema s procesorom. Mogao bi biti malo jeftiniji.", Rating = 5, HardwareId = 3 },
                       new Review { Id = 4, Title = "Nije vrijedan cijene", Text = "Poboljšanje performansi ne opravdava visoku cijenu.", Rating = 2, HardwareId = 4 },
                       new Review { Id = 5, Title = "Dobivam odlične brzine", Text = "Ne mogu vjerovati da radi tako brzo.", Rating = 5, HardwareId = 5 },
                       new Review { Id = 6, Title = "Najbolja grafička na tržištu", Text = "Ne isplati se uzimati 3090, ova je dovoljno dobra.", Rating = 5, HardwareId = 1 }
                       );
            context.SaveChanges();


            var reviewService = new ReviewService(context);
            var result = await reviewService.FindAllByHardwareCode("134");

            Assert.NotNull(result);
            Assert.True(result.Count == 0);
        }

        [Fact]
        public async Task GetAllReviews_Returns6Reviews()
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

            context.Reviews.AddRange(
                       new Review { Id = 1, Title = "Radi odlično", Text = "Radi odlično sa zahtjevnim aplikacijama.", Rating = 5, HardwareId = 1 },
                       new Review { Id = 2, Title = "Nisam impresioniran", Text = "Ne dobivam očekivane performanse.", Rating = 3, HardwareId = 2 },
                       new Review { Id = 3, Title = "Sretan kupac", Text = "Nemam nikakvih problema s procesorom. Mogao bi biti malo jeftiniji.", Rating = 5, HardwareId = 3 },
                       new Review { Id = 4, Title = "Nije vrijedan cijene", Text = "Poboljšanje performansi ne opravdava visoku cijenu.", Rating = 2, HardwareId = 4 },
                       new Review { Id = 5, Title = "Dobivam odlične brzine", Text = "Ne mogu vjerovati da radi tako brzo.", Rating = 5, HardwareId = 5 },
                       new Review { Id = 6, Title = "Najbolja grafička na tržištu", Text = "Ne isplati se uzimati 3090, ova je dovoljno dobra.", Rating = 5, HardwareId = 1 }
                       );
            context.SaveChanges();


            var reviewService = new ReviewService(context);
            var result = await reviewService.GetAllReviews();

            Assert.NotNull(result);
            Assert.True(result.Count == 6);
        }

    }
}
