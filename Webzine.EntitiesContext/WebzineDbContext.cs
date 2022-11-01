using Microsoft.EntityFrameworkCore;
using Webzine.Entity;

namespace Webzine.EntitiesContext
{
    /// <summary>
    /// DBContext de notre solution
    /// </summary>
    public class WebzineDbContext : DbContext
    {
        public DbSet<Titre> Titres { get; set; }
        public DbSet<Artiste> Artistes { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<LienStyle> LienStyles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuration du Provider
            optionsBuilder.UseSqlite(@"Data Source=WebzineDatabase.db");
            //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Webzine;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<LienStyle>().HasKey(c => new { c.IdStyle, c.IdTitre });
            modelBuilder.Entity<LienStyle>().HasOne(p => p.Style).WithMany(p => p.LienStyle).HasForeignKey(p => p.IdStyle);
            modelBuilder.Entity<LienStyle>().HasOne(p => p.Titre).WithMany(p => p.LienStyle).HasForeignKey(p => p.IdTitre);

        }
    }
}
