using Cepedi.Banco.Analise.Compartilhado.Dtos;
using Cepedi.Banco.Analise.Compartilhado.Enums;
using Cepedi.Banco.Analise.Compartilhado.Requests;
using Cepedi.Banco.Analise.Compartilhado.Responses;
using Cepedi.Banco.Analise.Dominio.Entidades;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using Cepedi.Banco.Analise.Dominio.Servicos;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Analise.Dominio.Handlers;
public class CriarPessoaCreditoRequestHandler : IRequestHandler<CriarPessoaCreditoRequest, Result<CriarPessoaCreditoResponse>>
{
    public readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    public readonly ILogger<CriarPessoaCreditoRequestHandler> _logger;
    public readonly IExternalBankHistory _externalBankHistory;
    public readonly IUnitOfWork _unitOfWork;

    public CriarPessoaCreditoRequestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<CriarPessoaCreditoRequestHandler> logger, IExternalBankHistory externalBankHistory, IUnitOfWork unitOfWork)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
        _externalBankHistory = externalBankHistory;
        _unitOfWork = unitOfWork;
    }



    public async Task<Result<CriarPessoaCreditoResponse>> Handle(CriarPessoaCreditoRequest request, CancellationToken cancellationToken)
    {

        var historicoTransacaoDto = await _externalBankHistory.GetExternalBankHistoryAsync(request.Cpf); // Consultar repository do banco para obter historico de transações
        if (historicoTransacaoDto == null)
        {
            _logger.LogError("Erro ao buscar historico de transações");
            return Result.Error<CriarPessoaCreditoResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(PessoaCreditoErros.BuscaHistoricoNaoEncontrado));
        }

        var scoreCalc = CalcularScore(historicoTransacaoDto, request.Cpf);
        var pessoaCredito = new PessoaCreditoEntity
        {
            Cpf = request.Cpf,
            LimiteCredito = request.LimiteCredito,
            CartaoCredito = request.CartaoCredito,
            ChequeEspecial = request.ChequeEspecial,
            Score = scoreCalc
        };
        var response = await _pessoaCreditoRepository.CriarPessoaCreditoAsync(pessoaCredito);

        if (response == null)
        {
            _logger.LogError("Erro ao criar pessoa credito");
            return Result.Error<CriarPessoaCreditoResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(PessoaCreditoErros.ErroGravacaoPessoaCredito));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Usuario criado com sucesso");

        return Result.Success(new CriarPessoaCreditoResponse(pessoaCredito.Cpf, pessoaCredito.CartaoCredito, pessoaCredito.ChequeEspecial, pessoaCredito.LimiteCredito, pessoaCredito.Score));
    }

    public int CalcularScore(List<HistoricoTransacaoDto> historicoTransacaoDto, string cpf)
    {
        decimal balanco = 0;
        if (historicoTransacaoDto.Count == 0)
            return 0;
        else
        {
            foreach (var item in historicoTransacaoDto)
            {
                if (item.CpfDestinario == cpf)
                    balanco += item.ValorTransacao;

                else if (item.CpfRemetente == cpf)
                    balanco -= item.ValorTransacao;
            }
        }
        if (balanco >= 2000)
            return 60;
        else if (balanco >= 5000)
            return 70;
        else if (balanco >= 10000)
            return 80;
        else if (balanco >= 20000)
            return 90;
        else if (balanco >= 50000)
            return 100;
        else if (balanco <= 0)
            return 0;
        else
            return 50;
    }
}
