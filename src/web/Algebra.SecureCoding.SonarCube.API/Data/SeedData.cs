using Microsoft.EntityFrameworkCore;

namespace Algebra.SecureCoding.SonarCube.API.Data
{
    public static class SeedData
    {
        public static IApplicationBuilder EnsureSeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var contextData = scope.ServiceProvider.GetService<DataContext>();

            if (contextData!=null)
            {
                contextData.Database.Migrate();
                if (!contextData.Hardwares.Any())
                {
                    contextData.Database.BeginTransaction();
                    contextData.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Hardwares ON");
                    contextData.Hardwares.AddRange(
                        new Hardware { Id = 1, Code = "1234561", Name = "Asus TUF RTX 3080", Type = HardwareType.GPU, Stock = 0, Price = 1599.00M },
                        new Hardware { Id = 2, Code = "1234562", Name = "EVGA XC3 RTX 3070 Ti", Type = HardwareType.GPU, Stock = 3, Price = 1299.00M },
                        new Hardware { Id = 3, Code = "1234563", Name = "AMD Ryzen 5950X", Type = HardwareType.CPU, Stock = 15, Price = 899.00M },
                        new Hardware { Id = 4, Code = "1234564", Name = "Samsung 980 PRO SSD 1TB", Type = HardwareType.STORAGE, Stock = 30, Price = 299.00M },
                        new Hardware { Id = 5, Code = "1234565", Name = "Kingston FURY Beast DDR5 32GB", Type = HardwareType.RAM, Stock = 0, Price = 699.00M }
                        );
                    contextData.SaveChanges();
                    contextData.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Hardwares OFF");
                    contextData.Database.CommitTransaction();
                }

                if (!contextData.Reviews.Any())
                {
                    contextData.Database.BeginTransaction();
                    contextData.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Reviews ON");
                    contextData.Reviews.AddRange(
                        new Review { Id = 1, Title = "Radi odlično", Text = "Radi odlično sa zahtjevnim aplikacijama.", Rating = 5, HardwareId = 1 },
                        new Review { Id = 2, Title = "Nisam impresioniran", Text = "Ne dobivam očekivane performanse.", Rating = 3, HardwareId = 2 },
                        new Review { Id = 3, Title = "Sretan kupac", Text = "Nemam nikakvih problema s procesorom. Mogao bi biti malo jeftiniji.", Rating = 5, HardwareId = 3 },
                        new Review { Id = 4, Title = "Nije vrijedan cijene", Text = "Poboljšanje performansi ne opravdava visoku cijenu.", Rating = 2, HardwareId = 4 },
                        new Review { Id = 5, Title = "Dobivam odlične brzine", Text = "Ne mogu vjerovati da radi tako brzo.", Rating = 5, HardwareId = 5 },
                        new Review { Id = 6, Title = "Najbolja grafička na tržištu", Text = "Ne isplati se uzimati 3090, ova je dovoljno dobra.", Rating = 5, HardwareId = 1 }
                        );
                    contextData.SaveChanges();
                    contextData.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Reviews OFF");
                    contextData.Database.CommitTransaction();
                }
            }
            
            return app;
        }
    }
}
