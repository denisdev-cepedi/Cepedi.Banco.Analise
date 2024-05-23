using Cepedi.Banco.Analise.Dominio.Entidades;

namespace Cepedi.Banco.Analise.Dominio.Repositorio.Queries;
public interface IPessoaCreditoQueryRepository
{
    Task<List<PessoaCreditoEntity>> ObterPessoasAsync(string cpf);
}