using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IContaMapper
    {
        DepositoBancarioViewModel Map(DepositoViewModel depositoViewModel);

        SaqueBancarioViewModel Map(SaqueViewModel SaqueViewModel);

        TransferenciaBancariaViewModel Map(TransferenciaViewModel transferenciaViewModel);
        
        ExtratoViewModel Map(Conta conta);
    }
}
