using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;

namespace Cepedi.Banco.Analise.Dominio;

public class ObterPessoaCreditoRequestHandler : IRequestHandler<ObterPessoaCreditoRequest, Result<ObterPessoaCreditoResponse>>
{
    public readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    public readonly ILogger<ObterPessoaCreditoRequestHandler> _logger;
    public ObterPessoaCreditoRequestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<ObterPessoaCreditoRequestHandler> logger)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
    }
    public async Task<Result<ObterPessoaCreditoResponse>> Handle(ObterPessoaCreditoRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Id);
        
        if (pessoaEntity == null)
        {
            return Result.Error<ObterPessoaCreditoResponse>(new SemResultadosExcecao());
        }

        var pessoaCredito = new ObterPessoaCreditoResponse(pessoaEntity.Id, pessoaEntity.CartaoCredito, pessoaEntity.ChequeEspecial, pessoaEntity.LimiteCredito);

        return Result.Success(pessoaCredito);
    }
}
