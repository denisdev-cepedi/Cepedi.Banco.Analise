using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Enums;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
using Cepedi.Banco.Analise.Dominio.Repositorio;
using MediatR;
using Microsoft.Extensions.Logging;
using OperationResult;

namespace Cepedi.Banco.Analise.Dominio;

public class DeletarPessoaCreditoRequestHandler : IRequestHandler<DeletarPessoaCreditoRequest, Result<DeletarPessoaCreditoResponse>>
{
    private readonly IPessoaCreditoRepository _pessoaCreditoRepository;
    private readonly ILogger<DeletarPessoaCreditoRequestHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public DeletarPessoaCreditoRequestHandler(IPessoaCreditoRepository pessoaCreditoRepository, ILogger<DeletarPessoaCreditoRequestHandler> logger, IUnitOfWork unitOfWork)
    {
        _pessoaCreditoRepository = pessoaCreditoRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DeletarPessoaCreditoResponse>> Handle(DeletarPessoaCreditoRequest request, CancellationToken cancellationToken)
    {
        var pessoaEntity = await _pessoaCreditoRepository.ObterPessoaCreditoAsync(request.Cpf);
       
        if (pessoaEntity == null)
        {
            _logger.LogError("Erro ao buscar pessoa credito");
            return Result.Error<DeletarPessoaCreditoResponse>(new SemResultadosExcecao());
        }


        var response = await _pessoaCreditoRepository.DeletarPessoaCreditoAsync(pessoaEntity.Cpf);

        if (response == null)
        {
            _logger.LogError("Erro ao deletar pessoa credito");
            return Result.Error<DeletarPessoaCreditoResponse>(new Compartilhado.Excecoes.ExcecaoAplicacao(PessoaCreditoErros.ErroGravacaoPessoaCredito));
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new DeletarPessoaCreditoResponse(pessoaEntity.Cpf));
    }
}
