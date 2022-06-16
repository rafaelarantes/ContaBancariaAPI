using ContaBancaria.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.Dominio.Entidades
{
    public class Conta : Entity
    {
        private readonly ulong _numero;
        private IList<ExtratoConta> _extrato;
        private readonly Banco _banco;

        public Guid GuidBanco => _banco.Guid;

        public decimal Saldo => CalcularSaldo();


        public IReadOnlyCollection<ExtratoConta> Extrato => _extrato.ToList();

        public Conta(ulong numero, Banco banco)
        {
            _numero = numero;
            _banco = banco;
            _extrato = new List<ExtratoConta>();
        }

        public async Task<bool> Creditar(decimal valor)
        {
            if (valor < 0)
                return await Task.FromResult(false);

            _extrato.Add(new ExtratoConta(valor, TipoOperacaoConta.Credito, DateTime.Now));

            return await Task.FromResult(true);
        }

        public async Task<bool> Debitar(decimal valor)
        {
            if (Saldo < valor || valor < 0)
                return await Task.FromResult(false);

            _extrato.Add(new ExtratoConta(valor, TipoOperacaoConta.Debito, DateTime.Now));

            return await Task.FromResult(true);
        }

        private decimal CalcularSaldo()
        {
            var somaValoresCredito = _extrato.Where(e => e.TipoOperacao == TipoOperacaoConta.Credito)
                                             .Sum(e => e.Valor);

            var somaValoresDebito = _extrato.Where(e => e.TipoOperacao == TipoOperacaoConta.Debito)
                                            .Sum(e => e.Valor);

            return somaValoresCredito - somaValoresDebito;
        }
    }
}
