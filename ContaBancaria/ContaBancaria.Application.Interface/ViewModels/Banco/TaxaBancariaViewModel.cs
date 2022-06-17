using ContaBancaria.Dominio.Enums;

namespace ContaBancaria.Application.Contracts.ViewModels.Banco
{
    public class TaxaBancariaViewModel
    {
        public decimal Valor { get; set; }
        public TipoTaxaBancaria TipoTaxaBancaria { get; set; }
        public string Descricao { get; set; }
    }
}
