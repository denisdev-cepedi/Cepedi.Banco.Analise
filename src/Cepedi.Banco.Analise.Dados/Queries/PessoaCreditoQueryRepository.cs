using Cepedi.Banco.Analise.Dados.Repositorios.Queries;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Repositorio.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Cepedi.Banco.Analise.Dados.Queries;
public class PessoaCreditoQueryRepository : BaseDapperRepository, IPessoaCreditoQueryRepository
{
    public PessoaCreditoQueryRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<List<PessoaCreditoEntity>> ObterPessoaAsync(string Cpf)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@Cpf", Cpf, System.Data.DbType.String);

        var query = @"SELECT 
                        Id, 
                        Score,
                        Cpf
                    FROM PessoaCredito WITH(NOLOCK)
                    Where
                        Cpf = @Cpf";

        return (await ExecuteQueryAsync<PessoaCreditoEntity>(query, parametros)).ToList();
    }

    public async Task<PessoaCreditoEntity?> AtualizarLimiteCreditoDapperAsync(PessoaCreditoEntity pessoa)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@LimiteCredito", pessoa.LimiteCredito, System.Data.DbType.String);
        parametros.Add("@Cpf", pessoa.Cpf, System.Data.DbType.String);

        var sql = @"UPDATE Pessoa
                    SET
                        LimiteCredito = @LimiteCredito,
                    WHERE
                        Cpf = @Cpf";

        var retorno = await ExecuteQueryAsync<PessoaCreditoEntity>(sql, parametros);

        return retorno.FirstOrDefault();
    }
}

