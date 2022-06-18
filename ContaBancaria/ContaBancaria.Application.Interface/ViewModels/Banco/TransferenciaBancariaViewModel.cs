namespace ContaBancaria.Application.Contracts.ViewModels.Banco
{
    public class TransferenciaBancariaViewModel
    {
        public Dominio.Entidades.Conta ContaOrigem { get; set; }

        public Dominio.Entidades.Conta ContaDestino { get; set; }

        public decimal Valor { get; set; }
    }
}
