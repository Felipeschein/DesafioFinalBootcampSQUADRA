using GerenciadorDeCursosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeCursosApi.Data
{
    public class GerenciadorDeCursosContext : DbContext
    {
        public GerenciadorDeCursosContext(DbContextOptions<GerenciadorDeCursosContext> options) : base (options)
        {

        }

        public DbSet<CursoModel> CursosModels { get; set; }
        public DbSet<UsuarioModel> UsuariosModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CursoModel>(e => 
            {
                e.ToTable("Cursos");
                e.HasKey(p => p.Id);
                e.Property(p => p.Titulo).HasColumnType("varchar(80)").IsRequired();
                e.Property(p => p.Duracao).HasColumnType("float").IsRequired();
                e.Property(p => p.Status).HasColumnType("varchar(20)").IsRequired();

            });

            modelBuilder.Entity<UsuarioModel>(e => {
                e.ToTable("Usuarios");
                e.HasKey(p => p.Id);
                e.Property(p => p.Login).HasColumnType("varchar(40)").IsRequired();
                e.Property(p => p.Senha).HasColumnType("varchar(20)").IsRequired();
                e.Property(p => p.Role).HasColumnType("varchar(20)").IsRequired();
            });
        }
    }
}
