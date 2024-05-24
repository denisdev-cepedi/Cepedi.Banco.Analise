using Cepedi.Banco.Analise.Compartilhado.Dtos;
using Cepedi.Banco.Analise.Compartilhado.Enums;
using Cepedi.Banco.Analise.Compartilhado.Requests;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Handlers;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using Cepedi.Banco.Analise.Dominio.Servicos;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Banco.Analise.Dominio.Tests.Handler;
public class CriarPessoaCreditoRequestHandlerTests
{
    private readonly IPessoaCreditoRepository _pessoaCreditoRepository = Substitute.For<IPessoaCreditoRepository>();
    private readonly ILogger<CriarPessoaCreditoRequestHandler> _logger = Substitute.For<ILogger<CriarPessoaCreditoRequestHandler>>();
    private readonly IExternalBankHistory _externalBankHistory = Substitute.For<IExternalBankHistory>();
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly CriarPessoaCreditoRequestHandler _sut;

    public CriarPessoaCreditoRequestHandlerTests()
    {
        _sut = new CriarPessoaCreditoRequestHandler(_pessoaCreditoRepository, _logger, _externalBankHistory, _unitOfWork);
    }

    [Fact]
    public async Task Handle_QuandoHistoricoNaoEncontrado_DeveRetornarErro()
    {
        // Arrange
        var request = new CriarPessoaCreditoRequest { Cpf = "123456789", LimiteCredito = 5000, CartaoCredito = true, ChequeEspecial = true };
        _externalBankHistory.GetExternalBankHistoryAsync(request.Cpf).Returns((List<HistoricoTransacaoDto>)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<Compartilhado.Excecoes.ExcecaoAplicacao>();
        result.Exception.Message.Should().Be(PessoaCreditoErros.BuscaHistoricoNaoEncontrado.Descricao);
    }

    [Fact]
    public async Task Handle_QuandoCriarPessoaCredito_DeveRetornarSucesso()
    {
        // Arrange
        var request = new CriarPessoaCreditoRequest { Cpf = "123456789", LimiteCredito = 5000, CartaoCredito = true, ChequeEspecial = true };
        var historicoTransacao = new List<HistoricoTransacaoDto>
        {
            new HistoricoTransacaoDto { CpfRemetente = "123456789", ValorTransacao = 1000 },
            new HistoricoTransacaoDto { CpfDestinario = "123456789", ValorTransacao = 3000 }
        };
        _externalBankHistory.GetExternalBankHistoryAsync(request.Cpf).Returns(historicoTransacao);
        _pessoaCreditoRepository.CriarPessoaCreditoAsync(Arg.Any<PessoaCreditoEntity>()).Returns(new PessoaCreditoEntity { Cpf = request.Cpf, LimiteCredito = request.LimiteCredito, CartaoCredito = request.CartaoCredito, ChequeEspecial = request.ChequeEspecial, Score = 70 });

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.cpf.Should().Be(request.Cpf);
        result.Value.limiteCredito.Should().Be(request.LimiteCredito);
        result.Value.cartaoCredito.Should().Be(request.CartaoCredito);
        result.Value.chequeEspecial.Should().Be(request.ChequeEspecial);
        result.Value.score.Should().Be(60);
        await _unitOfWork.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_QuandoErroAoCriarPessoaCredito_DeveRetornarErro()
    {
        // Arrange
        var request = new CriarPessoaCreditoRequest { Cpf = "123456789", LimiteCredito = 5000, CartaoCredito = true, ChequeEspecial = true };
        var historicoTransacao = new List<HistoricoTransacaoDto>
        {
            new HistoricoTransacaoDto { CpfRemetente = "123456789", ValorTransacao = 1000 },
            new HistoricoTransacaoDto { CpfDestinario = "123456789", ValorTransacao = 3000 }
        };
        _externalBankHistory.GetExternalBankHistoryAsync(request.Cpf).Returns(historicoTransacao);
        _pessoaCreditoRepository.CriarPessoaCreditoAsync(Arg.Any<PessoaCreditoEntity>()).Returns((PessoaCreditoEntity)null);

        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exception.Should().BeOfType<Compartilhado.Excecoes.ExcecaoAplicacao>();
        result.Exception.Message.Should().Be(PessoaCreditoErros.ErroGravacaoPessoaCredito.Descricao);
    }
}
