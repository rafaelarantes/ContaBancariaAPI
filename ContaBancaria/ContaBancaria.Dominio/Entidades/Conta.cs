﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.Dominio.Entidades
{
    public class Conta : Entity
    {
        private readonly ushort _numero;
        private IList<ExtratoConta> _extrato;
        
        public decimal Saldo { get; private set; }

        public IReadOnlyCollection<ExtratoConta> Extrato => _extrato.ToList();

        public Conta(ushort numero)
        {
            _numero = numero;
            _extrato = new List<ExtratoConta>();
        }

        public async Task<bool> Creditar(decimal valor)
        {
            Saldo += valor;

            _extrato.Add(new ExtratoConta(valor, Enums.TipoOperacaoConta.Credito, DateTime.Now));

            return await Task.FromResult(true);
        }

        public async Task<bool> Debitar(decimal valor)
        {
            if (Saldo == 0 || Saldo < valor)
                return await Task.FromResult(false);

            Saldo -= valor;

            _extrato.Add(new ExtratoConta(valor, Enums.TipoOperacaoConta.Debito, DateTime.Now));

            return await Task.FromResult(true);
        }

    }
}
