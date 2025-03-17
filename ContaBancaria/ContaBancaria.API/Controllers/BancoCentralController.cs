using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContaBancaria.API.Controllers
{
    [Authorize(Roles = "BancoCentral, Adm")]
    [Route("api/[controller]")]
    [ApiController]
    public class BancoCentralController : ControllerBase
    {
        private readonly IBancoCentralApplication _bancoCentralApplication;

        public BancoCentralController(IBancoCentralApplication bancoCentralApplication)
        {
            _bancoCentralApplication = bancoCentralApplication;
        }

        [HttpGet("ObterSelecaoTaxaBancaria")]
        public RetornoViewModel ObterSelecaoTaxaBancaria() => _bancoCentralApplication.ObterSelecaoTaxaBancaria();

        [HttpGet("ListarBancos")]
        public async Task<RetornoViewModel> ListarBancos() => await _bancoCentralApplication.ListarBancos();

        [HttpPost("CriarBanco")]
        public async Task<RetornoViewModel> CriarBanco([FromBody] NovoBancoViewModel novoBancoViewModel)
        {
            return await _bancoCentralApplication.CriarBanco(novoBancoViewModel);
        }

        [HttpDelete("ExcluirBanco")]
        public async Task<RetornoViewModel> ExcluirBanco(Guid guid)
        {
            return await _bancoCentralApplication.ExcluirBanco(guid);
        }
    }
}
