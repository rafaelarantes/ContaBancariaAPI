using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Enums;
using ContaBancaria.ServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ContaBancaria.ServiceBus
{
    public class Bus
    {
        private readonly IFilaProcessamentoQueueRepository _filaProcessamentoRepository;
        private readonly IFilaProcessamentoDbRepository _filaProcessamentoDbRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly TipoFila _tipoFila;
        private readonly IBusDb _busDb;
        private readonly IBusRabbitMQ _busRabbitMQ;

        public Bus(IFilaProcessamentoQueueRepository filaProcessamentoRepository,
                   IFilaProcessamentoDbRepository filaProcessamentoDbRepository,
                   IConfiguration configuration,
                   IBusDb busDb,
                   IBusRabbitMQ busRabbitMQ)
        {
            _filaProcessamentoRepository = filaProcessamentoRepository;
            _filaProcessamentoDbRepository = filaProcessamentoDbRepository;
            _configuration = configuration;
            _busDb = busDb;
            _busRabbitMQ = busRabbitMQ;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetSection("BaseAddress").Value);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            _tipoFila = Enum.Parse<TipoFila>(_configuration.GetSection("Messaging").Value);
        }

        public async Task Executar()
        {
            switch (_tipoFila)
            {
                case TipoFila.Db:
                    _busDb.CriarClient(_httpClient);
                    await _busDb.Executar();
                    break;

                case TipoFila.RabbitMQ:
                    _busRabbitMQ.CriarClient(_httpClient);
                    _busRabbitMQ.Executar();
                    break;
            }

        }
    }
}
