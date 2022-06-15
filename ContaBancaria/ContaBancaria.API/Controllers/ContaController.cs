using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using Microsoft.AspNetCore.Mvc;

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
        public ExtratoViewModel VisualizarExtrato() => _contaApplication.VisualizarExtrato();

        [HttpPost]
        public RetornoViewModel Depositar([FromBody] DepositoViewModel depositoViewModel)
        {
            return _contaApplication.Depositar(depositoViewModel);
        }

        [HttpPost]
        public RetornoViewModel Sacar([FromBody] SaqueViewModel saqueViewModel)
        {
            return _contaApplication.Sacar(saqueViewModel);
        }

        [HttpPost]
        public RetornoViewModel Transferir([FromBody] TransferenciaViewModel transferenciaViewModel)
        {
            return _contaApplication.Transferir(transferenciaViewModel);
        }
    }
}
