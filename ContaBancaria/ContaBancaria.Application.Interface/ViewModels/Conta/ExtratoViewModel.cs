using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.ViewModels.Conta
{
    public class ExtratoViewModel
    {
        public IEnumerable<ExtratoItemViewModel> ExtratoItems { get; set; }
                = new List<ExtratoItemViewModel>();
    }
}
