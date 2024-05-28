using System;

namespace ContaBancaria.Data.Contracts.Dtos.Banco
{
    public class DepositoBancarioDto
    {
        public Guid GuidConta { get; set; }

        public Guid? GuidContaOrigem { get; set; }

        public decimal Valor { get; set; }
    }
}
