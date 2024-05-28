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
    [Authorize(Roles = "BancoCentral, Banco, Adm")]
    [Route("api/[controller]")]
    [ApiController]
    public class BancoController : ControllerBase
    {
        private readonly IBancoApplication _bancoApplication; 

        public BancoController(IBancoApplication bancoApplication)
        {
            _bancoApplication = bancoApplication;
        }

        [HttpGet("ListarContas")]
        public async Task<RetornoViewModel> ListarContas() => await _bancoApplication.ListarContas();

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

        [HttpPost("ReceberTransferencia")]
        public async Task<RetornoViewModel> ReceberTransferencia([FromBody] DepositoBancarioViewModel depositoBancarioViewModel)
        {
            return await _bancoApplication.ReceberTransferencia(depositoBancarioViewModel);
        }
    }
}
