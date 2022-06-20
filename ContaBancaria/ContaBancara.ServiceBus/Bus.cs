using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContaBancaria.Bus
{
    public class Bus
    {
        private readonly IFilaProcessamentoRepository _filaProcessamentoRepository;

        public Bus()
        {
        }

        public async Task Executar()
        {
            var cancelationToken = CancellationToken.None;

            while (!cancelationToken.IsCancellationRequested)
            {
                var filaProcessamentos = await _filaProcessamentoRepository.ListarTracking();

                foreach (var processamento in filaProcessamentos)
                {
                    try
                    {
                        var dados = processamento.Dados;
                        var tipoComandoFila = processamento.TipoComandoFila;
                    }
                    catch (Exception)
                    {
                        
                    }
                }

                await _filaProcessamentoRepository.Gravar();
            }
        }

    }
}
