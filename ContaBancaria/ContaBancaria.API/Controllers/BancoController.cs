using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("Contas")]
        public async Task<IEnumerable<ContaViewModel>> ListarContas() => await _bancoApplication.ListarContas();

        [HttpPost("CriarConta")]
        public async Task<RetornoViewModel> CriarConta([FromBody] NovaContaViewModel novaContaViewModel)
        {
            return await _bancoApplication.CriarConta(novaContaViewModel);
        }

        [HttpDelete("ExcluirConta/{guid}")]
        public async Task<RetornoViewModel> ExcluirConta(Guid guid)
        {
            return await _bancoApplication.ExcluirConta(guid);
        }

    }
}
