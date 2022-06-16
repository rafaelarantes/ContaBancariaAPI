using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Conta
{
    public class DepositoViewModel
    {
        public Guid GuidContaDestino { get; set; }

        public decimal Valor { get; set; }

        public Guid GuidContaOrigem { get; set; }
    }
}
