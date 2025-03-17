using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Application.Contracts.ViewModels.Shared;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class BancoCentralApplication : IBancoCentralApplication
    {
        private readonly IBancoMapper _bancoMapper;
        private readonly IBancoRepository _bancoRepository;
        private readonly IRetornoMapper _retornoMapper;
        private readonly IFilaProcessamentoApplication _filaProcessamentoApplication;

        public BancoCentralApplication(IBancoMapper bancoMapper,
                        IBancoRepository bancoRepository,
                        IRetornoMapper retornoMapper,
                        IFilaProcessamentoApplication filaProcessamentoApplication)
        {
            _bancoMapper = bancoMapper;
            _bancoRepository = bancoRepository;
            _retornoMapper = retornoMapper;
            _filaProcessamentoApplication = filaProcessamentoApplication;
        }

        public RetornoViewModel ObterSelecaoTaxaBancaria()
        {
            var retornoDto = new RetornoDto
            {
                Resultado = true,
                Data = new SelecaoTaxaBancariaViewModel
                {
                    TipoTaxaBancaria = Enum.GetValues(typeof(TipoTaxaBancaria)).Cast<TipoTaxaBancaria>().ToList()
                                            .Select(t => new SelecaoViewModel
                                            {
                                                Descricao = t.ToString(),
                                                Valor = (int)t
                                            }).ToList(),
                    TipoValorTaxaBancaria = Enum.GetValues(typeof(TipoValorTaxaBancaria)).Cast<TipoValorTaxaBancaria>().ToList()
                                                .Select(t => new SelecaoViewModel
                                                {
                                                    Descricao = t.ToString(),
                                                    Valor = (int)t
                                                }).ToList()
                }
            };

            return _retornoMapper.Map(retornoDto);
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
            var depositoViewModel = new DepositoBancarioViewModel
            {
                GuidConta = contaDestino.Guid,
                Valor = valor,
                GuidContaOrigem = contaOrigem.Guid
            };

            var dados = JsonConvert.SerializeObject(depositoViewModel);
            var retornoDto = await _filaProcessamentoApplication.Enfileirar(TipoComandoFila.Deposito, dados);

            return _retornoMapper.Map(retornoDto);
        }
    }
}
