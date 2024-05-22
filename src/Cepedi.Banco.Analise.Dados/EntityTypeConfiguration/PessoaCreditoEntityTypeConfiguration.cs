using Cepedi.Banco.Analise.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Banco.Analise.Dados.EntityTypeConfiguration;
public class PessoaCreditoEntityTypeConfiguration : IEntityTypeConfiguration<PessoaCreditoEntity>
{
    public void Configure(EntityTypeBuilder<PessoaCreditoEntity> builder)
    {
        builder.ToTable("PessoaCredito");
        builder.HasKey(p => p.Id); // Define a chave primária

        builder.Property(p=> p.Cpf).IsRequired().HasMaxLength(11);
        builder.Property(p => p.CartaoCredito);
        builder.Property(p => p.ChequeEspecial);
        builder.Property(p => p.LimiteCredito);
        builder.Property(p => p.Score);
        builder.HasIndex(p => p.Cpf).IsUnique(); // Define o índice único
    }
}
