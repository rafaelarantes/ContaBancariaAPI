using ContaBancaria.Dominio.Entidades;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Contracts.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Obter(string login, string senha);
    }
}
