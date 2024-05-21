using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Analise.Dominio;

public class DeletarPessoaCreditoRequestHandler : IRequestHandler<DeletarPessoaCreditoRequest, Result<DeletarPessoaCreditoResponse>>
{
    private readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    private readonly ILogger<DeletarPessoaCreditoRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeletarPessoaCreditoRequestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<DeletarPessoaCreditoRequestHandler> logger, IUnitOfWork unitOfWork)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DeletarPessoaCreditoResponse>> Handle(DeletarPessoaCreditoRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Cpf);

        if (pessoaEntity == null)
        {
            return Result.Error<DeletarPessoaCreditoResponse>(new SemResultadosExcecao());
        }

        await _pessoaCreditoRepository.DeletarPessoaCreditoAsync(pessoaEntity.Cpf);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new DeletarPessoaCreditoResponse(pessoaEntity.Cpf));
    }
}
