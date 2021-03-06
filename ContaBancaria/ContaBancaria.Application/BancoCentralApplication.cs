using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class BancoCentralApplication : IBancoCentralApplication
    {
        private readonly IBancoMapper _bancoMapper;
        private readonly IBancoRepository _bancoRepository;
        private readonly IRetornoMapper _retornoMapper;
        private readonly IFilaProcessamentoRepository _filaProcessamentoRepository;

        public BancoCentralApplication(IBancoMapper bancoMapper,
                        IBancoRepository bancoRepository,
                        IRetornoMapper retornoMapper,
                        IFilaProcessamentoRepository filaProcessamentoRepository)
        {
            _bancoMapper = bancoMapper;
            _bancoRepository = bancoRepository;
            _retornoMapper = retornoMapper;
            _filaProcessamentoRepository = filaProcessamentoRepository;
        }

        public async Task<IEnumerable<BancosViewModel>> ListarBancos()
        {
            var bancos = await _bancoRepository.Listar();
            return _bancoMapper.Map(bancos);
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
            var filaProcessamento = new FilaProcessamento(TipoComandoFila.Deposito, dados);
            
            var retornoDto = await _filaProcessamentoRepository.Incluir(filaProcessamento);

            return _retornoMapper.Map(retornoDto);
        }
    }
}
