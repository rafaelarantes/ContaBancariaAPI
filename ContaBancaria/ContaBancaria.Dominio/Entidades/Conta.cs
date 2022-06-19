using ContaBancaria.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.Dominio.Entidades
{
    public class Conta : Entity
    {
        public ulong Numero { get; private set; }
        public decimal Saldo => CalcularSaldo();

        public List<ExtratoConta> Extrato { get; private set; } =
            new List<ExtratoConta>();
        
        public Banco Banco { get; private set; }
        public Guid GuidBanco { get; private set; }


        public Conta(ulong numero, Banco banco)
        {
            Numero = numero;
            GuidBanco = banco.Guid;
            Banco = banco;
            Extrato = new List<ExtratoConta>();
        }

        public Conta(ulong numero, Guid guidBanco)
        {
            Numero = numero;
            GuidBanco = guidBanco;
            Extrato = new List<ExtratoConta>();
        }

        public Conta()
        {

        }

        public async Task<bool> Creditar(decimal valor, Guid? guidContaOrigem = null)
        {
            if (valor < 0)
                return await Task.FromResult(false);

            if (guidContaOrigem == null) guidContaOrigem = Guid;

            Extrato.Add(new ExtratoConta(valor, TipoOperacaoConta.Credito, DateTime.Now, Guid, guidContaOrigem.Value));

            return await Task.FromResult(true);
        }

        public async Task<bool> Debitar(decimal valor, Guid? guidContaOrigem = null)
        {
            if (Saldo < valor || valor < 0)
                return await Task.FromResult(false);

            if (guidContaOrigem == null) guidContaOrigem = Guid;

            Extrato.Add(new ExtratoConta(valor, TipoOperacaoConta.Debito, DateTime.Now, Guid, guidContaOrigem.Value));

            return await Task.FromResult(true);
        }

        public async Task<bool> DebitarTaxaBancaria(TipoTaxaBancaria tipoTaxaBancaria, decimal valor)
        {
            var taxasBancarias = Banco.TaxasBancarias.ToList()
                                                      .Where(t => t.Tipo == tipoTaxaBancaria)
                                                      .ToList();

            var taxaBancaria = Banco.CalcularTaxaBancaria(valor, taxasBancarias);

            Extrato.Add(new ExtratoConta(taxaBancaria, TipoOperacaoConta.TaxaBancaria,
                                          DateTime.Now, Guid.Empty, Guid, tipoTaxaBancaria));

            return await Task.FromResult(true);
        }

        private decimal CalcularSaldo()
        {
            if (Extrato == null) return 0;

            var somaValoresCredito = Extrato.Where(e => e.TipoOperacao == TipoOperacaoConta.Credito)
                                             .Sum(e => e.Valor);

            var somaValoresDebito = Extrato.Where(e => e.TipoOperacao == TipoOperacaoConta.Debito ||
                                                       e.TipoOperacao == TipoOperacaoConta.TaxaBancaria)
                                            .Sum(e => e.Valor);

            return somaValoresCredito - somaValoresDebito;
        }
    }
}
