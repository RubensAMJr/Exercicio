using MediatR;

namespace Questao5.Application.Commands.ContaCorrente
{
    public class SaldoCommand : IRequest<SaldoCommandResult>
    {
        public string IdContaCorrente { get; set; }
    }
}
