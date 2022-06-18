using ContaBancaria.Dominio.Contracts;
using System;
using System.Collections;

namespace ContaBancaria.Dominio.Entidades
{
    public abstract class Entity : IEntity
    {
        public Guid Guid { get; private set; }

        public Entity()
        {
            Guid = Guid.NewGuid();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
