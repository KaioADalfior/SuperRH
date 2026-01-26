using Microsoft.EntityFrameworkCore;
using SuperRH.Models;

namespace SuperRH.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Historico> Historicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Nomes das tabelas
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Colaborador>().ToTable("Colaboradores");
            modelBuilder.Entity<Cargo>().ToTable("Cargos");
            modelBuilder.Entity<Historico>().ToTable("Historico");

            // 🔗 Relacionamento Usuario -> Colaborador
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Colaborador)
                .WithMany()
                .HasForeignKey(u => u.idColaborador)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
