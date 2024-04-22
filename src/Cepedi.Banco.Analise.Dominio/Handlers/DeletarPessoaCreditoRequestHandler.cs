using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Analise.Dominio;

public class DeletarPessoaCreditoRequestHandler : IRequestHandler<DeletarPessoaCreditoRequest, Result<DeletarPessoaCreditoResponse>>
{
    public readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    public readonly ILogger<DeletarPessoaCreditoRequestHandler> _logger;
    public DeletarPessoaCreditoRequestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<DeletarPessoaCreditoRequestHandler> logger)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
    }

    public async Task<Result<DeletarPessoaCreditoResponse>> Handle(DeletarPessoaCreditoRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Id);

        if (pessoaEntity == null)
        {
            return Result.Error<DeletarPessoaCreditoResponse>(new SemResultadosExcecao());
        }

        await _pessoaCreditoRepository.DeletarPessoaCreditoAsync(pessoaEntity.Id);

        return Result.Success(new DeletarPessoaCreditoResponse(pessoaEntity.Id));
    }
}
