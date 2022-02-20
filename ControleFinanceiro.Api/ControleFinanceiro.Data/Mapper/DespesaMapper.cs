﻿using ControleFinanceiro.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data.Mapper
{
    public class DespesaMapper : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.HasKey(d => d.DespesaId);

            builder.Property(d => d.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.Valor)
                .IsRequired();

            builder.Property(d => d.Dia)
                .IsRequired();

            builder.Property(d => d.Ano)
                .IsRequired();

            builder.HasOne(d => d.Cartao)
                .WithMany(d => d.Despesas)
                .HasForeignKey(d => d.CartaoId)
                .IsRequired();

            builder.HasOne(d => d.Categoria)
                .WithMany(d => d.Despesas)
                .HasForeignKey(d => d.CategoriaId)
                .IsRequired();

            builder.HasOne(d => d.Mes)
                .WithMany(d => d.Despesas)
                .HasForeignKey(d => d.MesId)
                .IsRequired();

            builder.HasOne(d => d.Usuario)
                .WithMany(d => d.Despesas)
                .HasForeignKey(d => d.UsuarioId)
                .IsRequired();

            builder.ToTable("Despesas");
        }
    }
}
