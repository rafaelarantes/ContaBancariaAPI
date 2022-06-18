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

        public DepositoBancarioViewModel Map(DepositoViewModel depositoViewModel, Conta conta)
        {
            return new DepositoBancarioViewModel
            {
                Conta = conta,
                GuidContaOrigem = depositoViewModel.GuidContaOrigem,
                Valor = depositoViewModel.Valor
            };
        }

        public SaqueBancarioViewModel Map(SaqueViewModel SaqueViewModel, Conta conta)
        {
            return new SaqueBancarioViewModel
            {
                Conta = conta,
                GuidContaOrigem = SaqueViewModel.GuidContaOrigem,
                Valor = SaqueViewModel.Valor
            };
        }

        public TransferenciaBancariaViewModel Map(Conta contaOrigem, Conta contaDestino, 
                                                  TransferenciaViewModel transferenciaViewModel)
        {
            return new TransferenciaBancariaViewModel
            {
                ContaOrigem = contaOrigem,
                ContaDestino = contaDestino,
                Valor = transferenciaViewModel.Valor
            };
        }

    }
}
