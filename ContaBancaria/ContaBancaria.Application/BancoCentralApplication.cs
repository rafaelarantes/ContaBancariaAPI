using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Enums;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class BancoCentralApplication : IBancoCentralApplication
    {
        private readonly IBancoMapper _bancoMapper;
        private readonly IBancoRepository _bancoRepository;
        private readonly IRetornoMapper _retornoMapper;
        private readonly IFilaProcessamentoRepository _filaProcessamentoRepository;
        private readonly IFilaProcessamentoDbRepository _filaProcessamentoDbRepository;
        private readonly IConfiguration _configuration;
        private readonly TipoFila _tipoFila;

        public BancoCentralApplication(IBancoMapper bancoMapper,
                        IBancoRepository bancoRepository,
                        IRetornoMapper retornoMapper,
                        IFilaProcessamentoRepository filaProcessamentoRepository,
                        IFilaProcessamentoDbRepository filaProcessamentoDbRepository,
                        IConfiguration configuration)
        {
            _bancoMapper = bancoMapper;
            _bancoRepository = bancoRepository;
            _retornoMapper = retornoMapper;
            _filaProcessamentoRepository = filaProcessamentoRepository;
            _filaProcessamentoDbRepository = filaProcessamentoDbRepository;
            _configuration = configuration;

            _tipoFila = Enum.Parse<TipoFila>(_configuration.GetSection("Messaging").Value);
        }

        public async Task<RetornoViewModel> ListarBancos()
        {
            var bancos = await _bancoRepository.Listar();
            var bancoViewModel =  _bancoMapper.Map(bancos);

            return _retornoMapper.Map(bancoViewModel);
        }

        public async Task<RetornoViewModel> CriarBanco(NovoBancoViewModel novoBancoViewModel)
        {
            var banco = _bancoMapper.Map(novoBancoViewModel);

            var retornoDto = await _bancoRepository.Incluir(banco);
            return _retornoMapper.Map(retornoDto);
        }

        public async Task<RetornoViewModel> ExcluirBanco(Guid guid)
        {
            var retornoDto = await _bancoRepository.Excluir(guid);
            return _retornoMapper.Map(retornoDto);
        }

        public async Task<RetornoViewModel> Transferir(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            switch (_tipoFila)
            {
                case TipoFila.Db:
                    return await TransferirDb(contaOrigem, contaDestino, valor);

                case TipoFila.RabbitMQ:
                    return TransferirRabbitMQ(contaOrigem, contaDestino, valor);

                default:
                    throw new NotImplementedException();
            }
        }

        private RetornoViewModel TransferirRabbitMQ(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            var depositoViewModel = new DepositoBancarioViewModel
            {
                GuidConta = contaDestino.Guid,
                Valor = valor,
                GuidContaOrigem = contaOrigem.Guid
            };

            var dados = JsonConvert.SerializeObject(depositoViewModel);

            _filaProcessamentoRepository.Publicar(
                new FilaProcessamento(TipoComandoFila.Deposito, dados));

            return _retornoMapper.Map(true, default);
        }

        private async Task<RetornoViewModel> TransferirDb(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            var depositoViewModel = new DepositoBancarioViewModel
            {
                GuidConta = contaDestino.Guid,
                Valor = valor,
                GuidContaOrigem = contaOrigem.Guid
            };

            var dados = JsonConvert.SerializeObject(depositoViewModel);
            var filaProcessamento = new FilaProcessamento(TipoComandoFila.Deposito, dados);

            var retornoDto = await _filaProcessamentoDbRepository.Incluir(filaProcessamento);

            return _retornoMapper.Map(retornoDto);
        }
    }
}
