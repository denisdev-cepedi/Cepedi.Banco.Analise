using Cepedi.Banco.Analise.Compartilhado;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;
using Cepedi.Banco.Analise.Compartilhado.Requests;
using Cepedi.Banco.Analise.Compartilhado.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cepedi.Banco.Analise.Api.Controllers;


[ApiController]
[Route("[controller]")]
public class PessoaCreditoController : BaseController
{
    private readonly ILogger<PessoaCreditoController> _logger;
    private readonly IMediator _mediator;

    public PessoaCreditoController(ILogger<PessoaCreditoController> logger, IMediator mediator) : base(mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CriarPessoaCreditoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CriarPessoaCreditoResponse>> CriarPagamentoAsync(
        [FromBody] CriarPessoaCreditoRequest request) => await SendCommand(request);

    [HttpPut]
    [ProducesResponseType(typeof(AtualizarPessoaCreditoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<AtualizarPessoaCreditoResponse>> AtualizarPagamentoAsync(
        [FromBody] AtualizarPessoaCreditoRequest request) => await SendCommand(request);

    [HttpGet("{Cpf}")]
    [ProducesResponseType(typeof(ObterPessoaCreditoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ObterPessoaCreditoResponse>> ObterPessoaCreditoRequestAsync(
        [FromRoute] ObterPessoaCreditoRequest request) => await SendCommand(request);

    [HttpDelete("{Cpf}")]
    [ProducesResponseType(typeof(DeletarPessoaCreditoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResultadoErro), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<DeletarPessoaCreditoResponse>> DeletarPagamentoAsync(
        [FromRoute] DeletarPessoaCreditoRequest request) => await SendCommand(request);
}
