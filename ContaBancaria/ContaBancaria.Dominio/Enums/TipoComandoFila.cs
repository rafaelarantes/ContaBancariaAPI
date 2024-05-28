using System.ComponentModel;

namespace ContaBancaria.Dominio.Enums
{
    public enum TipoComandoFila : byte
    {
        [Description("Banco/ReceberTransferencia")]
        Deposito = 1 
    }
}
