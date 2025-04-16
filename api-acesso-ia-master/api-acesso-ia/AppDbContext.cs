using api_acesso_ia.Models;
using Microsoft.EntityFrameworkCore;

namespace api_acesso_ia
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LoginUsuario> LoginUsuarios { get; set; }
        public DbSet<Acesso> Acessos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "Server=localhost;Database=db_api_acesso_ia;User=root;Password=;",
                    new MySqlServerVersion(new Version(5, 7, 0)),
                    mysqlOptions =>
                    {
                        mysqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    }
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Acesso>()
                .HasOne(a => a.Usuario)
                .WithMany(u => u.Acessos)
                .HasForeignKey(a => a.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
