using Microsoft.EntityFrameworkCore;

namespace Pizzaapp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaSkladniki> PizzaSkladnikis { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Skladniki> Skladnikis { get; set; }
        public DbSet<Zamowienie> Zamowienia { get; set; }
        public DbSet<PizzaZamowienie> PizzaZamowienia { get; set; }
        public DbSet<ZamowienieSkladniki> ZamowienieSkladnikis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Klient>().ToTable("Klient");
            modelBuilder.Entity<Pizza>().ToTable("Pizza");
            modelBuilder.Entity<PizzaSkladniki>().ToTable("PizzaSkladniki");
            modelBuilder.Entity<Pracownik>().ToTable("Pracownik");
            modelBuilder.Entity<Skladniki>().ToTable("Skladniki");
            modelBuilder.Entity<Zamowienie>().ToTable("Zamowienie");
            modelBuilder.Entity<PizzaZamowienie>().ToTable("PizzaZamowienie");
            modelBuilder.Entity<ZamowienieSkladniki>().ToTable("ZamowienieSkladniki");
            // PizzaSkladniki - kompozytowy klucz główny
            modelBuilder.Entity<PizzaSkladniki>()
                .HasKey(ps => new { ps.id_pizza, ps.id_skladniki });

            // PizzaZamowienie - kompozytowy klucz główny
            modelBuilder.Entity<PizzaZamowienie>()
                .HasKey(pz => new { pz.IdPizzaZamowienie });

            // ZamowienieSkladniki - kompozytowy klucz główny
            modelBuilder.Entity<ZamowienieSkladniki>()
                .HasKey(zs => new { zs.id_zamowienie, zs.id_skladniki });

            // Relacje one-to-many Klient-Zamowienie
            modelBuilder.Entity<Klient>()
                .HasMany(k => k.Zamowienies)
                .WithOne(z => z.Klient)
                .HasForeignKey(z => z.id_klient);

            // Relacje one-to-many Pracownik-Zamowienie (kucharz i dostawca)
            modelBuilder.Entity<Pracownik>()
         .HasMany(p => p.Zamowienia) // zmienione z ZamowieniaKucharz i ZamowieniaDostawca na Zamowienia
         .WithOne(z => z.Pracownik)
         .HasForeignKey(z => z.id_pracownik)
         .OnDelete(DeleteBehavior.Restrict); // Aby uniknąć kaskadowego usuwania

           

            // Relacje many-to-many Pizza-PizzaSkladniki-Skladniki
            modelBuilder.Entity<Pizza>()
                .HasMany(p => p.PizzaSkladnikis)
                .WithOne(ps => ps.Pizza)
                .HasForeignKey(ps => ps.id_pizza);

            modelBuilder.Entity<Skladniki>()
                .HasMany(s => s.PizzaSkladnikis)
                .WithOne(ps => ps.Skladniki)
                .HasForeignKey(ps => ps.id_skladniki);

            // Relacje one-to-many Pizza-PizzaZamowienie
            modelBuilder.Entity<Pizza>()
                .HasMany(p => p.PizzaZamowienies)
                .WithOne(pz => pz.Pizza)
                .HasForeignKey(pz => pz.IdPizza);

            // Relacje one-to-many Zamowienie-PizzaZamowienie
            modelBuilder.Entity<Zamowienie>()
                .HasMany(z => z.PizzaZamowienie)
                .WithOne(pz => pz.Zamowienie)
                .HasForeignKey(pz => pz.IdZamowienie);

            // Relacje one-to-many Zamowienie-ZamowienieSkladniki
            modelBuilder.Entity<Zamowienie>()
                .HasMany(z => z.ZamowienieSkladniki)
                .WithOne(zs => zs.Zamowienie)
                .HasForeignKey(zs => zs.id_zamowienie);

            // Relacje one-to-one-or-zero ZamowienieSkladniki-PizzaZamowienie (opcjonalnie)
            modelBuilder.Entity<ZamowienieSkladniki>()
                .HasOne(zs => zs.PizzaZamowienie)
                .WithMany(pz => pz.ZamowienieSkladniki)
                .HasForeignKey(zs => zs.id_pizza_zamowienie);
        }
    }
}
