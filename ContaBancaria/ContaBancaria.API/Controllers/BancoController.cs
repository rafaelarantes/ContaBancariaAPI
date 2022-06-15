using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContaBancaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IBancoApplication _bancoApplication; 

        public BancoController(IBancoApplication bancoApplication)
        {
            _bancoApplication = bancoApplication;
        }

        [HttpGet]
        public async Task<ListarBancosViewModel> ListarBancos() => await _bancoApplication.ListarBancos();

        [HttpPost]
        public async Task<RetornoViewModel> CriarBanco([FromBody] NovoBancoViewModel novoBancoViewModel)
        {
            return await _bancoApplication.CriarBanco(novoBancoViewModel);
        }

        [HttpDelete]
        public async Task<RetornoViewModel> ExcluirBanco(Guid guid)
        {
            return await _bancoApplication.ExcluirBanco(guid);
        }

    }
}
