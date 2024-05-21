using Cepedi.Banco.Analise.Dominio.Entidades;

namespace Cepedi.Banco.Analise.Dominio.Repositorio.Queries;
public interface IPessoaCreditoQueryRepository
{
    Task<List<PessoaCreditoEntity>> ObterPessoaAsync(string cpf);
    Task<PessoaCreditoEntity?> AtualizarLimiteCreditoDapperAsync(PessoaCreditoEntity pessoa);
}
