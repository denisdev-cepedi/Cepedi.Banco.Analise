namespace Cepedi.Banco.Analise.Compartilhado;

public record ObterPessoaCreditoResponse(int id, bool cartaoCredito, bool chequeEspecial, double limiteCredito);
