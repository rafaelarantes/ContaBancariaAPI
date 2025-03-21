﻿using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Data.Contracts.Dtos.Banco;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IBancoMapper
    {
        IEnumerable<BancosViewModel> Map(IEnumerable<Banco> bancos);

        Banco Map(NovoBancoViewModel novoBancoViewModel);

        DepositoBancarioDto Map(DepositoBancarioViewModel depositoBancarioViewModel);

        SaqueBancarioDto Map(SaqueBancarioViewModel saqueBancarioViewModel);

        TransferenciaBancariaDto Map(TransferenciaBancariaViewModel transferenciaBancariaViewModel);
        
        IEnumerable<ContaViewModel> Map(IEnumerable<Conta> contas);
    }
}
