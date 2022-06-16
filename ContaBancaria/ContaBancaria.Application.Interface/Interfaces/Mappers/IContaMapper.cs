﻿using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.Interfaces.Mappers
{
    public interface IContaMapper
    {
        ExtratoViewModel Map(IReadOnlyCollection<ExtratoConta> extrato);
    }
}
