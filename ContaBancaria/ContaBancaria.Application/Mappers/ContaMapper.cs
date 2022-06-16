using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Dominio.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace ContaBancaria.Application.Mappers
{
    public class ContaMapper : IContaMapper
    {
        public ExtratoViewModel Map(IReadOnlyCollection<ExtratoConta> extrato)
        {
            return new ExtratoViewModel
            {
                ExtratoItems = extrato.ToList().Select(e => new ExtratoItemViewModel 
                { 
                    DataOperacao = e.DataOperacao,
                    TipoOperacao = e.TipoOperacao,
                    Valor = e.Valor
                })
            };
        }
    }
}
