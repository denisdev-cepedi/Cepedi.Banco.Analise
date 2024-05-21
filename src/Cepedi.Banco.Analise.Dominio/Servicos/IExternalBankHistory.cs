using Cepedi.Banco.Analise.Compartilhado.Dtos;
using Refit;
namespace Cepedi.Banco.Analise.Dominio.Servicos;

    public interface IExternalBankHistory
    {
        [Get("/api/random/{cpf}")]
        Task<List<HistoricoTransacaoDto>> GetExternalBankHistoryAsync(string cpf);
    }


