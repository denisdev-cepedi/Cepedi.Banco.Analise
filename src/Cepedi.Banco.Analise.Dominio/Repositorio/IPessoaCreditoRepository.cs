using Cepedi.Banco.Analise.Compartilhado.Dtos;
using Cepedi.Banco.Analise.Dominio.Entidades;

namespace Cepedi.Banco.Analise.Dominio.Repositorio;
public interface IPessoaCreditoRepository
{
    Task<PessoaCreditoEntity> CriarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito);
    Task<PessoaCreditoEntity> ObterPessoaCreditoAsync(string cpf);
    Task<PessoaCreditoEntity> AtualizarPessoaCreditoAsync(PessoaCreditoEntity pessoaCredito);
    Task<PessoaCreditoEntity> DeletarPessoaCreditoAsync(string cpf);

}
