using System.Collections.Generic;

namespace ContaBancaria.Dominio.Entidades
{
    public class Banco : Entity
    {
        private readonly string _nome;
        private readonly ushort _numero;
        private readonly ushort _agencia;
        private IEnumerable<Conta> _contas;

        public Banco(string nome, ushort numero, ushort agencia)
        {
            _nome = nome;
            _numero = numero;
            _agencia = agencia;

            _contas = new List<Conta>();
        }

        public override string ToString()
        {
            return $"{ _nome } { _numero } { _agencia }";
        }
    }
}
