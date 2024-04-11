using Cepedi.Banco.Analise.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Banco.Analise.Data.EntityTypeConfiguration;
public class PessoaCreditoEntityTypeConfiguration : IEntityTypeConfiguration<PessoaCreditoEntity>
{
    public void Configure(EntityTypeBuilder<PessoaCreditoEntity> builder)
    {
        builder.ToTable("PessoaCredito");
        builder.HasKey(c => c.PessoaId); // Define a chave primária

        builder.Property(c => c.CartaoCredito);
        builder.Property(c => c.ChequeEspecial);
        builder.Property(c => c.LimiteCredito);

    }
}
