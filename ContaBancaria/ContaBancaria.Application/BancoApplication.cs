using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
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
            throw new NotImplementedException();
        }

        public async Task<RetornoViewModel> ExcluirConta(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BancosViewModel>> ListarContas()
        {
            throw new NotImplementedException();
        }

        public async Task<RetornoViewModel> Depositar(DepositoBancarioViewModel depositoBancarioViewModel)
        {
            var depositoBancarioDto = _bancoMapper.Map(depositoBancarioViewModel);

            if (depositoBancarioDto.GuidContaOrigem == null)
            {
                var debitadoTaxaBancaria = await depositoBancarioDto.Conta.DebitarTaxaBancaria(
                                                    TipoTaxaBancaria.Deposito,
                                                    depositoBancarioDto.Valor);

                if (!debitadoTaxaBancaria) return _retornoMapper.Map(debitadoTaxaBancaria);
            }

            return await Creditar(depositoBancarioDto.Conta,
                                  depositoBancarioDto.Valor,
                                  depositoBancarioDto.GuidContaOrigem);
        }

        public async Task<RetornoViewModel> Sacar(SaqueBancarioViewModel saqueBancarioViewModel)
        {
            var saqueBancarioDto = _bancoMapper.Map(saqueBancarioViewModel);

            var debitadoTaxaBancaria = await saqueBancarioDto.Conta.DebitarTaxaBancaria(
                                                TipoTaxaBancaria.Saque,
                                                saqueBancarioDto.Valor);

            if (!debitadoTaxaBancaria) return _retornoMapper.Map(debitadoTaxaBancaria);

            return await Debitar(saqueBancarioDto.Conta,
                                 saqueBancarioDto.Valor,
                                 saqueBancarioDto.GuidContaOrigem);
        }

        public async Task<RetornoViewModel> Transferir(TransferenciaBancariaViewModel transferenciaBancariaViewModel)
        {
            var transferenciaBancariaDto = _bancoMapper.Map(transferenciaBancariaViewModel);

            var contaOrigem = transferenciaBancariaDto.ContaOrigem;
            var contaDestino = transferenciaBancariaDto.ContaDestino;

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
