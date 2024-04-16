using Cepedi.Banco.Analise.Compartilhado.Responses;
using MediatR;

namespace Cepedi.Banco.Analise.Compartilhado.Requests;
public class CriarClienteCreditoRequest : IRequest<CriarClienteCreditoResponse>
{
    public int PessoaId { get; set; }
    public bool CartaoCredito { get; set; }
    public bool ChequeEspecial { get; set; }
    public double LimiteCredito { get; set; }
}
