using Cepedi.Banco.Analise.Compartilhado.Enums;
using Cepedi.Banco.Analise.Compartilhado.Excecoes;

namespace Cepedi.Banco.Analise.Compartilhado.Exceptions;
public class RequestInvalidaExcecao : ExcecaoAplicacao
{
    public RequestInvalidaExcecao(IDictionary<string, string[]> erros)
        : base(AnaliseMensagemErro.DadosInvalidos) =>
        Erros = erros.Select(e => $"{e.Key}: {string.Join(", ", e.Value)}");

    public IEnumerable<string> Erros { get; }
}
