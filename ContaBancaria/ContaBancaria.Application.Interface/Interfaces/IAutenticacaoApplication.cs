using ContaBancaria.Application.Contracts.ViewModels.Autenticacao;
using System.Threading.Tasks;

namespace ContaBancaria.Application.Contracts.Interfaces
{
    public interface IAutenticacaoApplication
    {
        Task<string> Autenticar(AutenticacaoLoginViewModel autenticacaoLoginViewModel);
    }
}
