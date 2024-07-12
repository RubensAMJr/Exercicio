using MediatR;

namespace Questao5.Application.Commands.ContaCorrente
{
    public class SaldoQueryRequest : IRequest<SaldoQueryResult>
    {
        public string IdContaCorrente { get; set; }
    }
}
