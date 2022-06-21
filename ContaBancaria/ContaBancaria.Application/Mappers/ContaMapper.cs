using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System.Linq;

namespace ContaBancaria.Application.Mappers
{
    public class ContaMapper : IContaMapper
    {
        public ExtratoViewModel Map(Conta conta)
        {
            return new ExtratoViewModel
            {
                ExtratoItems = conta.Extrato.ToList().Select(e => new ExtratoItemViewModel 
                { 
                    DataOperacao = e.DataOperacao,
                    TipoOperacao = e.TipoOperacao,
                    Valor = e.Valor
                }),
                Saldo = conta.Saldo
            };
        }

        public DepositoBancarioViewModel Map(DepositoViewModel depositoViewModel)
        {
            return new DepositoBancarioViewModel
            {
                GuidConta = depositoViewModel.GuidConta,
                Valor = depositoViewModel.Valor
            };
        }

        public SaqueBancarioViewModel Map(SaqueViewModel SaqueViewModel)
        {
            return new SaqueBancarioViewModel
            {
                GuidConta = SaqueViewModel.GuidConta,
                Valor = SaqueViewModel.Valor
            };
        }

        public TransferenciaBancariaViewModel Map(TransferenciaViewModel transferenciaViewModel)
        {
            return new TransferenciaBancariaViewModel
            {
                Valor = transferenciaViewModel.Valor,
                GuidContaOrigem = transferenciaViewModel.GuidContaOrigem,
                GuidContaDestino = transferenciaViewModel.GuidContaDestino
            };
        }

    }
}
