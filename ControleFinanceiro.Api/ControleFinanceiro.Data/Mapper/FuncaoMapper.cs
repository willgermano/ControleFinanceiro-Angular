using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data.Mapper
{
    public class FuncaoMapper : IEntityTypeConfiguration<Funcao>
    {
        public void Configure(EntityTypeBuilder<Funcao> builder)
        {
            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Property(f => f.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new Funcao
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                    Descricao = "Administrador do sistema"
                },
                new Funcao
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Usuario",
                    NormalizedName = "USUARIO",
                    Descricao = "Usuário do sistema"
                });

            builder.ToTable("Funcoes");
        }
    }
}
