using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using System;
using System.Collections.Generic;
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
            var conta = new Conta(novaContaViewModel.NumeroConta, banco);

            var retornoDto = await _contaRepository.Incluir(conta);

            return _retornoMapper.Map(retornoDto.Resultado);
        }

        public async Task<RetornoViewModel> ExcluirConta(Guid guid)
        {
            var retornoDto = await _contaRepository.Excluir(guid);
            return _retornoMapper.Map(retornoDto.Resultado);
        }

        public async Task<IEnumerable<ContaViewModel>> ListarContas()
        {
            var contas = await _contaRepository.Listar();
            return _bancoMapper.Map(contas);
        }

        public async Task<RetornoViewModel> Depositar(DepositoBancarioViewModel depositoBancarioViewModel)
        {
            var depositoBancarioDto = _bancoMapper.Map(depositoBancarioViewModel);
            var conta = await _contaRepository.Obter(depositoBancarioDto.GuidConta);

            if (depositoBancarioDto.GuidContaOrigem == null)
            {
                var debitadoTaxaBancaria = await conta.DebitarTaxaBancaria(
                                                    TipoTaxaBancaria.Deposito,
                                                    depositoBancarioDto.Valor);

                if (!debitadoTaxaBancaria) return _retornoMapper.Map(debitadoTaxaBancaria);
            }

            return await Creditar(conta, depositoBancarioDto.Valor, depositoBancarioDto.GuidContaOrigem);
        }

        public async Task<RetornoViewModel> Sacar(SaqueBancarioViewModel saqueBancarioViewModel)
        {
            var saqueBancarioDto = _bancoMapper.Map(saqueBancarioViewModel);
            var conta = await _contaRepository.Obter(saqueBancarioDto.GuidConta);

            var debitadoTaxaBancaria = await conta.DebitarTaxaBancaria(TipoTaxaBancaria.Saque,
                                                                       saqueBancarioDto.Valor);

            if (!debitadoTaxaBancaria) return _retornoMapper.Map(debitadoTaxaBancaria);

            return await Debitar(conta, saqueBancarioDto.Valor, saqueBancarioDto.GuidContaOrigem);
        }

        public async Task<RetornoViewModel> Transferir(TransferenciaBancariaViewModel transferenciaBancariaViewModel)
        {
            var transferenciaBancariaDto = _bancoMapper.Map(transferenciaBancariaViewModel);

            var contaOrigem = await _contaRepository.Obter(transferenciaBancariaViewModel.GuidContaOrigem);
            var contaDestino = await _contaRepository.Obter(transferenciaBancariaViewModel.GuidContaDestino);

            var debitadoTaxaBancaria = await contaOrigem.DebitarTaxaBancaria(TipoTaxaBancaria.Transferencia,
                                                                             transferenciaBancariaDto.Valor);
            if (!debitadoTaxaBancaria) return _retornoMapper.Map(debitadoTaxaBancaria);

            var retornoViewModel = await Debitar(contaOrigem, transferenciaBancariaDto.Valor, null);
            if (!retornoViewModel.Resultado) return retornoViewModel;

            if (contaOrigem.GuidBanco == contaDestino.GuidBanco)
                return await Creditar(contaDestino, transferenciaBancariaDto.Valor, null);

            return await _bancoCentralApplication.Transferir(contaOrigem, contaDestino, transferenciaBancariaDto.Valor);
        }

        private async Task<RetornoViewModel> Debitar(Conta conta, decimal valor, Guid? guidContaOrigem)
        {
            var debitado = await conta.Debitar(valor, guidContaOrigem);
            if (!debitado) return _retornoMapper.Map(debitado);

            return await AtualizarConta(conta);
        }

        private async Task<RetornoViewModel> Creditar(Conta conta, decimal valor, Guid? guidContaOrigem)
        {
            var creditado = await conta.Creditar(valor, guidContaOrigem);
            if (!creditado) return _retornoMapper.Map(creditado);

            return await AtualizarConta(conta);
        }

        private async Task<RetornoViewModel> AtualizarConta(Conta conta)
        {
            var atualizado = await _contaRepository.Atualizar(conta);
            return _retornoMapper.Map(atualizado);
        }
    }
}
