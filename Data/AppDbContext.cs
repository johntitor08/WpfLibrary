using Microsoft.EntityFrameworkCore;
using WpfLibrary.Models;
using System.IO;

namespace WpfLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "library.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // İstersen kurallar yazabilirsin
            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
