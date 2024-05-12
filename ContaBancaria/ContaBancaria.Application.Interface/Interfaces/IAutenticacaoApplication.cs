using ContaBancaria.Application.Contracts.ViewModels.Autenticacao;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using System.Threading.Tasks;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IAutenticacaoApplication
    {
        Task<RetornoViewModel> Autenticar(AutenticacaoLoginViewModel autenticacaoLoginViewModel);
    }
}
