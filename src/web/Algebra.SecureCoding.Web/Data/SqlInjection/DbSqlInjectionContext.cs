using Algebra.SecureCoding.Web.Data.SqlInjection.Entities;
using Microsoft.EntityFrameworkCore;

namespace Algebra.SecureCoding.Web.Data.SqlInjection
{
    public class DbSqlInjectionContext : DbContext
    {
        public DbSqlInjectionContext(DbContextOptions<DbSqlInjectionContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
