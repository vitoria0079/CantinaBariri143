using CantinaBariri143.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CantinaBariri143.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Fornecedores> Fornecedores { get; set; }
        public DbSet<Alimentos> Alimentos { get; set; }
        public DbSet<Compras> Compras { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Vendas> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Clientes>().ToTable("Clientes");
            builder.Entity<Usuarios>().ToTable("Usuarios");
            builder.Entity<Fornecedores>().ToTable("Fornecedores");
            builder.Entity<Alimentos>().ToTable("Alimentos");
            builder.Entity<Compras>().ToTable("Compras");
            builder.Entity<Pedidos>().ToTable("Pedidos");
            builder.Entity<Vendas>().ToTable("Vendas");
        }


    }
}