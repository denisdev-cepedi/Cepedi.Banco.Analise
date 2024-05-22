using MediatR;
using OperationResult;

namespace Cepedi.Banco.Analise.Compartilhado;

public class ObterPessoaCreditoRequest : IRequest<Result<ObterPessoaCreditoResponse>>
{
    public string Cpf { get; set; } = default!;
}
