using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            if (command.TipoMovimentacao != 'C' && command.TipoMovimentacao != 'D')
                return new MovimentacaoCommandResult { Message = "INVALID_TYPE" };
            else if (command.ValorMovimentacao < 0.00M)
                return new MovimentacaoCommandResult { Message = "INVALID_VALUE" };

            var IdMovimentacao = await _contaCorrenteDataStore.VerificarChaveIdempotencia(command.ChaveIdempotencia);

            if (IdMovimentacao == null)
            {
                var result = await _contaCorrenteDataStore.GravarMovimentacao(command);
                _contaCorrenteDataStore.GravarIdempotencia(command.ChaveIdempotencia, command.ToString(), result.Message);
                return result;
            }
            else
            {
                return new MovimentacaoCommandResult { IsSucess = true, Message = HttpStatusCode.OK.ToString(),IdMovimentacao = IdMovimentacao};
            }
        }

        public async Task<SaldoQueryResult> Handle(SaldoQueryRequest command, CancellationToken cancellationToken)
        {
            return await _contaCorrenteDataStore.ConsultarSaldo(command);
        }
    }
}
