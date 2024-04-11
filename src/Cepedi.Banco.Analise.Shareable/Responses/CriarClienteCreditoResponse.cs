namespace Cepedi.Banco.Analise.Shareable.Responses;

public record CriarClienteCreditoResponse( int idPessoaCredito, bool cartaoCredito, bool chequeEspecial, double limiteCredito);
