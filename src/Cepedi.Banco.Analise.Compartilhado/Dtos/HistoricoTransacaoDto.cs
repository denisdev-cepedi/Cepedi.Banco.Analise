namespace Cepedi.Banco.Analise.Compartilhado.Dtos;
public class HistoricoTransacaoDto
{
    public string CpfDestinario { get; set; } = default!;
    public string CpfRemetente { get; set; } = default!;
    public decimal ValorTransacao { get; set; }
    public DateTime DataTransacao { get; set; }
    public string DescricaoTransacao { get; set; } = default!;
    public string TipoTransacao { get; set; } = default!;

}
