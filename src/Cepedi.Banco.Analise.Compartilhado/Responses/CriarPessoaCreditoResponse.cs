namespace Cepedi.Banco.Analise.Compartilhado.Responses;

public record CriarPessoaCreditoResponse( string cpf, bool cartaoCredito, bool chequeEspecial, double limiteCredito);
