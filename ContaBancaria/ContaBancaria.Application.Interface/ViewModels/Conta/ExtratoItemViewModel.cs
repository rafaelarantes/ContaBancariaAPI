using ContaBancaria.Dominio.Enums;
using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Conta
{
    public class ExtratoItemViewModel
    {
        public DateTime DataOperacao { get; set; }

        public TipoOperacaoConta TipoOperacao { get; set; }

        public decimal Valor { get; set; }
    }
}
