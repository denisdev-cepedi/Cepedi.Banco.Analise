using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Cepedi.Banco.Analise.Dominio.Tests.Handler;
public class DeletarPessoaCreditoRequestHandlerTests
{
    private readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    private readonly ILogger<DeletarPessoaCreditoRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DeletarPessoaCreditoRequestHandler _handler;

    public DeletarPessoaCreditoRequestHandlerTests()
    {
        _pessoaCreditoRepository = Substitute.For<IPessoaCreditoRepository>();
        _logger = Substitute.For<ILogger<DeletarPessoaCreditoRequestHandler>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DeletarPessoaCreditoRequestHandler(_pessoaCreditoRepository, _logger, _unitOfWork);
    }


    [Fact]
    public async Task Handle_PessoaCreditoNaoEncontrada_DeveRetornarErro()
    {
        // Arrange
        var request = new DeletarPessoaCreditoRequest { Cpf = "12345678900" };
        _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Cpf).Returns((PessoaCreditoEntity)null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.IsType<SemResultadosExcecao>(result.Exception);
        _logger.Received().LogError("Erro ao buscar pessoa credito");
        await _unitOfWork.DidNotReceive().SaveChangesAsync(CancellationToken.None);
    }
}

