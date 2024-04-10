namespace Cepedi.Banco.Analise.Domain.Entities
{
    public class PessoaCreditoEntity
    {
        public int PessoaId { get; set; }
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
