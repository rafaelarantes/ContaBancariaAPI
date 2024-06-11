using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Data.Helper;
using ContaBancaria.ServiceBus.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ContaBancaria.Dominio.Helper;

namespace ContaBancaria.ServiceBus
{
    public class BusDb : IBusDb
    {
        private readonly IConfiguration _configuration;
        private readonly IFilaProcessamentoDbRepository _filaProcessamentoDbRepository;
        
        private HttpClient _httpClient;

        public BusDb(IConfiguration configuration,
                     IFilaProcessamentoDbRepository filaProcessamentoDbRepository)
        {
            _configuration = configuration;
            _filaProcessamentoDbRepository = filaProcessamentoDbRepository;
        }

        public void CriarClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Executar()
        {
            var cancelationToken = CancellationToken.None;

            var login = _configuration.GetSection("Login").Value;
            var autorizacao = _configuration.GetSection("Autorizacao").Value;
            var secret = _configuration.GetSection("Secret").Value;

            while (!cancelationToken.IsCancellationRequested)
            {
                var filaProcessamentos = _filaProcessamentoDbRepository.ListarPendenteTracking();
                if (!filaProcessamentos.Any())
                {
                    _filaProcessamentoDbRepository.Rollback();
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

                        if (!retornoDto.Resultado)
                            processamento.ProcessadoComErro();
                        processamento.Finalizado();

                        await _filaProcessamentoDbRepository.Gravar(processamento);
                        _filaProcessamentoDbRepository.Commit();
                    }
                    catch (Exception)
                    {
                        processamento.ProcessadoComErro();
                        _filaProcessamentoDbRepository.Rollback();
                    }
                }
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
