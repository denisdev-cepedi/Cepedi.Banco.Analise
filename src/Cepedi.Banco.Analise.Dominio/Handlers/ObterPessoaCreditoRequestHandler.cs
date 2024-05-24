using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Enums;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Analise.Dominio;

public class ObterPessoaCreditoRequestHandler : IRequestHandler<ObterPessoaCreditoRequest, Result<ObterPessoaCreditoResponse>>
{
    private readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    private readonly ILogger<ObterPessoaCreditoRequestHandler> _logger;
    private readonly ICache<PessoaCreditoEntity> _cache;
    public ObterPessoaCreditoRequestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<ObterPessoaCreditoRequestHandler> logger, ICache<PessoaCreditoEntity> cache)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
        _cache = cache;
    }
    public async Task<Result<ObterPessoaCreditoResponse>> Handle(ObterPessoaCreditoRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _cache.ObterAsync(request.Cpf);
        if (pessoaEntity != null)
        {
            
            var pessoaCredito = new ObterPessoaCreditoResponse(pessoaEntity.Cpf, pessoaEntity.CartaoCredito, pessoaEntity.ChequeEspecial, pessoaEntity.LimiteCredito, pessoaEntity.Score);
            return Result.Success(pessoaCredito);
        }
        else
        {
            var pessoaEntityRepository = await _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Cpf);

            if (pessoaEntityRepository == null)
            {
                _logger.LogError("Erro ao buscar pessoa credito");
                return Result.Error<ObterPessoaCreditoResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(PessoaCreditoErros.SemResultados));
            }
            await _cache.SalvarAsync(request.Cpf, pessoaEntityRepository);
            var pessoaCredito = new ObterPessoaCreditoResponse(pessoaEntityRepository.Cpf, pessoaEntityRepository.CartaoCredito, pessoaEntityRepository.ChequeEspecial, pessoaEntityRepository.LimiteCredito, pessoaEntityRepository.Score);
            return Result.Success(pessoaCredito);
        }

    }


}

