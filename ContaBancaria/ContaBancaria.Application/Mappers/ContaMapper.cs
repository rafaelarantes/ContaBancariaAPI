using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace ContaBancaria.Application.Mappers
{
    public class ContaMapper : IContaMapper
    {
        public ExtratoViewModel Map(List<ExtratoConta> extrato)
        {
            return new ExtratoViewModel
            {
                ExtratoItems = extrato.ToList().Select(e => new ExtratoItemViewModel 
                { 
                    DataOperacao = e.DataOperacao,
                    TipoOperacao = e.TipoOperacao,
                    Valor = e.Valor
                })
            };
        }

        public DepositoBancarioViewModel Map(DepositoViewModel depositoViewModel)
        {
            return new DepositoBancarioViewModel
            {
                GuidConta = depositoViewModel.GuidContaDestino,
                GuidContaOrigem = depositoViewModel.GuidContaOrigem,
                Valor = depositoViewModel.Valor
            };
        }

        public SaqueBancarioViewModel Map(SaqueViewModel SaqueViewModel)
        {
            return new SaqueBancarioViewModel
            {
                GuidConta = SaqueViewModel.GuidConta,
                GuidContaOrigem = SaqueViewModel.GuidContaOrigem,
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
