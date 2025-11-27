using Microsoft.EntityFrameworkCore;

namespace MyProject
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Example DbSet — remove or replace with your real entities
        public DbSet<ExampleItem> ExampleItems { get; set; } = null!;
    }

    public class ExampleItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}