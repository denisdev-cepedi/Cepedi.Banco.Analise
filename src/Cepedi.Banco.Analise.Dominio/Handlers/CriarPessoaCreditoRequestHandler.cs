using Cepedi.Banco.Analise.Compartilhado.Requests;
using Cepedi.Banco.Analise.Compartilhado.Responses;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Analise.Dominio.Handlers;
public class CriarPessoaCretidoRequestHandler : IRequestHandler<CriarPessoaCreditoRequest, Result<CriarPessoaCreditoResponse>>
{
    public readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    public readonly ILogger<CriarPessoaCretidoRequestHandler> _logger;

    public CriarPessoaCretidoRequestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<CriarPessoaCretidoRequestHandler> logger)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
    }


    public async Task<Result<CriarPessoaCreditoResponse>> Handle(CriarPessoaCreditoRequest request, CancellationToken cancellationToken)
    {

        var PessoaEntity = _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Cpf); // Consultar repository do banco para obter pessoa (falta implementar)

        var pessoaCredito = new PessoaCreditoEntity
        {
            Cpf = request.Cpf,
            LimiteCredito = request.LimiteCredito,
            CartaoCredito = request.CartaoCredito,
            ChequeEspecial = request.ChequeEspecial,
            Score = 0 // implementar método com regra de negócio para calcular score

        };

        await _pessoaCreditoRepository.CriarPessoaCreditoAsync(pessoaCredito);



        return Result.Success(new CriarPessoaCreditoResponse(pessoaCredito.Cpf, pessoaCredito.CartaoCredito, pessoaCredito.ChequeEspecial, pessoaCredito.LimiteCredito));
    }
}
