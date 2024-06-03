using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class BancoApplication : IBancoApplication
    {
        private readonly IBancoCentralApplication _bancoCentralApplication;
        private readonly IBancoRepository _bancoRepository;
        private readonly IRetornoMapper _retornoMapper;
        private readonly IBancoMapper _bancoMapper;
        private readonly IContaRepository _contaRepository;

        public BancoApplication(IBancoCentralApplication bancoCentralApplication, 
                                IBancoRepository bancoRepository,
                                IRetornoMapper retornoMapper,
                                IBancoMapper bancoMapper,
                                IContaRepository contaRepository)
        {
            _bancoCentralApplication = bancoCentralApplication;
            _bancoRepository = bancoRepository;
            _retornoMapper = retornoMapper;
            _bancoMapper = bancoMapper;
            _contaRepository = contaRepository;
        }

        public async Task<RetornoViewModel> CriarConta(NovaContaViewModel novaContaViewModel)
        {
            var banco = await _bancoRepository.Obter(novaContaViewModel.GuidBanco);
            var conta = new Conta(novaContaViewModel.NumeroConta, banco.Guid);

            var retornoDto = await _contaRepository.Incluir(conta);

            return _retornoMapper.Map(retornoDto.Resultado, default);
        }

        public async Task<RetornoViewModel> ExcluirConta(Guid guid)
        {
            var retornoDto = await _contaRepository.Excluir(guid);
            return _retornoMapper.Map(retornoDto.Resultado, default);
        }

        public async Task<RetornoViewModel> ListarContas()
        {
            var contas = await _contaRepository.ListarInclude();
            var contasViewModel =  _bancoMapper.Map(contas);

            return _retornoMapper.Map(contasViewModel);
        }

        public async Task<RetornoViewModel> Depositar(DepositoBancarioViewModel depositoBancarioViewModel)
        {
            if(!ValidarDeposito(depositoBancarioViewModel, out var mensagens))
                return _retornoMapper.Map(false, mensagens);

            var depositoBancarioDto = _bancoMapper.Map(depositoBancarioViewModel);

            var conta = await _contaRepository.ObterInclude(depositoBancarioDto.GuidConta);
            
            var validacaoConta = ValidarConta(conta);
            if (!validacaoConta.Resultado) return validacaoConta;

             conta.DebitarTaxaBancaria(TipoTaxaBancaria.Deposito, depositoBancarioDto.Valor);

            return await Creditar(conta, depositoBancarioDto.Valor, depositoBancarioDto.GuidContaOrigem);
        }

        public async Task<RetornoViewModel> Sacar(SaqueBancarioViewModel saqueBancarioViewModel)
        {
            var saqueBancarioDto = _bancoMapper.Map(saqueBancarioViewModel);
            
            var conta = await _contaRepository.ObterInclude(saqueBancarioDto.GuidConta);

            var validacaoConta = ValidarConta(conta);
            if (!validacaoConta.Resultado) return validacaoConta;

            conta.DebitarTaxaBancaria(TipoTaxaBancaria.Saque, saqueBancarioDto.Valor);

            return await Debitar(conta, saqueBancarioDto.Valor, saqueBancarioDto.GuidContaOrigem);
        }

        public async Task<RetornoViewModel> Transferir(TransferenciaBancariaViewModel transferenciaBancariaViewModel)
        {
            var transferenciaBancariaDto = _bancoMapper.Map(transferenciaBancariaViewModel);

            var contaOrigem = await _contaRepository.ObterInclude(transferenciaBancariaViewModel.GuidContaOrigem);
            
            var validacaoConta = ValidarConta(contaOrigem);
            if (!validacaoConta.Resultado) return validacaoConta;

            var contaDestino = await _contaRepository.ObterInclude(
                                            transferenciaBancariaViewModel.GuidContaDestino);
            
            validacaoConta = ValidarConta(contaDestino);
            if (!validacaoConta.Resultado) return validacaoConta;

            contaOrigem.DebitarTaxaBancaria(TipoTaxaBancaria.Transferencia,
                                           transferenciaBancariaDto.Valor);

            var retornoViewModel = await Debitar(contaOrigem, transferenciaBancariaDto.Valor, null);
            if (!retornoViewModel.Resultado) return retornoViewModel;

            var contabancoExterno = contaOrigem.GuidBanco != contaDestino.GuidBanco;

            if (contabancoExterno)
                return _bancoCentralApplication.Transferir(contaOrigem, contaDestino,
                                                           transferenciaBancariaDto.Valor);

            return await Creditar(contaDestino, transferenciaBancariaDto.Valor, null);
        }

        public async Task<RetornoViewModel> ReceberTransferencia(DepositoBancarioViewModel depositoBancarioViewModel)
        {
            var depositoBancarioDto = _bancoMapper.Map(depositoBancarioViewModel);
            var conta = await _contaRepository.ObterInclude(depositoBancarioDto.GuidConta);
            
            var validacaoConta = ValidarConta(conta);
            if (!validacaoConta.Resultado) return validacaoConta;

            return await Creditar(conta, depositoBancarioDto.Valor, depositoBancarioDto.GuidContaOrigem);
        }

        private async Task<RetornoViewModel> Debitar(Conta conta, decimal valor, Guid? guidContaOrigem)
        {
            var debitado = await conta.Debitar(valor, guidContaOrigem);
            if (!debitado) return _retornoMapper.Map(debitado, default);

            return await AtualizarConta();
        }

        private async Task<RetornoViewModel> Creditar(Conta conta, decimal valor, Guid? guidContaOrigem)
        {
            var creditado = await conta.Creditar(valor, guidContaOrigem);
            if (!creditado) return _retornoMapper.Map(creditado, default);

            return await AtualizarConta();
        }

        private async Task<RetornoViewModel> AtualizarConta()
        {
            var atualizado = await _contaRepository.Gravar();
            return _retornoMapper.Map(atualizado);
        }

        private bool ValidarDeposito(DepositoBancarioViewModel depositoBancarioViewModel,
                                     out List<string> mensagens)
        {
            mensagens = new List<string>();

            if (depositoBancarioViewModel.Valor <= 0)
                mensagens.Add("O valor de depósito deve ser maior do que 0");

            return !mensagens.Any();
        }

        private RetornoViewModel ValidarConta(Conta conta)
        {
            if (conta == null)
            {
                return _retornoMapper.Map(false, new List<string>
                {
                    "Conta não cadastrada",
                });

            }

            return _retornoMapper.Map(true, default);
        }
    }
}
