using Cepedi.Banco.Analise.Compartilhado.Excecoes;

namespace Cepedi.Banco.Analise.Compartilhado.Enums;
public class PessoaCreditoErros
{
    public static readonly ResultadoErro BuscaHistoricoNaoEncontrado = new()
    {
        Titulo = "Histórico não encontrado",
        Descricao = "Verifique os dados informados e tente novamente",
        Tipo = ETipoErro.Alerta
    };

    public static readonly ResultadoErro Generico = new()
    {
        Titulo = "Ops ocorreu um erro no nosso sistema",
        Descricao = "No momento, nosso sistema está indisponível. Por Favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static readonly ResultadoErro SemResultados = new()
    {
        Titulo = "Sua busca não obteve resultados",
        Descricao = "Tente buscar novamente",
        Tipo = ETipoErro.Alerta
    };

    public static ResultadoErro ErroGravacaoPessoaCredito = new()
    {
        Titulo = "Ocorreu um erro na gravação",
        Descricao = "Ocorreu um erro na gravação da Pessoa Credito. Por favor tente novamente",
        Tipo = ETipoErro.Erro
    };

    public static ResultadoErro DadosInvalidos = new()
    {
        Titulo = "Dados inválidos",
        Descricao = "Os dados enviados na requisição são inválidos",
        Tipo = ETipoErro.Erro
    };
}
