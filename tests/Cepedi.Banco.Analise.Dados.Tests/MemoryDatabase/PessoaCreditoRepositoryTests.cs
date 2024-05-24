using Cepedi.Banco.Analise.Dados.Repositories;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Cepedi.Banco.Analise.Dados.Tests;

public class PessoaCreditoRepositoryTest
{
    [Fact]
    public async Task Can_Create_PessoaCredito()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var pessoaCreditoRepository = new PessoaCreditoRepository(context);
            var pessoaCredito = new PessoaCreditoEntity { Id = 2, LimiteCredito = 100 };

            var result = await pessoaCreditoRepository.CriarPessoaCreditoAsync(pessoaCredito);

            Assert.Equal(2, result.Id);
            Assert.Equal(100, result.LimiteCredito);
        }
    }

    [Fact]
    public async Task Can_Update_PessoaCredito()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.PessoaCredito.Add(new PessoaCreditoEntity { Id = 1, Cpf = "12345678900", LimiteCredito = 100 });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var pessoaCreditoRepository = new PessoaCreditoRepository(context);
            var pessoaCredito = new PessoaCreditoEntity { Id = 1, Cpf = "12345678900", LimiteCredito = 200 };

            var result = await pessoaCreditoRepository.AtualizarPessoaCreditoAsync(pessoaCredito);

            Assert.Equal(1, result.Id);
            Assert.Equal(200, result.LimiteCredito);
        }
    }


    [Fact]
    public async Task Can_Get_PessoaCredito_By_Cpf()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            context.PessoaCredito.Add(new PessoaCreditoEntity { Id = 3, LimiteCredito = 100, Cpf = "123456789" });
            context.SaveChanges();
        }

        using (var context = new ApplicationDbContext(options))
        {
            var pessoaCreditoRepository = new PessoaCreditoRepository(context);
            var result = await pessoaCreditoRepository.ObterPessoaCreditoAsync("123456789");

            Assert.NotNull(result);
            Assert.Equal(3, result.Id);
            Assert.Equal(100, result.LimiteCredito);
        }
    }
}
