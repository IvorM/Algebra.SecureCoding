using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace Albegra.SecureCoding.Auth.API.Data
{
    public static class SeedData
    {
        public static IApplicationBuilder EnsureSeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var contextData = scope.ServiceProvider.GetService<ApplicationDbContext>();

            if (contextData != null)
            {
                contextData.Database.Migrate();
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var user = userManager.FindByEmailAsync("ivor.maric@gmail.com").Result;

            if (user is null)
            {
                user = new IdentityUser
                {
                    UserName = "ivor.maric@gmail.com",
                    Email = "ivor.maric@gmail.com",
                    EmailConfirmed = true,
                };
                var result = userManager.CreateAsync(user, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }

            return app;
        }
    }
}
