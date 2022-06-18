using ContaBancaria.Dominio.Enums;
using System;

namespace ContaBancaria.Dominio.Entidades
{
    public class ExtratoConta : Entity
    {
        public DateTime DataOperacao { get; private set; }

        public TipoOperacaoConta TipoOperacao { get; private set; }

        public decimal Valor { get; private set; }

        public Guid GuidConta { get; private set; }

        public Conta Conta { get; private set; }

        public Guid GuidContaOrigem { get; private set; }

        public TipoTaxaBancaria? TipoTaxaBancaria { get; private set; }

        public ExtratoConta(decimal valor,
                            TipoOperacaoConta tipoOperacao,
                            DateTime dataOperacao,
                            Guid guidConta,
                            Guid guidContaOrigem,
                            TipoTaxaBancaria? tipoTaxaBancaria = null)
        {
            Valor = valor;
            TipoOperacao = tipoOperacao;
            DataOperacao = dataOperacao;
            GuidConta = guidConta;
            GuidContaOrigem = guidContaOrigem;
            TipoTaxaBancaria = tipoTaxaBancaria;
        }

        public ExtratoConta()
        {

        }
    }
}
