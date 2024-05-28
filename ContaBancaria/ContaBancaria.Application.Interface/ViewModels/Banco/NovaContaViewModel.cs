using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Banco
{
    public class NovaContaViewModel
    {
        public Guid GuidBanco { get; set; }
        public ulong NumeroConta { get; set; }
    }
}
