using ContaBancaria.Dominio.Contracts;
using System;

namespace ContaBancaria.Dominio.Entidades
{
    public abstract class Entity : IEntity
    {
        public Guid Guid { get; private set; }
        
        public Entity()
        {
            Guid = Guid.NewGuid();
        }
    }
}
