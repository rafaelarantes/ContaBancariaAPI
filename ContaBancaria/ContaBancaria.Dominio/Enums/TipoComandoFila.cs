using System.ComponentModel;

namespace ContaBancaria.Dominio.Enums
{
    public enum TipoComandoFila : byte
    {
        [Description("Conta/Depositar")]
        Deposito = 1 
    }
}
