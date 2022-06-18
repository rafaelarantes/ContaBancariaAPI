using System;

namespace ContaBancaria.Data.Contracts.Dtos.Banco
{
    public class TransferenciaBancariaDto
    {
        public Guid GuidContaOrigem { get; set; }

        public Guid GuidContaDestino { get; set; }

        public decimal Valor { get; set; }

    }
}
