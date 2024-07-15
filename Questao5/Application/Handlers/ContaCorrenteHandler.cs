using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Questao5.Application.Commands.ContaCorrente;
using Questao5.Domain.Entities.ContaCorrente.Interfaces;
using System.Net;

namespace Questao5.Application.Handlers
{
    public class ContaCorrenteHandler : IRequestHandler<MovimentacaoCommandRequest, MovimentacaoCommandResult>,
                                        IRequestHandler<SaldoQueryRequest,SaldoQueryResult>
    {
        private readonly IContaCorrenteDataStore _contaCorrenteDataStore;

        public ContaCorrenteHandler(IContaCorrenteDataStore contaCorrenteDataStore)
        {
            _contaCorrenteDataStore = contaCorrenteDataStore;
        }

        public async Task<MovimentacaoCommandResult> Handle(MovimentacaoCommandRequest command, CancellationToken cancellationToken)
        {
            var commandValidation = command.Validate();

            if (commandValidation != "")
                return new MovimentacaoCommandResult { Message = commandValidation};

            var IdMovimentacao = await _contaCorrenteDataStore.VerificarChaveIdempotencia(command.ChaveIdempotencia);

            if (IdMovimentacao != null)
                return new MovimentacaoCommandResult { IsSucess = true, Message = HttpStatusCode.OK.ToString(), IdMovimentacao = IdMovimentacao};

            var result = await _contaCorrenteDataStore.GravarMovimentacao(command);
            _contaCorrenteDataStore.GravarIdempotencia(command.ChaveIdempotencia, command.ToString(), result.Message);
            return result;
        }

        public async Task<SaldoQueryResult> Handle(SaldoQueryRequest command, CancellationToken cancellationToken)
        {
            return await _contaCorrenteDataStore.ConsultarSaldo(command);
        }
    }
}
