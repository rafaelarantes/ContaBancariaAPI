using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Data.Helper;
using ContaBancaria.Dominio.Enums;
using ContaBancaria.ServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ContaBancaria.Dominio.Helper;

namespace ContaBancaria.ServiceBus
{
    public class BusRabbitMQ : IBusRabbitMQ
    {
        private readonly IConfiguration _configuration;
        private readonly IFilaProcessamentoQueueRepository _filaProcessamentoRepository;

        private HttpClient _httpClient;

        public BusRabbitMQ(IConfiguration configuration,
                           IFilaProcessamentoQueueRepository filaProcessamentoRepository)
        {
            _configuration = configuration;
            _filaProcessamentoRepository = filaProcessamentoRepository;
        }


        public void CriarClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Executar()
        {
            _filaProcessamentoRepository.Receber(TipoComandoFila.Deposito, ProcessarRabbitMQ());
        }

        private Action<TipoComandoFila, string> ProcessarRabbitMQ()
        {
            var login = _configuration.GetSection("Login").Value;
            var autorizacao = _configuration.GetSection("Autorizacao").Value;
            var secret = _configuration.GetSection("Secret").Value;

            return async (TipoComandoFila tipoComandoFila, string dados) =>
            {
                var token = TokenHelper.GerarToken(login, autorizacao, secret);

                var url = tipoComandoFila.GetEnumDescription();
                var retornoDto = await ProcessarPost(url, dados, token);
            };
        }

        private async Task<RetornoDto> ProcessarPost(string url, string data, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var resultado = await _httpClient.PostAsync(url, content);

            Console.WriteLine($"{url} - {resultado.StatusCode}");

            return new RetornoDto
            {
                Resultado = resultado.IsSuccessStatusCode
            };
        }
    }
}
