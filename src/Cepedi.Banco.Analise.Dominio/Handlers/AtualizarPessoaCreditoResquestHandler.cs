using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Enums;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
using Cepedi.Banco.Analise.Compartilhado.Responses;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Analise.Dominio;

public class AtualizarPessoaCreditoResquestHandler : IRequestHandler<AtualizarPessoaCreditoRequest, Result<AtualizarPessoaCreditoResponse>>
{
    private readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    private readonly ILogger<AtualizarPessoaCreditoResquestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public AtualizarPessoaCreditoResquestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<AtualizarPessoaCreditoResquestHandler> logger, IUnitOfWork unitOfWork)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<AtualizarPessoaCreditoResponse>> Handle(AtualizarPessoaCreditoRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Cpf);
        if (pessoaEntity == null)
        {
            _logger.LogError("Erro ao buscar pessoa credito");
            return Result.Error<AtualizarPessoaCreditoResponse>(new SemResultadosExcecao());
        }


        pessoaEntity.Atualizar(request.CartaoCredito, request.ChequeEspecial, request.LimiteCredito);

        var response = await _pessoaCreditoRepository.AtualizarPessoaCreditoAsync(pessoaEntity);

        if (response == null)
        {
            _logger.LogError("Erro ao atualizar pessoa credito");
            return Result.Error<AtualizarPessoaCreditoResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(PessoaCreditoErros.ErroGravacaoPessoaCredito));
        }
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var pessoaCredito = new AtualizarPessoaCreditoResponse(pessoaEntity.Cpf, pessoaEntity.CartaoCredito, pessoaEntity.ChequeEspecial, pessoaEntity.LimiteCredito);

        return Result.Success(pessoaCredito);
    }
}
