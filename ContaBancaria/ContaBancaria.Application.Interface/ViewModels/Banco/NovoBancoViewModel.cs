using ContaBancaria.Dominio.Enums;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.ViewModels.Banco
{
    public class NovoBancoViewModel
    {
        public ushort NumeroBanco { get; set; }

        public ushort Agencia { get; set; }

        public string Nome { get; set; }

        public IEnumerable<TaxaBancariaViewModel> TaxasBancarias { get; set; } =
            new List<TaxaBancariaViewModel>();

        public TipoValorTaxaBancaria TipoValorTaxaBancaria { get; set; }
    }
}
