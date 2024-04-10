using Cepedi.Banco.Analise.Shareable.Enums;

namespace Cepedi.Banco.Analise.Shareable.Exceptions;
public class SemResultadosException : ApplicationException
{
    public SemResultadosException() : 
        base(BancoCentralMensagemErrors.SemResultados)
    {
    }
}
