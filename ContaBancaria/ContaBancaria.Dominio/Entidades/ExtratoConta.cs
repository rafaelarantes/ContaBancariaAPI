using ContaBancaria.Dominio.Enums;
using System;

namespace ContaBancaria.Dominio.Entidades
{
    public class ExtratoConta : Entity
    {
        public DateTime DataOperacao { get; private set; }

        public TipoOperacaoConta TipoOperacao { get; private set; }

        public decimal Valor { get; private set; }

        public Guid GuidContaOrigem { get; private set; }

        public TipoTaxaBancaria? TipoTaxaBancaria { get; private set; }

        public ExtratoConta(decimal valor,
                            TipoOperacaoConta tipoOperacao,
                            DateTime dataOperacao,
                            Guid guidContaOrigem,
                            TipoTaxaBancaria? tipoTaxaBancaria = null)
        {
            Valor = valor;
            TipoOperacao = tipoOperacao;
            DataOperacao = dataOperacao;
            GuidContaOrigem = guidContaOrigem;
            TipoTaxaBancaria = tipoTaxaBancaria;
        }
    }
}
