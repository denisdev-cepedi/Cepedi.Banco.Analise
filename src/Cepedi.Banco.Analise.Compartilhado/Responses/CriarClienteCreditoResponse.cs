namespace Cepedi.Banco.Analise.Compartilhado.Responses;

public record CriarClienteCreditoResponse( int idPessoaCredito, bool cartaoCredito, bool chequeEspecial, double limiteCredito);
