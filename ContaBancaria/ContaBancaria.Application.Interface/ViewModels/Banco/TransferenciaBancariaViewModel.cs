using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Banco
{
    public class TransferenciaBancariaViewModel
    {
        public Guid GuidContaOrigem { get; set; }

        public Guid GuidContaDestino { get; set; }

        public decimal Valor { get; set; }
    }
}
