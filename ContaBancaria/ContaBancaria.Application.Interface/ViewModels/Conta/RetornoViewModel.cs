using System.Collections.Generic;

namespace ContaBancaria.Application.Contracts.ViewModels.Conta
{
    public class RetornoViewModel
    {
        public bool Resultado { get; set; }

        public List<string> Mensagens { get; set; }

        public object Data { get; set; }
    }
}
