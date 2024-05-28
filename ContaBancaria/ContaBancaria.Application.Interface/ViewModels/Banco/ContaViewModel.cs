using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Banco
{
    public class ContaViewModel
    {
        public Guid Guid { get; set; }
        public Guid GuidBanco { get; set; }
        public ulong Numero { get; set; }
        public decimal Saldo { get; set; }
    }
}
