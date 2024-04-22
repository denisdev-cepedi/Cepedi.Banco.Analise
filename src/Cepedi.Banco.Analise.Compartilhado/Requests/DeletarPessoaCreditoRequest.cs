using MediatR;
using OperationResult;

namespace Cepedi.Banco.Analise.Compartilhado;

public class DeletarPessoaCreditoRequest : IRequest<Result<DeletarPessoaCreditoResponse>>
{
    public int Id { get; set; }
    
}
