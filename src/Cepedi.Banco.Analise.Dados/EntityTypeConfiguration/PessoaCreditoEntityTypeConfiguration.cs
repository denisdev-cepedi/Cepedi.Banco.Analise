using Cepedi.Banco.Analise.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cepedi.Banco.Analise.Dados.EntityTypeConfiguration;
public class PessoaCreditoEntityTypeConfiguration : IEntityTypeConfiguration<PessoaCreditoEntity>
{
    public void Configure(EntityTypeBuilder<PessoaCreditoEntity> builder)
    {
        builder.ToTable("PessoaCredito");
        builder.HasKey(c => c.Id); // Define a chave primária

        builder.Property(c => c.CartaoCredito);
        builder.Property(c => c.ChequeEspecial);
        builder.Property(c => c.LimiteCredito);
        builder.Property(c => c.Score);

        builder.Property(c => c.PessoaId);
        // builder.HasOne(c => c.Pessoa)
        //     .WithOne()
        //     .HasForeignKey<PessoaCreditoEntity>(c => c.PessoaId); falta implementar
    }
}
