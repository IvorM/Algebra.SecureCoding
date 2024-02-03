using Algebra.SecureCoding.Web.Data.SqlInjection;
using Algebra.SecureCoding.Web.Data.SqlInjection.Entities;
using Algebra.SecureCoding.Web.Models.Shared;
using Algebra.SecureCoding.Web.Models.SqlInjection;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Algebra.SecureCoding.Web.Services
{
    public class ProductService:IProductService
    {
        private readonly DbSqlInjectionContext _context;
        public ProductService(DbSqlInjectionContext context)
        {
            _context = context;
        }
        public async Task<Result<List<ProductDto>>> SearchForProductByName(string name)
        {
            try
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.EnsureCreatedAsync();
                await _context.Products.AddRangeAsync(
                    new Product { Name = "Laptop", Price = 1000 },
                    new Product { Name = "Smartphone", Price = 500 }
                );
                await _context.SaveChangesAsync();

                var products = await _context.Products
                            .FromSqlRaw($"SELECT * FROM Products WHERE Name LIKE '%{name}%'")
                            .Select(x => new ProductDto
                            {
                                Name = x.Name,
                                Price = x.Price
                            })
                            .ToListAsync();

                return new Result<List<ProductDto>>
                {
                    IsOk = true,
                    Value = products
                };
            }
            catch (Exception e)
            {

                return new Result<List<ProductDto>>
                {
                    IsOk = false,
                    ErrorMessage = e.Message
                };
            }
           
        }
    }
}
