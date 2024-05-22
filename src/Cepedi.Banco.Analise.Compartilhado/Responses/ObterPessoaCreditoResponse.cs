namespace Cepedi.Banco.Analise.Compartilhado;

public record ObterPessoaCreditoResponse(string cpf, bool cartaoCredito, bool chequeEspecial, double limiteCredito);
