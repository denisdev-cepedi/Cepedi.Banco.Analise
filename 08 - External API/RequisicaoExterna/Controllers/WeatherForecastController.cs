using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RequisicaoExternas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        [HttpGet]
        public IActionResult HistoricoTransacao()
        {
            var lista = new List<HistoricoTransacaoDto>
            {
                new HistoricoTransacaoDto
                {
                    CpfDestinario = "12345678901",
                    CpfRemetente = "10987654321",
                    ValorTransacao = 100,
                    DataTransacao = DateTime.Now,
                    DescricaoTransacao = "Transferência",
                    TipoTransacao = "Débito"
                },
                new HistoricoTransacaoDto
                {
                    CpfDestinario = "12345678901",
                    CpfRemetente = "10987654321",
                    ValorTransacao = 100,
                    DataTransacao = DateTime.Now,
                    DescricaoTransacao = "Transferência",
                    TipoTransacao = "Débito"
                },
                new HistoricoTransacaoDto
                {
                    CpfDestinario = "12345678901",
                    CpfRemetente = "10987654321",
                    ValorTransacao = 100,
                    DataTransacao = DateTime.Now,
                    DescricaoTransacao = "Transferência",
                    TipoTransacao = "Débito"
                }
            };

            return Ok(lista);
        }
    }

    public class HistoricoTransacaoDto {
    public string CpfDestinario { get; set; } = default!;
    public string CpfRemetente { get; set; } = default!;
    public decimal ValorTransacao { get; set; }
    public DateTime DataTransacao { get; set; }
    public string DescricaoTransacao { get; set; } = default!;
    public string TipoTransacao { get; set; } = default!;
    }
}
