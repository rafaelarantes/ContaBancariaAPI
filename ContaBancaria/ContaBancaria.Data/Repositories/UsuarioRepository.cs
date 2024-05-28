using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ContaBancaria.Data.Repositories
{
    public class UsuarioRepository : DbRepository, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Usuario> Obter(string login, string senha)
        {
            return await _bancoContext.Set<Usuario>()
                                      .AsNoTracking()
                                      .SingleOrDefaultAsync(u => u.Login == login && u.Senha == senha);

        }
    }
}
