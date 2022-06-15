using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace ContaBancaria.Application
{
    public class BancoApplication : IBancoApplication
    {
        private readonly IBancoMapper _bancoMapper;
        private readonly IBancoRepository _bancoRepository; 

        public BancoApplication(IBancoMapper bancoMapper, IBancoRepository bancoRepository)
        {
            _bancoMapper = bancoMapper;
            _bancoRepository = bancoRepository;
        }

        public async Task<ListarBancosViewModel> ListarBancos()
        {
            var bancos = await _bancoRepository.Listar();

            return _bancoMapper.Map(bancos);
        }

        public async Task<RetornoViewModel> CriarBanco(NovoBancoViewModel novoBancoViewModel)
        {
            var banco = _bancoMapper.Map(novoBancoViewModel);

            var retornoDto = await _bancoRepository.Incluir(banco);

            return _bancoMapper.Map(retornoDto);
        }

        public async Task<RetornoViewModel> ExcluirBanco(Guid guid)
        {
            var retornoDto = await _bancoRepository.Excluir(guid);

            return _bancoMapper.Map(retornoDto);
        }

        public async Task<RetornoViewModel> Transferir(Conta contaOrigem, Conta contaDestino, decimal valor)
        {
            var retornoViewModel = new RetornoViewModel();
            
            var saqueRealizado = await contaOrigem.Debitar(valor);

            if (!saqueRealizado) return retornoViewModel;

            var depositoRealizado = await contaDestino.Creditar(valor);

            retornoViewModel.Resultado = depositoRealizado;

            return retornoViewModel;
        }

        public async Task<RetornoViewModel> Depositar(Conta conta, decimal valor)
        {
            var retornoViewModel = new RetornoViewModel();
           
            retornoViewModel.Resultado = await conta.Creditar(valor);

            return retornoViewModel;
        }

        public async Task<RetornoViewModel> Sacar(Conta conta, decimal valor)
        {
            var retornoViewModel = new RetornoViewModel();

            retornoViewModel.Resultado = await conta.Debitar(valor);

            return retornoViewModel;
        }

        public async Task<ExtratoViewModel> VisualizarExtrato(Conta conta)
        {
            throw new NotImplementedException();
        }


    }
}
