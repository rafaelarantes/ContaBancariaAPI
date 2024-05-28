using System;

namespace ContaBancaria.Data.Contracts.Dtos.Banco
{
    public class SaqueBancarioDto
    {
        public Guid GuidConta { get; set; }
        public Guid? GuidContaOrigem { get; set; }
        public decimal Valor { get; set; }
    }
}
