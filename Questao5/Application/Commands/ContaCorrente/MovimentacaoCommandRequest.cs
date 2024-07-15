using MediatR;
using System.Text;
using System.Text.Json;

namespace Questao5.Application.Commands.ContaCorrente
{
    public class MovimentacaoCommandRequest : IRequest<MovimentacaoCommandResult>
    {
        public string ChaveIdempotencia { get; set; }
        public string IdContaCorrente { get; set; }
        public decimal ValorMovimentacao { get; set; }
        public char TipoMovimentacao { get; set; }
        
        public string Validate()
        {
            var errors = new StringBuilder();

            if (TipoMovimentacao != 'C' && TipoMovimentacao != 'D')
                errors.Append("INVALID_TYPE");
            if ((TipoMovimentacao == 'C' && ValorMovimentacao < 0.00M) ||
                 TipoMovimentacao == 'D' && ValorMovimentacao > 0.00M)
                errors.Append("INVALID_VALUE");

            return errors.ToString();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(new
            {
                IdContaCorrente = IdContaCorrente,
                ValorMovimentacao = ValorMovimentacao,
                TipoMovimentacao = TipoMovimentacao
            });
        }
    }
}
