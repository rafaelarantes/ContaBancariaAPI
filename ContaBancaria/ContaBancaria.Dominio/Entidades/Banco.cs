using System.Collections.Generic;

namespace ContaBancaria.Dominio.Entidades
{
    public class Banco : Entity
    {
        private readonly string _nome;
        private readonly ushort _numero;
        private readonly ushort _agencia;
        public IEnumerable<TaxaBancaria> TaxasBancarias { get; private set; }

        public Banco(string nome, ushort numero, ushort agencia, IEnumerable<TaxaBancaria> taxasBancarias)
        {
            _nome = nome;
            _numero = numero;
            _agencia = agencia;
            TaxasBancarias = taxasBancarias;
        }

        public override string ToString()
        {
            return $"{ _nome } { _numero } { _agencia }";
        }
    }
}
