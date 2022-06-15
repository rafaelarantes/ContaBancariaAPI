using System;

namespace ContaBancaria.Dominio.Entidades
{
    public class Conta
    {
        private readonly ushort _numero;
        public ulong Saldo { get; private set; }
        
        public Guid Guid { get; set; }

        public Conta(ushort numero)
        {
            _numero = numero;

            Guid = Guid.NewGuid();
        }
    }
}
