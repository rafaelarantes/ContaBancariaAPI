using System;

namespace ContaBancaria.Cross.Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceBus = new ServiceBus();
            serviceBus.Execute();
        }
    }
}
