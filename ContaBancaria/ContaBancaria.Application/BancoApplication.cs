using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class BancoApplication : IBancoApplication
    {
        private readonly IBancoMapper _bancoMapper;
        private readonly IRetornoMapper _retornoMapper;
        private readonly IBancoRepository _bancoRepository;
        private readonly IContaRepository _contaRepository;

        public BancoApplication(IBancoMapper bancoMapper,
                                IBancoRepository bancoRepository,
                                IRetornoMapper retornoMapper,
                                IContaRepository contaRepository)
        {
            _bancoMapper = bancoMapper;
            _bancoRepository = bancoRepository;
            _retornoMapper = retornoMapper;
            _contaRepository = contaRepository;
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
            var saqueRealizado = await contaOrigem.Debitar(valor);
            if (!saqueRealizado) return _retornoMapper.Map(saqueRealizado);

            var depositoRealizado = await contaDestino.Creditar(valor);
            if (!depositoRealizado) return _retornoMapper.Map(depositoRealizado);

            return await AtualizarContas(contaOrigem, contaDestino);
        }

        public async Task<RetornoViewModel> Depositar(Conta conta, decimal valor)
        {
            var creditado = await conta.Creditar(valor);
            if (!creditado) return _retornoMapper.Map(creditado);

            return await AtualizarConta(conta);
        }

        public async Task<RetornoViewModel> Sacar(Conta conta, decimal valor)
        {
            var debitado = await conta.Debitar(valor);
            if (!debitado) return _retornoMapper.Map(debitado);

            return await AtualizarConta(conta);
        }

        private async Task<RetornoViewModel> AtualizarConta(Conta conta)
        {
            var atualizado = await _bancoRepository.AtualizarConta(conta);
            return _retornoMapper.Map(atualizado);
        }

        private async Task<RetornoViewModel> AtualizarContas(Conta contaOrigem, Conta contaDestino)
        {
            var retornoViewModel = await AtualizarConta(contaOrigem);
            if (!retornoViewModel.Resultado) return retornoViewModel;

            return await AtualizarConta(contaDestino);
        }
    }
}
