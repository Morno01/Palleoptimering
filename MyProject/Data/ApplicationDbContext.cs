using Microsoft.EntityFrameworkCore;
using MyProject.Models;  // ⬅️ Dette manglede!

namespace MyProject.Data  // ⬅️ Og dette manglede!
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Dine tabeller som DbSet
        public DbSet<Bruger> Bruger { get; set; }
        public DbSet<Element> Element { get; set; }
        public DbSet<Palle> Palle { get; set; }
        public DbSet<Referrings_regel> Referrings_regel { get; set; }
        public DbSet<Mellemrums_regel> Mellemrums_regel { get; set; }
        public DbSet<Stablings_regel> Stablings_regel { get; set; }
        public DbSet<Placering> Placering { get; set; }
    }
}