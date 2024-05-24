using Cepedi.Banco.Analise.Api.Controllers;
using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Requests;
using Cepedi.Banco.Analise.Compartilhado.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OperationResult;

namespace Cepedi.Banco.Analise.Api.Tests;

public class PessoaCreditoControllerTests
{
    private readonly IMediator _mediator = Substitute.For<IMediator>();
    private readonly ILogger<PessoaCreditoController> _logger = Substitute.For<ILogger<PessoaCreditoController>>();
    private readonly PessoaCreditoController _sut;

    public PessoaCreditoControllerTests()
    {
        _sut = new PessoaCreditoController(_logger, _mediator);
    }

    [Fact]
    public async Task CriarPagamento_DeveEnviarRequest_Para_Mediator()
    {
        // Arrange
        var request = new CriarPessoaCreditoRequest { Cpf = "123456789", LimiteCredito = 100 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new CriarPessoaCreditoResponse("123456789", true, true, 100, 50)));

        // Act
        await _sut.CriarPagamentoAsync(request);

        // Assert
        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task AtualizarPagamento_DeveEnviarRequest_Para_Mediator()
    {
        // Arrange
        var request = new AtualizarPessoaCreditoRequest { Cpf = "123456789", LimiteCredito = 200 };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new AtualizarPessoaCreditoResponse("123423489", true, true, 200)));

        // Act
        await _sut.AtualizarPagamentoAsync(request);

        // Assert
        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task ObterPessoaCredito_DeveEnviarRequest_Para_Mediator()
    {
        // Arrange
        var request = new ObterPessoaCreditoRequest { Cpf = "123456789" };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new ObterPessoaCreditoResponse("123456789", true, true, 100, 50)));

        // Act
        await _sut.ObterPessoaCreditoRequestAsync(request);

        // Assert
        await _mediator.ReceivedWithAnyArgs().Send(request);
    }

    [Fact]
    public async Task DeletarPagamento_DeveEnviarRequest_Para_Mediator()
    {
        // Arrange
        var request = new DeletarPessoaCreditoRequest { Cpf = "123456789" };
        _mediator.Send(request).ReturnsForAnyArgs(Result.Success(new DeletarPessoaCreditoResponse("123456789")));

        // Act
        await _sut.DeletarPagamentoAsync(request);

        // Assert
        await _mediator.ReceivedWithAnyArgs().Send(request);
    }
}
