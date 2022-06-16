using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Conta
{
    public class DepositoViewModel
    {
        public Guid GuidConta { get; set; }

        public decimal Valor { get; set; }
    }
}
