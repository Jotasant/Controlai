using Dominio.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestrutura.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Demanda> Demandas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Demanda>().ToTable("Demandas");
        }
    }
}
