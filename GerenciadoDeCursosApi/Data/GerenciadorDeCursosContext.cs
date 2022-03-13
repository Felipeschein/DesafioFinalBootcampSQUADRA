using GerenciadoDeCursosApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadoDeCursosApi.Data
{
    public class GerenciadorDeCursosContext : DbContext
    {
        public GerenciadorDeCursosContext(DbContextOptions<GerenciadorDeCursosContext> options) : base (options)
        {

        }

        public DbSet<CursosModel> CursosModels { get; set; }
        public DbSet<UsuariosModel> UsuariosModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CursosModel>(e => 
            {
                e.ToTable("Cursos");
                e.HasKey(p => p.Id);
                e.Property(p => p.Titulo).HasColumnType("varchar(80)").IsRequired();
                e.Property(p => p.Duracao).HasColumnType("float").IsRequired();
                e.Property(p => p.Status).HasColumnType("varchar(20)").IsRequired();

            });

            modelBuilder.Entity<UsuariosModel>(e => {
                e.ToTable("Usuarios");
                e.HasKey(p => p.Id);
                e.Property(p => p.Login).HasColumnType("varchar(40)").IsRequired();
                e.Property(p => p.Senha).HasColumnType("varchar(20)").IsRequired();
                e.Property(p => p.Role).HasColumnType("varchar(20)").IsRequired();
            });
        }
    }
}
