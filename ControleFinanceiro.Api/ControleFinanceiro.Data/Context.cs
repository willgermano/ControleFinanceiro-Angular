using ControleFinanceiro.Data.Mapper;
using ControleFinanceiro.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data
{
    public class Context : IdentityDbContext<Usuario, Funcao, string>
    {
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<Ganho> Ganhos { get; set; }
        public DbSet<Mes> Meses { get; set; }
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public Context(DbContextOptions<Context> opcoes) : base(opcoes) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new CartaoMapper());
            builder.ApplyConfiguration(new CategoriaMapper());
            builder.ApplyConfiguration(new DespesaMapper());
            builder.ApplyConfiguration(new FuncaoMapper());
            builder.ApplyConfiguration(new GanhoMapper());
            builder.ApplyConfiguration(new MesMapper());
            builder.ApplyConfiguration(new TipoMapper());
            builder.ApplyConfiguration(new UsuarioMapper());
        }
    }
}
