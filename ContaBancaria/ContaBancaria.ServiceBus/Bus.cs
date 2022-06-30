using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Data.Helper;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using ContaBancaria.Dominio.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContaBancaria.ServiceBus
{
    public class Bus
    {
        private readonly IFilaProcessamentoRepository _filaProcessamentoRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public Bus(IFilaProcessamentoRepository filaProcessamentoRepository,
                   IConfiguration configuration)
        {

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(configuration.GetSection("BaseAddress").Value);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _filaProcessamentoRepository = filaProcessamentoRepository;
            _configuration = configuration;
        }

        public async Task Executar()
        {
            _filaProcessamentoRepository.Receber(TipoComandoFila.Deposito, Processar());
        }

        private Action<TipoComandoFila, string> Processar()
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
