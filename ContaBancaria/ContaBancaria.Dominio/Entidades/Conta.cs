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

        public async Task<bool> Creditar(decimal valor, Guid? guidContaOrigem = null)
        {
            if (valor < 0)
                return await Task.FromResult(false);

            if (guidContaOrigem == null) guidContaOrigem = Guid;

            _extrato.Add(new ExtratoConta(valor, TipoOperacaoConta.Credito, DateTime.Now, guidContaOrigem.Value));

            return await Task.FromResult(true);
        }

        public async Task<bool> Debitar(decimal valor, Guid? guidContaOrigem = null)
        {
            if (Saldo < valor || valor < 0)
                return await Task.FromResult(false);

            if (guidContaOrigem == null) guidContaOrigem = Guid;

            _extrato.Add(new ExtratoConta(valor, TipoOperacaoConta.Debito, DateTime.Now, guidContaOrigem.Value));

            return await Task.FromResult(true);
        }

        public async Task<bool> DebitarTaxaBancaria(TipoTaxaBancaria tipoTaxaBancaria, decimal valor)
        {
            var taxasBancarias = _banco.TaxasBancarias.ToList()
                                                      .Where(t => t.Tipo == tipoTaxaBancaria)
                                                      .ToList();

            var taxaBancaria = _banco.CalcularTaxaBancaria(valor, taxasBancarias);

            _extrato.Add(new ExtratoConta(taxaBancaria, TipoOperacaoConta.TaxaBancaria,
                                          DateTime.Now, Guid.Empty, tipoTaxaBancaria));

            return await Task.FromResult(true);
        }

        private decimal CalcularSaldo()
        {
            var somaValoresCredito = Extrato.Where(e => e.TipoOperacao == TipoOperacaoConta.Credito)
                                             .Sum(e => e.Valor);

            var somaValoresDebito = Extrato.Where(e => e.TipoOperacao == TipoOperacaoConta.Debito ||
                                                       e.TipoOperacao == TipoOperacaoConta.TaxaBancaria)
                                            .Sum(e => e.Valor);

            return somaValoresCredito - somaValoresDebito;
        }
    }
}
