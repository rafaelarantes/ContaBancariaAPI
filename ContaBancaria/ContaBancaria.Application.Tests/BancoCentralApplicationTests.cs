using Castle.Components.DictionaryAdapter.Xml;
using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.BancoCentral;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ContaBancaria.Application.Tests
{
    public class BancoCentralApplicationTests
    {
        private readonly IBancoCentralApplication _bancoCentralApplication;

        private readonly Mock<IBancoMapper> _bancoMapperMock;
        private readonly Mock<IRetornoMapper> _retornoMapperMock;
        private readonly Mock<IBancoRepository> _bancoRepositoryMock;
        private readonly Mock<IFilaProcessamentoApplication> _filaProcessamentoApplication;

        public BancoCentralApplicationTests()
        {
            _bancoMapperMock = new Mock<IBancoMapper>();
            _retornoMapperMock = new Mock<IRetornoMapper>();
            _bancoRepositoryMock = new Mock<IBancoRepository>();
            _filaProcessamentoApplication = new Mock<IFilaProcessamentoApplication>();

            _bancoCentralApplication = new BancoCentralApplication(
                                                     bancoMapper: _bancoMapperMock.Object,
                                                     bancoRepository: _bancoRepositoryMock.Object,
                                                     filaProcessamentoApplication: _filaProcessamentoApplication.Object,
                                                     retornoMapper: _retornoMapperMock.Object);
        }

        private void Mockar_BancoMapper_Map(Banco banco)
        {
            _bancoMapperMock.Setup(b => b.Map(It.IsAny<NovoBancoViewModel>()))
                .Returns(banco);
        }

        private void Mockar_RetornoMapper_Map(RetornoViewModel retornoViewModel)
        {
            _retornoMapperMock.Setup(b => b.Map(It.IsAny<RetornoDto>()))
                .Returns(retornoViewModel);
        }

        private void Mockar_BancoRepository_Incluir(RetornoDto retornoDto)
        {
            _bancoRepositoryMock.Setup(b => b.Incluir(It.IsAny<Banco>()))
                .Returns(Task.FromResult(retornoDto));
        }

        private void Mockar_BancoRepository_Excluir(RetornoDto retornoDto)
        {
            _bancoRepositoryMock.Setup(b => b.Excluir(It.IsAny<Guid>()))
                .Returns(Task.FromResult(retornoDto));
        }

        [Fact]
        public async Task CriarBanco_Valido_RetornaPositivo()
        {
            //Arrange
            var novoBancoViewModel = new NovoBancoViewModel
            {
                NumeroBanco = 1,
                Agencia = 1111,
                Nome = "Banco teste"
            };

            var novoBanco = new Banco(novoBancoViewModel.Nome,
                                      novoBancoViewModel.NumeroBanco,
                                      novoBancoViewModel.Agencia,
                                      new List<TaxaBancaria>());

            Mockar_BancoMapper_Map(novoBanco);

            Mockar_BancoRepository_Incluir(new RetornoDto
            {
                Resultado = true
            });

            Mockar_RetornoMapper_Map(new RetornoViewModel { Resultado = true });

            //Act
            var retornoViewModel = await _bancoCentralApplication.CriarBanco(novoBancoViewModel);

            //Assert
            Assert.True(retornoViewModel.Resultado);

            _bancoRepositoryMock.Verify(b => b.Incluir(It.IsAny<Banco>()), Times.Once);
        }

        [Fact]
        public async Task CriarBanco_ErroAoIncluir_RetornaNegativo()
        {
            //Arrange
            var novoBancoViewModel = new NovoBancoViewModel
            {
                NumeroBanco = 1,
                Agencia = 1111,
                Nome = "Banco teste"
            };

            var novoBanco = new Banco(novoBancoViewModel.Nome,
                                      novoBancoViewModel.NumeroBanco,
                                      novoBancoViewModel.Agencia,
                                      new List<TaxaBancaria>());
            
            Mockar_BancoMapper_Map(novoBanco);

            Mockar_BancoRepository_Incluir(new RetornoDto
            {
                Resultado = false
            });

            Mockar_RetornoMapper_Map(new RetornoViewModel { Resultado = false });

            //Act
            var retornoViewModel = await _bancoCentralApplication.CriarBanco(novoBancoViewModel);

            //Assert
            Assert.False(retornoViewModel.Resultado);

            _bancoRepositoryMock.Verify(b => b.Incluir(It.IsAny<Banco>()), Times.Once);
        }

        [Fact]
        public async Task ExcluirBanco_RetornaPositivo()
        {
            //Arrange
            Mockar_RetornoMapper_Map(new RetornoViewModel { Resultado = true });

            Mockar_BancoRepository_Excluir(new RetornoDto
            {
                Resultado = true
            });

            //Act
            var retornoViewModel = await _bancoCentralApplication.ExcluirBanco(Guid.NewGuid());

            //Assert
            Assert.True(retornoViewModel.Resultado);

            _bancoRepositoryMock.Verify(b => b.Excluir(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task ExcluirBanco_ErroAoExcluir_RetornaNegativo()
        {
            //Arrange
            Mockar_BancoRepository_Excluir(new RetornoDto
            {
                Resultado = false
            });

            Mockar_RetornoMapper_Map(new RetornoViewModel { Resultado = false });

            //Act
            var retornoViewModel = await _bancoCentralApplication.ExcluirBanco(Guid.NewGuid());

            //Assert
            Assert.False(retornoViewModel.Resultado);

            _bancoRepositoryMock.Verify(b => b.Excluir(It.IsAny<Guid>()), Times.Once);
        }
    }
}
