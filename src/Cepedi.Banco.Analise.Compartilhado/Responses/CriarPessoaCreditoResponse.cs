namespace Cepedi.Banco.Analise.Compartilhado.Responses;

public record CriarPessoaCreditoResponse( int idPessoaCredito, bool cartaoCredito, bool chequeEspecial, double limiteCredito);
