﻿using Cepedi.Banco.Analise.Benchmark.Test.Helpers;

namespace Cepedi.Banco.Analise.Benchmark.Test;
public class DapperVsEfCoreBenchmark
{
    private readonly string _connectionString;
    private readonly int iterations = 10;
    
    public DapperVsEfCoreBenchmark()
    {
        var config = new AppConfigurations();
        _connectionString = config.ConnectionString;
    }

    //[Benchmark]
    //public async Task DapperQueryAsync()
    //{
    //    using (var connection = new SqlConnection(_connectionString))
    //    {
    //        connection.Open();
    //        for (int i = 0; i < iterations; i++)
    //        {
    //            var result = await connection.QueryAsync<UsuarioEntity>("SELECT * FROM USUARIO WITH(NOLOCK) WHERE Id = 1");
    //        }
    //    }
    //}

    //[Benchmark]
    //public async Task EfCoreQuery()
    //{
    //    using (var dbContext = new CepediDbContext(_connectionString))
    //    {
    //        for (int i = 0; i < iterations; i++)
    //        {
    //            var result = await dbContext.Usuario.Where(x => x.Id == 1).FirstOrDefaultAsync();
    //        }
    //    }
    //}
}
