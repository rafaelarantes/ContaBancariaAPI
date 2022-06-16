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

        [HttpGet]
        public async Task<ExtratoViewModel> VisualizarExtrato(Guid guid) 
            => await _contaApplication.VisualizarExtrato(guid);

        [HttpPost]
        public async Task<RetornoViewModel> Depositar([FromBody] DepositoViewModel depositoViewModel)
        {
            return await _contaApplication.Depositar(depositoViewModel);
        }

        [HttpPost]
        public async Task<RetornoViewModel> Sacar([FromBody] SaqueViewModel saqueViewModel)
        {
            return await _contaApplication.Sacar(saqueViewModel);
        }

        [HttpPost]
        public async Task<RetornoViewModel> Transferir([FromBody] TransferenciaViewModel transferenciaViewModel)
        {
            return await _contaApplication.Transferir(transferenciaViewModel);
        }
    }
}
