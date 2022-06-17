using ContaBancaria.Application.Contracts.Interfaces;
using ContaBancaria.Application.Contracts.Interfaces.Mappers;
using ContaBancaria.Application.Contracts.ViewModels.Banco;
using ContaBancaria.Application.Contracts.ViewModels.Conta;
using ContaBancaria.Application.Mappers;
using ContaBancaria.Data.Contracts.Repositories.Interfaces;
using ContaBancaria.Data.Dtos;
using ContaBancaria.Dominio.Entidades;
using ContaBancaria.Dominio.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ContaBancaria.Application.Tests
{
    public class BancoApplicationTests
    {
        private readonly IBancoApplication _bancoApplication;

        private readonly Mock<IBancoCentralApplication> _bancoCentralAplicationMock;
        private readonly Mock<IRetornoMapper> _retornoMapperMock;
        private readonly Mock<IBancoRepository> _bancoRepositoryMock;

        public BancoApplicationTests()
        {
            _bancoCentralAplicationMock = new Mock<IBancoCentralApplication>();
            _retornoMapperMock = new Mock<IRetornoMapper>();
            _bancoRepositoryMock = new Mock<IBancoRepository>();

            _bancoApplication = new BancoApplication(bancoCentralApplication: _bancoCentralAplicationMock.Object,
                                                     retornoMapper: _retornoMapperMock.Object,
                                                     bancoRepository: _bancoRepositoryMock.Object);
        }

        private void Mockar_RetornoMapper_Map(RetornoViewModel retornoViewModel)
        {
            _retornoMapperMock.Setup(b => b.Map(It.IsAny<RetornoDto>()))
                .Returns(retornoViewModel);
        }

        private void Mockar_RetornoMapper_MapBool(RetornoViewModel retornoViewModel)
        {
            _retornoMapperMock.Setup(b => b.Map(It.IsAny<bool>()))
                .Returns(retornoViewModel);
        }

        private void Mockar_BancoRepository_AtualizarConta(RetornoDto retornoDto)
        {
            _bancoRepositoryMock.Setup(b => b.AtualizarConta(It.IsAny<Conta>()))
                .Returns(Task.FromResult(retornoDto));
        }

        private void Mockar_AtualizarConta(Conta conta)
        {
            Mockar_BancoRepository_AtualizarConta(new RetornoDto
            {
                Resultado = true
            });

            Mockar_RetornoMapper_Map(new RetornoViewModel { Resultado = true });
        }

        private void Mockar_BancoCentralApplication_Transferir(RetornoViewModel retornoViewModel)
        {
            _bancoCentralAplicationMock.Setup(b => b.Transferir(It.IsAny<Conta>(), It.IsAny<Conta>(), It.IsAny<decimal>()))
                .Returns(Task.FromResult(retornoViewModel));
        }

        private Conta CriarConta()
        {
            var banco = new Banco("Banco teste", 1, 1111, new List<TaxaBancaria>());
            return new Conta(111111, banco);
        }

        private decimal CalcularSaldoExtrato(Conta conta)
        {
            var valoresCredito = conta.Extrato.Where(e => e.TipoOperacao == TipoOperacaoConta.Credito)
                                              .Sum(e => e.Valor);

            var valoresDebito = conta.Extrato.Where(e => e.TipoOperacao == TipoOperacaoConta.Debito ||
                                                         e.TipoOperacao == TipoOperacaoConta.TaxaBancaria)
                                             .Sum(e => e.Valor);

            var saldoExtrato = valoresCredito - valoresDebito;
            return saldoExtrato;
        }
                
        [Fact]
        public async Task Depositar_DepositoValido_SaldoEExtratoDevemContemOValor()
        {
            //Arrange
            const decimal VALOR_DEPOSITO = 1000M;

            var conta = CriarConta();
            Mockar_AtualizarConta(conta);

            //Act
            var retornoViewModel = await _bancoApplication.Depositar(conta, VALOR_DEPOSITO);

            //Assert
            Assert.True(retornoViewModel.Resultado);

            Assert.Equal(conta.Saldo, VALOR_DEPOSITO);
            Assert.Equal(conta.Extrato.Sum(e => e.Valor), VALOR_DEPOSITO);

            _bancoRepositoryMock.Verify(b => b.AtualizarConta(It.IsAny<Conta>()), Times.Once);
        }

        [Fact]
        public async Task Sacar_SaqueValido_SaldoEExtratoDevemContemOValor()
        {
            //Arrange
            const decimal VALOR_DEPOSITO = 1000M;
            const decimal VALOR_SAQUE = 50M;
            const decimal SALDO = VALOR_DEPOSITO - VALOR_SAQUE;

            var conta = CriarConta();
            Mockar_AtualizarConta(conta);
            Mockar_RetornoMapper_MapBool(new RetornoViewModel { Resultado = true });

            //Act
            await _bancoApplication.Depositar(conta, VALOR_DEPOSITO);
            var retornoViewModel = await _bancoApplication.Sacar(conta, VALOR_SAQUE);

            var saldoExtrato = CalcularSaldoExtrato(conta);

            //Assert
            Assert.True(retornoViewModel.Resultado);

            Assert.Equal(conta.Saldo, SALDO);
            Assert.Equal(saldoExtrato, SALDO);

            _bancoRepositoryMock.Verify(b => b.AtualizarConta(It.IsAny<Conta>()), Times.Exactly(2));
        }

        [Fact]
        public async Task Sacar_ValorMaiorDoquePossui_NaoPermitir()
        {
            //Arrange
            const decimal VALOR_DEPOSITO = 1000M;
            const decimal VALOR_SAQUE = 1500M;
            const decimal SALDO = VALOR_DEPOSITO;

            var conta = CriarConta();
            Mockar_RetornoMapper_MapBool(new RetornoViewModel { Resultado = false });

            //Act
            await _bancoApplication.Depositar(conta, VALOR_DEPOSITO);
            var retornoViewModel = await _bancoApplication.Sacar(conta, VALOR_SAQUE);

            var saldoExtrato = CalcularSaldoExtrato(conta);

            //Assert
            Assert.False(retornoViewModel.Resultado);

            Assert.Equal(conta.Saldo, SALDO);
            Assert.Equal(saldoExtrato, SALDO);

            _bancoRepositoryMock.Verify(b => b.AtualizarConta(It.IsAny<Conta>()), Times.Once);
        }

        [Fact]
        public async Task Transferir_TransferirParaMesmoBanco_SaldoEExtratoDevemContemOValorTransferido()
        {
            //Arrange
            const decimal VALOR_DEPOSITO = 1000M;
            const decimal VALOR_TRANSFERIDO = 200M;
            const decimal SALDO_CONTA_ORIGEM = VALOR_DEPOSITO - VALOR_TRANSFERIDO;
            const decimal SALDO_CONTA_DESTINO = VALOR_TRANSFERIDO;

            var bancoOrigem = new Banco("Banco teste", 1, 1111, new List<TaxaBancaria>());
            var contaOrigem = new Conta(111111, bancoOrigem);
            var contaDestino = new Conta(211112, bancoOrigem);

            Mockar_AtualizarConta(contaOrigem);
            Mockar_RetornoMapper_MapBool(new RetornoViewModel { Resultado = true });

            //Act
            await _bancoApplication.Depositar(contaOrigem, VALOR_DEPOSITO);
            var retornoViewModel = await _bancoApplication.Transferir(contaOrigem, contaDestino, VALOR_TRANSFERIDO);

            var saldoExtratoContaOrigem = CalcularSaldoExtrato(contaOrigem);
            var saldoExtratoContaDestino = CalcularSaldoExtrato(contaDestino);

            //Assert
            Assert.True(retornoViewModel.Resultado);

            Assert.Equal(contaOrigem.Saldo, SALDO_CONTA_ORIGEM);
            Assert.Equal(saldoExtratoContaOrigem, SALDO_CONTA_ORIGEM);

            Assert.Equal(contaDestino.Saldo, SALDO_CONTA_DESTINO);
            Assert.Equal(saldoExtratoContaDestino, SALDO_CONTA_DESTINO);

            _bancoRepositoryMock.Verify(b => b.AtualizarConta(It.IsAny<Conta>()), Times.Exactly(3));
        }

        [Fact]
        public async Task Transferir_TransferirParaBancosDiferentes_DebitoContaOrigemEEnvioParaBancoCentralContaDestino()
        {
            //Arrange
            const decimal VALOR_DEPOSITO = 1000M;
            const decimal VALOR_TRANSFERIDO = 200M;
            const decimal SALDO_CONTA_ORIGEM = VALOR_DEPOSITO - VALOR_TRANSFERIDO;
            const decimal SALDO_CONTA_DESTINO = 0;

            var bancoOrigem = new Banco("Banco teste", 1, 1111, new List<TaxaBancaria>());
            var contaOrigem = new Conta(111111, bancoOrigem);

            var bancoDestino = new Banco("Banco teste 2", 2, 2222, new List<TaxaBancaria>());
            var contaDestino = new Conta(211112, bancoDestino);

            Mockar_AtualizarConta(contaOrigem);
            Mockar_BancoCentralApplication_Transferir(new RetornoViewModel { Resultado = true });
            Mockar_RetornoMapper_MapBool(new RetornoViewModel { Resultado = true });

            //Act
            await _bancoApplication.Depositar(contaOrigem, VALOR_DEPOSITO);
            var retornoViewModel = await _bancoApplication.Transferir(contaOrigem, contaDestino, VALOR_TRANSFERIDO);
            
            var saldoExtratoContaOrigem = CalcularSaldoExtrato(contaOrigem);

            //Assert
            Assert.True(retornoViewModel.Resultado);

            Assert.Equal(contaOrigem.Saldo, SALDO_CONTA_ORIGEM);
            Assert.Equal(saldoExtratoContaOrigem, SALDO_CONTA_ORIGEM);

            _bancoRepositoryMock.Verify(b => b.AtualizarConta(It.IsAny<Conta>()), Times.Exactly(2));
            _bancoCentralAplicationMock.Verify(b => b.Transferir(It.IsAny<Conta>(),
                                                                 It.IsAny<Conta>(),
              
                                                                 It.IsAny<decimal>()), Times.Once);
        }


        [Fact]
        public async Task Transferir_ValorMaiorDoquePossui_NaoPermitir()
        {
            //Arrange
            const decimal VALOR_DEPOSITO = 1000M;
            const decimal VALOR_TRANSFERIDO = 1500M;
            const decimal SALDO_CONTA_ORIGEM = VALOR_DEPOSITO;
            const decimal SALDO_CONTA_DESTINO = 0;

            var bancoOrigem = new Banco("Banco teste", 1, 1111, new List<TaxaBancaria>());
            var contaOrigem = new Conta(111111, bancoOrigem);
            var contaDestino = new Conta(211112, bancoOrigem);

            Mockar_AtualizarConta(contaOrigem);
            Mockar_RetornoMapper_MapBool(new RetornoViewModel { Resultado = false });

            //Act
            await _bancoApplication.Depositar(contaOrigem, VALOR_DEPOSITO);
            var retornoViewModel = await _bancoApplication.Transferir(contaOrigem, contaDestino, VALOR_TRANSFERIDO);

            var saldoExtratoContaOrigem = CalcularSaldoExtrato(contaOrigem);
            var saldoExtratoContaDestino = CalcularSaldoExtrato(contaDestino);

            //Assert
            Assert.False(retornoViewModel.Resultado);

            Assert.Equal(contaOrigem.Saldo, SALDO_CONTA_ORIGEM);
            Assert.Equal(saldoExtratoContaOrigem, SALDO_CONTA_ORIGEM);

            Assert.Equal(contaDestino.Saldo, SALDO_CONTA_DESTINO);
            Assert.Equal(saldoExtratoContaDestino, SALDO_CONTA_DESTINO);

            _bancoRepositoryMock.Verify(b => b.AtualizarConta(It.IsAny<Conta>()), Times.Once);
        }

        [Fact]
        public async Task Depositar_Taxa_RefletirNoExtrato()
        {
            //deposito = 1% valor depositado
        }

        [Fact]
        public async Task Sacar_Taxa_RefletirNoExtrato()
        {
            //saque = R$ 4
        }

        [Fact]
        public async Task Transferir_Taxa_RefletirNoExtrato()
        {
            //transferencia = R$ 1
        }









    }
}
