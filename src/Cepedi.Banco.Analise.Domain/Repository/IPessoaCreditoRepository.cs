using Cepedi.Banco.Analise.Domain.Entities;

namespace Cepedi.Banco.Analise.Domain.Repository;
public interface IPessoaCreditoRepository
{
    Task<PessoaCreditoEntity> CriarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito);
    Task<PessoaCreditoEntity> ObterPessoaCreditoAsync(int id);
    Task<PessoaCreditoEntity> AtualizarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito);
}
