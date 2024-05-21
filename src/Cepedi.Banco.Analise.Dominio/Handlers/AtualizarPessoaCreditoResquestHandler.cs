using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
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
            return Result.Error<AtualizarPessoaCreditoResponse>(new SemResultadosExcecao());
        }

        pessoaEntity.Atualizar(request.CartaoCredito, request.ChequeEspecial, request.LimiteCredito);

        await _pessoaCreditoRepository.AtualizarPessoaCreditoAsync(pessoaEntity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var pessoaCredito = new AtualizarPessoaCreditoResponse(pessoaEntity.Cpf, pessoaEntity.CartaoCredito, pessoaEntity.ChequeEspecial, pessoaEntity.LimiteCredito);

        return Result.Success(pessoaCredito);
    }
}
