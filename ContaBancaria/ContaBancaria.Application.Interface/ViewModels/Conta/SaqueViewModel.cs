using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Conta
{
    public class SaqueViewModel
    {
        public Guid GuidConta { get; set; }

        public decimal Valor { get; set; }
    }
}
