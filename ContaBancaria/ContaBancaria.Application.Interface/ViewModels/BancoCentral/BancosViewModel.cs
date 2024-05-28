using System;

namespace ContaBancaria.Application.Contracts.ViewModels.BancoCentral
{
    public class BancosViewModel
    {
        public Guid Guid { get; set; }
        public string Nome { get; set; }
        public ushort Agencia { get; set; }
        public ushort Numero { get; set; }
    }
}
