using ContaBancaria.Dominio.Enums;
using System;

namespace ContaBancaria.Dominio.Entidades
{
    public class ExtratoConta : Entity
    {
        public DateTime DataOperacao { get; private set; }

        public TipoOperacaoConta TipoOperacao { get; private set; }

        public decimal Valor { get; set; }

        public ExtratoConta(decimal valor, TipoOperacaoConta tipoOperacao, DateTime dataOperacao)
        {
            Valor = valor;
            TipoOperacao = tipoOperacao;
            DataOperacao = dataOperacao; 
        }
    }
}
