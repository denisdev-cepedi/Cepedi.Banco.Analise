namespace Cepedi.Banco.Analise.Compartilhado;

public record AtualizarPessoaCreditoResponse (string Cpf,bool CartaoCredito, bool ChequeEspecial, double LimiteCredito);
