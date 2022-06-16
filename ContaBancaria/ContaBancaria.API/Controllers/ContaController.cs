using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ContaBancaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly IContaApplication _contaApplication;

        public ContaController(IContaApplication contaApplication)
        {
            _contaApplication = contaApplication;
        }

        [HttpGet("{guid}")]
        public async Task<ExtratoViewModel> VisualizarExtrato(Guid guid) 
            => await _contaApplication.VisualizarExtrato(guid);

        [HttpPost("Depositar")]
        public async Task<RetornoViewModel> Depositar([FromBody] DepositoViewModel depositoViewModel)
        {
            return await _contaApplication.Depositar(depositoViewModel);
        }

        [HttpPost("Sacar")]
        public async Task<RetornoViewModel> Sacar([FromBody] SaqueViewModel saqueViewModel)
        {
            return await _contaApplication.Sacar(saqueViewModel);
        }

        [HttpPost("Transferir")]
        public async Task<RetornoViewModel> Transferir([FromBody] TransferenciaViewModel transferenciaViewModel)
        {
            return await _contaApplication.Transferir(transferenciaViewModel);
        }
    }
}
