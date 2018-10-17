using Microsoft.EntityFrameworkCore;

namespace TOT.Data
{
    public class TOTDBContext : DbContext
    {
        public TOTDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
