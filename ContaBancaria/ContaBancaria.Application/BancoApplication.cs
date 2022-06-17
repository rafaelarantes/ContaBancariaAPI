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

        public BancoApplication(IBancoCentralApplication bancoCentralApplication, 
                                IBancoRepository bancoRepository,
                                IRetornoMapper retornoMapper)
        {
            _bancoCentralApplication = bancoCentralApplication;
            _bancoRepository = bancoRepository;
            _retornoMapper = retornoMapper;
        }
        public Task<IEnumerable<BancosViewModel>> ListarContas()
        {
            // _bancoRepository.Listar();

            throw new NotImplementedException();
        }

        public async Task<RetornoViewModel> Depositar(Conta conta, decimal valor, Guid? guidContaOrigem = null)
        {
            var debitadoTaxaBancaria = await conta.DebitarTaxaBancaria(TipoTaxaBancaria.Deposito);
            if (!debitadoTaxaBancaria) return _retornoMapper.Map(debitadoTaxaBancaria);

            var creditado = await conta.Creditar(valor, guidContaOrigem);
            if (!creditado) return _retornoMapper.Map(creditado);

            return await AtualizarConta(conta);
        }

        public async Task<RetornoViewModel> Sacar(Conta conta, decimal valor, Guid? guidContaOrigem = null)
        {
            var debitadoTaxaBancaria = await conta.DebitarTaxaBancaria(TipoTaxaBancaria.Saque);
            if (!debitadoTaxaBancaria) return _retornoMapper.Map(debitadoTaxaBancaria);

            var debitado = await conta.Debitar(valor, guidContaOrigem);
            if (!debitado) return _retornoMapper.Map(debitado);

            return await AtualizarConta(conta);
        }

        public async Task<RetornoViewModel> Transferir(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            var debitadoTaxaBancaria = await contaOrigem.DebitarTaxaBancaria(TipoTaxaBancaria.Deposito);
            if (!debitadoTaxaBancaria) return _retornoMapper.Map(debitadoTaxaBancaria);

            var retornoViewModel = await Sacar(contaOrigem, valor);
            if (!retornoViewModel.Resultado) return retornoViewModel;

            if (contaOrigem.GuidBanco == contaDestino.GuidBanco)
                return await Depositar(contaDestino, valor);

            return await _bancoCentralApplication.Transferir(contaOrigem, contaDestino, valor);
        }

        private async Task<RetornoViewModel> AtualizarConta(Conta conta)
        {
            var atualizado = await _bancoRepository.AtualizarConta(conta);
            return _retornoMapper.Map(atualizado);
        }
    }
}
