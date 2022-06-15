using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ContaBancaria.Application.Tests
{
    public class ContaApplicationTests
    {
        private readonly IContaApplication _contaApplication;
        private Mock<IBancoApplication> _bancoApplicationMock;

        public ContaApplicationTests()
        {
            _bancoApplicationMock = new Mock<IBancoApplication>();
            _contaApplication = new ContaApplication(_bancoApplicationMock.Object);
        }

        [Fact]
        public async Task Deposito_ValorNegativo_RetornaFalso()
        {
            var depositoViewModel = new DepositoViewModel();

            var retornoViewModel = await _contaApplication.Depositar(depositoViewModel);

            Assert.False(retornoViewModel.Resultado);
        }
    }
}
