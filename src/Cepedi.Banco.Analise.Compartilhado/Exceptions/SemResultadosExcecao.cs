using Cepedi.Banco.Analise.Compartilhado.Enums;

namespace Cepedi.Banco.Analise.Compartilhado.Excecoes;
public class SemResultadosExcecao : ExcecaoAplicacao
{
    public SemResultadosExcecao() : 
        base(AnaliseMensagemErro.SemResultados)
    {
    }
}
