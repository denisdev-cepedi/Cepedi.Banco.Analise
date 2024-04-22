using MediatR;
using OperationResult;

namespace Cepedi.Banco.Analise.Compartilhado;

public class AtualizarPessoaCreditoRequest : IRequest<Result<AtualizarPessoaCreditoResponse>>
{
    public int Id { get; set; }
    public bool CartaoCredito { get; set; }
    public bool ChequeEspecial { get; set; }
    public double LimiteCredito { get; set; }
}
