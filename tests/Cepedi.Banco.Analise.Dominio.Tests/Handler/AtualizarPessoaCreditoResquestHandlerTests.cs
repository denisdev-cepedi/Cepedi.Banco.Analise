using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Banco.Analise.Dominio.Tests.Handler;
public class AtualizarPessoaCreditoRequestHandlerTests
{
    private readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    private readonly ILogger<AtualizarPessoaCreditoResquestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly AtualizarPessoaCreditoResquestHandler _handler;

    public AtualizarPessoaCreditoRequestHandlerTests()
    {
        _pessoaCreditoRepository = Substitute.For<IPessoaCreditoRepository>();
        _logger = Substitute.For<ILogger<AtualizarPessoaCreditoResquestHandler>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new AtualizarPessoaCreditoResquestHandler(_pessoaCreditoRepository, _logger, _unitOfWork);
    }



    [Fact]
    public async Task Handle_PessoaCreditoNaoEncontrada_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarPessoaCreditoRequest { Cpf = "12345678900", LimiteCredito = 2000, CartaoCredito = true, ChequeEspecial = true };
        _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Cpf).Returns((PessoaCreditoEntity)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.IsType<SemResultadosExcecao>(result.Exception);
        _logger.Received().LogError("Erro ao buscar pessoa credito");
        await _unitOfWork.DidNotReceive().SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_ErroAoAtualizarPessoaCredito_DeveRetornarErro()
    {
        // Arrange
        var request = new AtualizarPessoaCreditoRequest { Cpf = "12345678900", LimiteCredito = 2000, CartaoCredito = true, ChequeEspecial = true };
        var pessoaEntity = new PessoaCreditoEntity { Cpf = request.Cpf, LimiteCredito = 1000, CartaoCredito = false, ChequeEspecial = false };

        _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Cpf).Returns(pessoaEntity);
        _pessoaCreditoRepository.AtualizarPessoaCreditoAsync(pessoaEntity).Returns((PessoaCreditoEntity)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.IsType<Compartilhado.Excecoes.ExcecaoAplicacao>(result.Exception);
        _logger.Received().LogError("Erro ao atualizar pessoa credito");
        await _unitOfWork.DidNotReceive().SaveChangesAsync(CancellationToken.None);
    }
}
