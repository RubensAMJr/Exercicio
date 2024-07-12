using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.ContaCorrente;
using Questao5.Domain.Entities.ContaCorrente.Interfaces;
using System.Net;

namespace Questao5.Application.Handlers
{
    public class ContaCorrenteCommandHandler : IRequestHandler<MovimentacaoCommand, MovimentacaoCommandResult>,
                                               IRequestHandler<SaldoCommand,SaldoCommandResult>
    {
        private readonly IContaCorrenteDataStore _contaCorrenteDataStore;

        public ContaCorrenteCommandHandler(IContaCorrenteDataStore contaCorrenteDataStore)
        {
            _contaCorrenteDataStore = contaCorrenteDataStore;
        }

        public async Task<MovimentacaoCommandResult> Handle(MovimentacaoCommand command, CancellationToken cancellationToken)
        {
            if (command.TipoMovimentacao != 'C' && command.TipoMovimentacao != 'D')
                return new MovimentacaoCommandResult { Message = "INVALID_TYPE" };
            else if (command.ValorMovimentacao < 1)
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
                return new MovimentacaoCommandResult { Message = HttpStatusCode.OK.ToString(),IdMovimentacao = IdMovimentacao};
            }
        }

        public async Task<SaldoCommandResult> Handle(SaldoCommand command, CancellationToken cancellationToken)
        {
            return await _contaCorrenteDataStore.ConsultarSaldo(command);
        }
    }
}
