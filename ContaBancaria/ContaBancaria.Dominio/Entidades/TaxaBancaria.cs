﻿using ContaBancaria.Dominio.Enums;

namespace ContaBancaria.Dominio.Entidades
{
    public class TaxaBancaria : Entity
    {
        public decimal Valor { get; private set; }

        public TipoValorTaxaBancaria TipoValor { get; private set; }

        public TipoTaxaBancaria Tipo { get; private set; }

        public string Descricao { get; private set; }

        public TaxaBancaria(decimal valor, TipoTaxaBancaria tipoTaxaBancaria,
                            TipoValorTaxaBancaria tipoValorTaxaBancaria, string descricao)
        {
            Valor = valor;
            Tipo = tipoTaxaBancaria;
            Descricao = descricao;
            TipoValor = tipoValorTaxaBancaria;
        }
    }
}