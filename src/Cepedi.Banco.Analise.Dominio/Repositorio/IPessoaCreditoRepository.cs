using Cepedi.Banco.Analise.Dominio.Entidades;

namespace Cepedi.Banco.Analise.Dominio.Repositorio;
public interface IPessoaCreditoRepository
{
    Task<PessoaCreditoEntity> CriarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito);
    Task<PessoaCreditoEntity> ObterPessoaCreditoAsync(int id);
    Task<PessoaCreditoEntity> AtualizarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito);
}
