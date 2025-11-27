using Microsoft.EntityFrameworkCore;
using PalleOptimering.Domain.Models;

namespace PalleOptimering.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // ===== DBSETS =====

        public DbSet<Palle> Paller { get; set; }
        public DbSet<Element> Elementer { get; set; }
        public DbSet<Placering> Placeringer { get; set; }
        public DbSet<Rotations_Regel> RotationsRegler { get; set; }
        public DbSet<Mellemrum_Regel> MellemrumRegler { get; set; }
        public DbSet<Stablings_Regel> StablingsRegler { get; set; }
        public DbSet<Bruger> Brugere { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===== PALLE KONFIGURATION =====

            modelBuilder.Entity<Palle>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PalleBeskrivelse)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Vaegt)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.MaksVaegt)
                      .HasColumnType("decimal(10,2)");

                // Indexes for bedre performance
                entity.HasIndex(e => e.Aktiv);
                entity.HasIndex(e => e.Palletype);

                // Relationer
                entity.HasMany(p => p.Placeringer)
                      .WithOne(pl => pl.Palle)
                      .HasForeignKey(pl => pl.PalleId)
                      .OnDelete(DeleteBehavior.Restrict); // Beskyt mod utilsigtet sletning
            });

            // ===== ELEMENT KONFIGURATION =====

            modelBuilder.Entity<Element>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Vaegt)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.MaaRoteres)
                      .IsRequired()
                      .HasDefaultValue("Ja");

                // Indexes
                entity.HasIndex(e => e.ElementReferenceId);
                entity.HasIndex(e => e.Serie);
                entity.HasIndex(e => e.Maerke);

                // Relationer
                entity.HasMany(e => e.Placeringer)
                      .WithOne(pl => pl.Element)
                      .HasForeignKey(pl => pl.ElementId)
                      .OnDelete(DeleteBehavior.Cascade); // Hvis element slettes, slet også placeringer
            });

            // ===== PLACERING KONFIGURATION =====

            modelBuilder.Entity<Placering>(entity =>
            {
                entity.HasKey(e => e.Id);

                // Composite index for queries
                entity.HasIndex(e => new { e.PalleId, e.Raekke, e.Lag });

                // Constraints
                entity.HasCheckConstraint("CK_Placering_Raekke", "[Raekke] >= 1 AND [Raekke] <= 5");
                entity.HasCheckConstraint("CK_Placering_Lag", "[Lag] >= 1 AND [Lag] <= 10");
            });

            // ===== ROTATIONS_REGEL KONFIGURATION =====

            modelBuilder.Entity<Rotations_Regel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.TilladVendeOpTilMaksVaegt)
                      .HasColumnType("decimal(10,2)");

                entity.Property(e => e.HoejdeBreddefaktor)
                      .HasColumnType("decimal(5,2)");

                entity.HasIndex(e => e.Aktiv);
            });

            // ===== MELLEMRUM_REGEL KONFIGURATION =====

            modelBuilder.Entity<Mellemrum_Regel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Aktiv);
            });

            // ===== STABLINGS_REGEL KONFIGURATION =====

            modelBuilder.Entity<Stablings_Regel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.TilladStablingOpTilMaksVaegt)
                      .HasColumnType("decimal(10,2)");

                entity.HasIndex(e => e.Aktiv);
            });

            // ===== BRUGER KONFIGURATION =====

            modelBuilder.Entity<Bruger>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Brugernavn)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(255);

                // Unique constraints
                entity.HasIndex(e => e.Brugernavn).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.HasIndex(e => e.Aktiv);
            });

            // ===== SEED DATA =====

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Standard paller
            modelBuilder.Entity<Palle>().HasData(
                new Palle
                {
                    Id = 1,
                    PalleBeskrivelse = "Standard EUR palle 80x120",
                    Laengde = 1200,
                    Bredde = 800,
                    Hoejde = 150,
                    Pallegruppe = "80'er",
                    Palletype = "Træ",
                    Vaegt = 25m,
                    MaksHoejde = 2800,
                    MaksVaegt = 800m,
                    Overmaal = 50,
                    LuftMellemElementer = 10,
                    Aktiv = true,
                    OprettetDato = new DateTime(2024, 11, 1)
                },
                new Palle
                {
                    Id = 2,
                    PalleBeskrivelse = "Aluminium palle 100x120",
                    Laengde = 1200,
                    Bredde = 1000,
                    Hoejde = 100,
                    Pallegruppe = "100'er",
                    Palletype = "Aluminium",
                    Vaegt = 15m,
                    MaksHoejde = 3000,
                    MaksVaegt = 1000m,
                    Overmaal = 100,
                    LuftMellemElementer = 15,
                    Aktiv = true,
                    OprettetDato = new DateTime(2024, 11, 1)
                }
            );

            // Standard rotationsregel
            modelBuilder.Entity<Rotations_Regel>().HasData(
                new Rotations_Regel
                {
                    Id = 1,
                    RegelNavn = "Standard rotationsregel",
                    TilladVendeOpTilMaksVaegt = 70m,
                    HoejdeBreddefaktor = 0.3m,
                    HoejdeBreddefaktorKunEnkelt = true,
                    Aktiv = true,
                    OprettetDato = new DateTime(2024, 11, 1)
                }
            );

            // Standard mellemrumsregel
            modelBuilder.Entity<Mellemrum_Regel>().HasData(
                new Mellemrum_Regel
                {
                    Id = 1,
                    RegelNavn = "Standard mellemrumsregel",
                    LuftMellemElementer = 10,
                    TillaegEndeplate = 50,
                    Overmaal = 50,
                    Aktiv = true,
                    OprettetDato = new DateTime(2024, 11, 1)
                }
            );

            // Standard stablingsregel
            modelBuilder.Entity<Stablings_Regel>().HasData(
                new Stablings_Regel
                {
                    Id = 1,
                    RegelNavn = "Standard stablingsregel",
                    MaksLag = 3,
                    TilladStablingOpTilMaksHoejde = 1500,
                    TilladStablingOpTilMaksVaegt = 70m,
                    Aktiv = true,
                    OprettetDato = new DateTime(2024, 11, 1)
                }
            );

            // Standard admin bruger (HUSK at ændre password!)
            modelBuilder.Entity<Bruger>().HasData(
                new Bruger
                {
                    Id = 1,
                    Brugernavn = "admin",
                    Email = "admin@palleoptimering.dk",
                    PasswordHash = "BMM", 
                    Fornavn = "Admin",
                    Efternavn = "Bruger",
                    Rolle = "Administrator",
                    Aktiv = true,
                    OprettetDato = new DateTime(2024, 11, 1)
                }
            );
        }
    }
}
