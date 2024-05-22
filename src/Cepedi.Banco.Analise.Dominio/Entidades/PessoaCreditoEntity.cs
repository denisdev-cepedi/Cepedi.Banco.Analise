namespace Cepedi.Banco.Analise.Dominio.Entidades
{
    public class PessoaCreditoEntity
    {
        public int Id { get; set; }
        public string Cpf { get; set; } = default!;
        public int Score { get; set; }
        public bool CartaoCredito { get; set; }
        public bool ChequeEspecial { get; set; }
        public double LimiteCredito { get; set; }
        internal void Atualizar(bool cartaoCredito, bool chequeEspecial, double limiteCredito)
        {
            CartaoCredito = cartaoCredito;
            ChequeEspecial = chequeEspecial;
            LimiteCredito = limiteCredito;
        }
        
        
    }
}
