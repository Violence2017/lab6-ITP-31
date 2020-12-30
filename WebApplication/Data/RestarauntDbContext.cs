using WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Data
{
    public class RestarauntDbContext : DbContext
    {
        public RestarauntDbContext(DbContextOptions<RestarauntDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Provider> Providers { get; set; }
    }
}