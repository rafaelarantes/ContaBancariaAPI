using ContaBancaria.Application.Contracts.ViewModels.Shared;
using ContaBancaria.Dominio.Enums;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.ViewModels.BancoCentral
{
    public class SelecaoTaxaBancariaViewModel
    {
        public List<SelecaoViewModel> TipoTaxaBancaria { get; set; }

        public List<SelecaoViewModel> TipoValorTaxaBancaria { get; set; }
    }
}
