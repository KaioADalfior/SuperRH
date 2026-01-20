using Microsoft.EntityFrameworkCore;
using SuperRH.Models;

namespace SuperRH.Data // <-- Verifique se este nome está IGUAL ao do Program.cs
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(u => u.idUsuario);
        }
    }
}