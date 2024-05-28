using ContaBancaria.Dominio.Enums;
using System.Collections.Generic;

namespace ContaBancaria.Dominio.Entidades
{
    public class Banco : Entity
    {
        public string Nome { get; private set; }
        public ushort Numero { get; private set; }
        public ushort Agencia { get; private set; }

        public List<TaxaBancaria> TaxasBancarias { get; private set; }
        public List<Conta> Contas { get; private set; }

        public Banco(string nome, ushort numero, ushort agencia, List<TaxaBancaria> taxasBancarias)
        {
            Nome = nome;
            Numero = numero;
            Agencia = agencia;
            TaxasBancarias = taxasBancarias ?? new List<TaxaBancaria>();
        }

        public Banco()
        {

        }

        public decimal CalcularTaxaBancaria(decimal valor, List<TaxaBancaria> taxasBancarias)
        {
            decimal taxaBancaria = 0;

            taxasBancarias.ForEach(t =>
            {
                if (t.TipoValor == TipoValorTaxaBancaria.Percentual)
                {
                    taxaBancaria += valor * t.Valor / 100;
                    return;
                }

                taxaBancaria += t.Valor;
            });
            return taxaBancaria;
        }
    }
}
