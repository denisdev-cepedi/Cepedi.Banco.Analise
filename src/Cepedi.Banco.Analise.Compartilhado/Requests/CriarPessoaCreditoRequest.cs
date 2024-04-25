using Cepedi.Banco.Analise.Compartilhado.Responses;
using MediatR;
using OperationResult;

namespace Cepedi.Banco.Analise.Compartilhado.Requests;
public class CriarPessoaCreditoRequest : IRequest<Result<CriarPessoaCreditoResponse>>
{
    public string Cpf { get; set; } = default!;
    public bool CartaoCredito { get; set; }
    public bool ChequeEspecial { get; set; }
    public double LimiteCredito { get; set; } = 0;
}
