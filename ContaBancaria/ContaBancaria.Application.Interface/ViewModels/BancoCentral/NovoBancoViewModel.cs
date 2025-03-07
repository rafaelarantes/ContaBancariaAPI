using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Dominio.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContaBancaria.Application.Contracts.ViewModels.BancoCentral
{
    public class NovoBancoViewModel
    {
        public ushort NumeroBanco { get; set; }

        public ushort Agencia { get; set; }

        public string Nome { get; set; }

        public IEnumerable<TaxaBancariaViewModel> TaxasBancarias { get; set; } =
            new List<TaxaBancariaViewModel>();     
    }
}
