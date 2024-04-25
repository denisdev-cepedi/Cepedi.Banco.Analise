using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Analise.Dominio;

public class AtualizarPessoaCreditoResquestHandler : IRequestHandler<AtualizarPessoaCreditoRequest, Result<AtualizarPessoaCreditoResponse>>
{
    public readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    public readonly ILogger<AtualizarPessoaCreditoResquestHandler> _logger;
    public AtualizarPessoaCreditoResquestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<AtualizarPessoaCreditoResquestHandler> logger)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
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

        var pessoaCredito = new AtualizarPessoaCreditoResponse(pessoaEntity.Cpf, pessoaEntity.CartaoCredito, pessoaEntity.ChequeEspecial, pessoaEntity.LimiteCredito);

        return Result.Success(pessoaCredito);
    }
}
