using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Data.Contracts.Dtos.Banco;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace ContaBancaria.Application.Mappers
{
    public class BancoMapper : IBancoMapper
    {
        public IEnumerable<BancosViewModel> Map(IEnumerable<Banco> bancos)
        {
            return bancos.Select(b => new BancosViewModel
            {
                Guid = b.Guid,
                Nome = b.Nome,
                Agencia = b.Agencia,
                Numero = b.Numero
            });
        }

        public Banco Map(NovoBancoViewModel novoBancoViewModel)
        {
            return new Banco(novoBancoViewModel.Nome, novoBancoViewModel.NumeroBanco, novoBancoViewModel.Agencia,
                             novoBancoViewModel.TaxasBancarias
                             .Select(t => new TaxaBancaria(t.Valor, t.TipoTaxaBancaria,
                                                           novoBancoViewModel.TipoValorTaxaBancaria, t.Descricao)).ToList());
        }

        public DepositoBancarioDto Map(DepositoBancarioViewModel depositoBancarioViewModel)
        {
            return new DepositoBancarioDto
            {
                GuidConta = depositoBancarioViewModel.GuidConta,
                GuidContaOrigem = depositoBancarioViewModel.GuidContaOrigem,
                Valor = depositoBancarioViewModel.Valor
            };
        }

        public SaqueBancarioDto Map(SaqueBancarioViewModel saqueBancarioViewModel)
        {
            return new SaqueBancarioDto
            {
                GuidConta = saqueBancarioViewModel.GuidConta,
                GuidContaOrigem = saqueBancarioViewModel.GuidContaOrigem,
                Valor = saqueBancarioViewModel.Valor
            };
        }

        public TransferenciaBancariaDto Map(TransferenciaBancariaViewModel transferenciaBancariaViewModel)
        {
            return new TransferenciaBancariaDto
            {
                GuidContaOrigem = transferenciaBancariaViewModel.GuidContaOrigem,
                GuidContaDestino = transferenciaBancariaViewModel.GuidContaDestino,
                Valor = transferenciaBancariaViewModel.Valor
            };
        }

        public IEnumerable<ContaViewModel> Map(IEnumerable<Conta> contas)
        {
            return contas.Select(c => new ContaViewModel
            {
                Guid = c.Guid,
                GuidBanco = c.GuidBanco,
                Numero = c.Numero,
                Saldo = c.Saldo
            });
        }
    }
}
