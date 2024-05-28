using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using Moq;

namespace ContaBancaria.Application.Tests
{
    public class ContaApplicationTests
    {
        private readonly IContaApplication _contaApplication;

        private Mock<IBancoApplication> _bancoApplicationMock;
        private Mock<IContaMapper> _contaMapperMock;
        private Mock<IContaRepository> _contaRepositoryMock;
        private Mock<IRetornoMapper> _retornoMapperMock;
        

        public ContaApplicationTests()
        {
            _bancoApplicationMock = new Mock<IBancoApplication>();
            _contaMapperMock = new Mock<IContaMapper>();
            _contaRepositoryMock = new Mock<IContaRepository>();
            _retornoMapperMock = new Mock<IRetornoMapper>();

            _contaApplication = new ContaApplication(bancoApplication: _bancoApplicationMock.Object,
                                                     contaMapper: _contaMapperMock.Object,
                                                     contaRepository: _contaRepositoryMock.Object,
                                                     retornoMapper: _retornoMapperMock.Object);
        }
    }
}
