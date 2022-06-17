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
    public class BancoController : ControllerBase
    {
        private readonly IBancoApplication _bancoApplication; 

        public BancoController(IBancoApplication bancoApplication)
        {
            _bancoApplication = bancoApplication;
        }

        [HttpGet]
        public async Task<IEnumerable<BancosViewModel>> ListarContas() => await _bancoApplication.ListarContas();
    }
}
