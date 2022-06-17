using ContaBancaria.Dominio.Enums;

namespace ContaBancaria.Dominio.Entidades
{
    public class TaxaBancaria : Entity
    {
        public decimal Valor { get; private set; }

        public TipoTaxaBancaria TipoTaxaBancaria { get; private set; }

        public string Descricao { get; private set; }

        public TaxaBancaria(decimal valor, TipoTaxaBancaria tipoTaxaBancaria, string descricao)
        {
            Valor = valor;
            TipoTaxaBancaria = tipoTaxaBancaria;
            Descricao = descricao;
        }
    }
}