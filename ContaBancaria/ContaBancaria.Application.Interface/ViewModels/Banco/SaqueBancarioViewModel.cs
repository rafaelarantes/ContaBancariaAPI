﻿using System;

namespace ContaBancaria.Application.Contracts.ViewModels.Banco
{
    public class SaqueBancarioViewModel
    {
        public Dominio.Entidades.Conta Conta { get; set; }

        public decimal Valor { get; set; }

        public Guid? GuidContaOrigem { get; set; } = null;
    }
}
