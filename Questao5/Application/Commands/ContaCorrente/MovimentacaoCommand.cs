using MediatR;
using System.Text.Json;

namespace Questao5.Application.Commands.ContaCorrente
{
    public class MovimentacaoCommand : IRequest<MovimentacaoCommandResult>
    {
        public string ChaveIdempotencia { get; set; }
        public string IdContaCorrente { get; set; }
        public decimal ValorMovimentacao { get; set; }
        public char TipoMovimentacao { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(new
            {
                IdContaCorrente = this.IdContaCorrente,
                ValorMovimentacao = this.ValorMovimentacao,
                TipoMovimentacao = this.TipoMovimentacao
            });
        }

    }
}
