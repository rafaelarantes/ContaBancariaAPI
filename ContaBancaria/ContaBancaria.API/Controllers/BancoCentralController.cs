using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BancoCentralController : ControllerBase
    {
        private readonly IBancoCentralApplication _bancoCentralApplication;

        public BancoCentralController(IBancoCentralApplication bancoCentralApplication)
        {
            _bancoCentralApplication = bancoCentralApplication;
        }

        [HttpGet]
        public async Task<IEnumerable<BancosViewModel>> ListarBancos() => await _bancoCentralApplication.ListarBancos();

        [HttpPost]
        public async Task<RetornoViewModel> CriarBanco([FromBody] NovoBancoViewModel novoBancoViewModel)
        {
            return await _bancoCentralApplication.CriarBanco(novoBancoViewModel);
        }

        [HttpDelete]
        public async Task<RetornoViewModel> ExcluirBanco(Guid guid)
        {
            return await _bancoCentralApplication.ExcluirBanco(guid);
        }
    }
}
