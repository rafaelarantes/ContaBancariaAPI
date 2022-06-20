using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Data.Helper;
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
            var cancelationToken = CancellationToken.None;

            var login = _configuration.GetSection("Login").Value;
            var autorizacao = _configuration.GetSection("Autorizacao").Value;
            var secret = _configuration.GetSection("Secret").Value;

            while (!cancelationToken.IsCancellationRequested)
            {
                var filaProcessamentos = _filaProcessamentoRepository.ListarPendenteTracking();
                if(!filaProcessamentos.Any())
                {
                    await Task.Delay(5000);
                    continue;
                }

                var token = TokenHelper.GerarToken(login, autorizacao, secret);

                foreach (var processamento in filaProcessamentos)
                {
                    try
                    {
                        var url = processamento.TipoComandoFila.GetEnumDescription();
                        var retornoDto = await Processar(url, processamento.Dados, token);
                        
                        if(!retornoDto.Resultado) processamento.ProcessadoComErro();
                        
                        processamento.Finalizado();
                    }
                    catch (Exception)
                    {
                        processamento.ProcessadoComErro();
                    }

                    await _filaProcessamentoRepository.Gravar();
                }

                _filaProcessamentoRepository.FinalizarTransacao();
            }
        }

        private async Task<RetornoDto> Processar(string url, string data, string token)
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
