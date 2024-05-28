using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Conta
{
    public class TransferenciaViewModel
    {
        public Guid GuidContaOrigem { get; set; }

        public Guid GuidContaDestino { get; set; }

        public decimal Valor { get; set; }
    }
}
