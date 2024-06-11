using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Data.Enums;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class FilaProcessamentoApplication : IFilaProcessamentoApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IFilaProcessamentoDbRepository _filaProcessamentoDbRepository;
        private readonly IFilaProcessamentoQueueRepository _filaProcessamentoQueueRepository;
        private readonly TipoFila _tipoFila;

        public FilaProcessamentoApplication(IConfiguration configuration,
                                            IFilaProcessamentoDbRepository filaProcessamentoDbRepository,
                                            IFilaProcessamentoQueueRepository filaProcessamentoQueueRepository)
        {
            _configuration = configuration;
            _filaProcessamentoDbRepository = filaProcessamentoDbRepository;
            _filaProcessamentoQueueRepository = filaProcessamentoQueueRepository;

            _tipoFila = Enum.Parse<TipoFila>(_configuration.GetSection("Messaging").Value);
        }

        public async Task<RetornoDto> Enfileirar(TipoComandoFila tipoComandoFila, string dados)
        {
            var filaProcessamento = new FilaProcessamento(tipoComandoFila, dados);

            var retornoDto = new RetornoDto { Resultado = true };

            switch (_tipoFila)
            {
                case TipoFila.Db:
                    retornoDto = await _filaProcessamentoDbRepository.Incluir(filaProcessamento);
                    break;

                case TipoFila.RabbitMQ:
                    _filaProcessamentoQueueRepository.Publicar(filaProcessamento);
                    break;

                default:
                    throw new NotImplementedException();
            }

            return retornoDto;
        }
    }
}
