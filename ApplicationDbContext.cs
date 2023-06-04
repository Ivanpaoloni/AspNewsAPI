using AspNewsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNewsAPI.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<News> News { get; set; }
    }
}
