using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Dtos;
using System.Collections.Generic;

namespace ContaBancaria.Application.Mappers
{
    public class RetornoMapper : IRetornoMapper
    {
        public RetornoViewModel Map(object data, bool resultado = true)
        {
            return new RetornoViewModel
            {
                Resultado = resultado, 
                Data = data
            };
        }

        public RetornoViewModel Map(RetornoDto retornoDto)
        {
            return new RetornoViewModel
            {
                Resultado = retornoDto.Resultado,
                Data = retornoDto.Data
            };
        }

        public RetornoViewModel Map(bool resultado, List<string> mensagens)
        {
            return new RetornoViewModel
            {
                Resultado = resultado,
                Mensagens = mensagens ?? default
            };
        }
    }
}
