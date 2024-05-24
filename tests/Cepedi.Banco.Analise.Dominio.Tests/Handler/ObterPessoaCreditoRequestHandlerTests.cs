using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Banco.Analise.Dominio.Tests;

public class ObterPessoaCreditoRequestHandlerTests
{
    private readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    private readonly ILogger<ObterPessoaCreditoRequestHandler> _logger;
    private readonly ICache<PessoaCreditoEntity> _cache;
    private readonly ObterPessoaCreditoRequestHandler _handler;

    public ObterPessoaCreditoRequestHandlerTests()
    {
        _pessoaCreditoRepository = Substitute.For<IPessoaCreditoRepository>();
        _logger = Substitute.For<ILogger<ObterPessoaCreditoRequestHandler>>();
        _cache = Substitute.For<ICache<PessoaCreditoEntity>>();
        _handler = new ObterPessoaCreditoRequestHandler(_pessoaCreditoRepository, _logger, _cache);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPessoaFoundInCache()
    {
        // Arrange
        var cpf = "12345678900";
        var pessoaEntity = new PessoaCreditoEntity { Cpf = cpf, CartaoCredito = true, ChequeEspecial = false, LimiteCredito = 1500, Score = 700 };
        var request = new ObterPessoaCreditoRequest { Cpf = cpf };
        _cache.ObterAsync(cpf).Returns(Task.FromResult(pessoaEntity));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        var response = result.Value;
        Assert.Equal(cpf, response.cpf);
        Assert.Equal(true, response.cartaoCredito);
        Assert.Equal(false, response.chequeEspecial);
        Assert.Equal(1500, response.limiteCredito);
        Assert.Equal(700, response.score);
    }


}
