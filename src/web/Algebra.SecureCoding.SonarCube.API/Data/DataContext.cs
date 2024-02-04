using Microsoft.EntityFrameworkCore;

namespace Algebra.SecureCoding.SonarCube.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<Hardware> Hardwares { get; set; }
    }
}
