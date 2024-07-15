using Questao5.Domain.Entities.ContaCorrente.Interfaces;
using NSubstitute;
using Questao5.Application.Commands.ContaCorrente;
using NSubstitute.ReturnsExtensions;
using Questao5.Tests.Mocks;
using Questao5.Application.Handlers;

namespace Questao5.Tests.Tests
{
    public class ContaCorrenteHandlerTest
    {
        private IContaCorrenteDataStore _contaCorrenteDataSotre;
        public ContaCorrenteHandlerTest()
        {
            _contaCorrenteDataSotre = Substitute.For<IContaCorrenteDataStore>();
        }

        [Fact]
        public async void MovimentacaoCreditoCommandHandlerTest()
        {
            var movimentacaoMock = MovimentacoesMock.MovimentacaoMockSucess();
            var movimentacaoCommandRequest = new MovimentacaoCommandRequest()
            {
                ChaveIdempotencia = "D94B7E6A-4D44-4E2A-91F5-9DA5E8C7B7BC",
                IdContaCorrente = "C6A1D8B4-92B8-4E4E-8F76-6E5C3D43F92A",
                TipoMovimentacao = 'C',
                ValorMovimentacao = 300
            };

            _contaCorrenteDataSotre.VerificarChaveIdempotencia(movimentacaoCommandRequest.ChaveIdempotencia).ReturnsNull();
            _contaCorrenteDataSotre.GravarMovimentacao(movimentacaoCommandRequest).ReturnsForAnyArgs(movimentacaoMock);

            var handler = new ContaCorrenteHandler(_contaCorrenteDataSotre);

            var test = await handler.Handle(movimentacaoCommandRequest, new CancellationToken());

            Assert.True(test.IdMovimentacao != null && test.IsSucess);
        }

        [Fact]
        public async void MovimentacaoDebitoCommandHandlerTest()
        {
            var movimentacaoMock = MovimentacoesMock.MovimentacaoMockSucess();
            var movimentacaoCommandRequest = new MovimentacaoCommandRequest()
            {
                ChaveIdempotencia = "D94B7E6A-4D44-4E2A-91F5-9DA5E8C7B7BC",
                IdContaCorrente = "C6A1D8B4-92B8-4E4E-8F76-6E5C3D43F92A",
                TipoMovimentacao = 'D',
                ValorMovimentacao = -300
            };

            _contaCorrenteDataSotre.VerificarChaveIdempotencia(movimentacaoCommandRequest.ChaveIdempotencia).ReturnsNull();
            _contaCorrenteDataSotre.GravarMovimentacao(movimentacaoCommandRequest).ReturnsForAnyArgs(movimentacaoMock);

            var handler = new ContaCorrenteHandler(_contaCorrenteDataSotre);

            var test = await handler.Handle(movimentacaoCommandRequest, new CancellationToken());

            Assert.True(test.IdMovimentacao != null && test.IsSucess);
        }

        [Fact]
        public async void TipoMovimentacaoInvalidaCommandHandlerTest()
        {
            var movimentacaoMock = MovimentacoesMock.MovimentacaoMockSucess();
            var movimentacaoCommandRequest = new MovimentacaoCommandRequest()
            {
                ChaveIdempotencia = "D94B7E6A-4D44-4E2A-91F5-9DA5E8C7B7BC",
                IdContaCorrente = "C6A1D8B4-92B8-4E4E-8F76-6E5C3D43F92A",
                TipoMovimentacao = 'F',
                ValorMovimentacao = 300
            };

            _contaCorrenteDataSotre.VerificarChaveIdempotencia(movimentacaoCommandRequest.ChaveIdempotencia).ReturnsNull();
            _contaCorrenteDataSotre.GravarMovimentacao(movimentacaoCommandRequest).ReturnsForAnyArgs(movimentacaoMock);

            var handler = new ContaCorrenteHandler(_contaCorrenteDataSotre);

            var test = await handler.Handle(movimentacaoCommandRequest, new CancellationToken());

            Assert.True(test.Message.Equals("INVALID_TYPE") && !test.IsSucess);
        }

        [Fact]
        public async void MovimentacaoDebitoPositivaCommandHandlerTest()
        {
            var movimentacaoMock = MovimentacoesMock.MovimentacaoMockSucess();
            var movimentacaoCommandRequest = new MovimentacaoCommandRequest()
            {
                ChaveIdempotencia = "D94B7E6A-4D44-4E2A-91F5-9DA5E8C7B7BC",
                IdContaCorrente = "C6A1D8B4-92B8-4E4E-8F76-6E5C3D43F92A",
                TipoMovimentacao = 'D',
                ValorMovimentacao = 300
            };

            _contaCorrenteDataSotre.VerificarChaveIdempotencia(movimentacaoCommandRequest.ChaveIdempotencia).ReturnsNull();
            _contaCorrenteDataSotre.GravarMovimentacao(movimentacaoCommandRequest).ReturnsForAnyArgs(movimentacaoMock);

            var handler = new ContaCorrenteHandler(_contaCorrenteDataSotre);

            var test = await handler.Handle(movimentacaoCommandRequest, new CancellationToken());

            Assert.True(test.Message.Equals("INVALID_VALUE") && !test.IsSucess);
        }

        [Fact]
        public async void MovimentacaoCreditoNegativaCommandHandlerTest()
        {
            var movimentacaoMock = MovimentacoesMock.MovimentacaoMockSucess();
            var movimentacaoCommandRequest = new MovimentacaoCommandRequest()
            {
                ChaveIdempotencia = "D94B7E6A-4D44-4E2A-91F5-9DA5E8C7B7BC",
                IdContaCorrente = "C6A1D8B4-92B8-4E4E-8F76-6E5C3D43F92A",
                TipoMovimentacao = 'C',
                ValorMovimentacao = -300
            };

            _contaCorrenteDataSotre.VerificarChaveIdempotencia(movimentacaoCommandRequest.ChaveIdempotencia).ReturnsNull();
            _contaCorrenteDataSotre.GravarMovimentacao(movimentacaoCommandRequest).ReturnsForAnyArgs(movimentacaoMock);

            var handler = new ContaCorrenteHandler(_contaCorrenteDataSotre);

            var test = await handler.Handle(movimentacaoCommandRequest, new CancellationToken());

            Assert.True(test.Message.Equals("INVALID_VALUE") && !test.IsSucess);
        }

        [Fact]
        public async void MovimentacaoIdempotenciaExisteCommandHandlerTest()
        {
            var movimentacaoMock = MovimentacoesMock.MovimentacaoMockSucess();
            var movimentacaoCommandRequest = new MovimentacaoCommandRequest()
            {
                ChaveIdempotencia = "D94B7E6A-4D44-4E2A-91F5-9DA5E8C7B7BC",
                IdContaCorrente = "C6A1D8B4-92B8-4E4E-8F76-6E5C3D43F92A",
                TipoMovimentacao = 'C',
                ValorMovimentacao = 300
            };

            _contaCorrenteDataSotre.VerificarChaveIdempotencia(movimentacaoCommandRequest.ChaveIdempotencia).ReturnsForAnyArgs("D94B7E6A-4D44-4E2A-91F5-9DA5E8C7B7B");

            var handler = new ContaCorrenteHandler(_contaCorrenteDataSotre);

            var test = await handler.Handle(movimentacaoCommandRequest, new CancellationToken());

            Assert.True(test.IdMovimentacao != null && test.IsSucess);
        }
    }
}