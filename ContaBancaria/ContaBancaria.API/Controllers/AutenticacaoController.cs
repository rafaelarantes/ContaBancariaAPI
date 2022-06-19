using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Autenticacao;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContaBancaria.API.Controllers
{
    public class AutenticacaoController : Controller
    {
        private readonly IAutenticacaoApplication _autenticacaoApplication;
        
        public AutenticacaoController(IAutenticacaoApplication autenticacaoApplication)
        {
            _autenticacaoApplication = autenticacaoApplication;
        }

        [HttpPost]
        [Route("login")]
        public async Task<string> Autenticar([FromBody] AutenticacaoLoginViewModel autenticacaoLoginViewModel)
        {
            return await _autenticacaoApplication.Autenticar(autenticacaoLoginViewModel);
        }
    }
}
