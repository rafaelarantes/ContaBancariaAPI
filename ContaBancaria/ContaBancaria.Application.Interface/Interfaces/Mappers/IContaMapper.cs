﻿using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IContaMapper
    {
        DepositoBancarioViewModel Map(DepositoViewModel depositoViewModel, Conta conta);

        SaqueBancarioViewModel Map(SaqueViewModel SaqueViewModel, Conta conta);

        TransferenciaBancariaViewModel Map(Conta contaOrigem, Conta contaDestino, 
                                            TransferenciaViewModel transferenciaViewModel);
        ExtratoViewModel Map(List<ExtratoConta> extrato);
    }
}
