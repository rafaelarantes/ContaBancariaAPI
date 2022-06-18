using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IEnumerable<BancosViewModel>> ListarContas() => await _bancoApplication.ListarContas();
    }
}
