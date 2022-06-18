namespace ContaBancaria.Data.Contracts.Dtos.Banco
{
    public class TransferenciaBancariaDto
    {
        public Dominio.Entidades.Conta ContaOrigem { get; set; }

        public Dominio.Entidades.Conta ContaDestino { get; set; }

        public decimal Valor { get; set; }
    }
}
