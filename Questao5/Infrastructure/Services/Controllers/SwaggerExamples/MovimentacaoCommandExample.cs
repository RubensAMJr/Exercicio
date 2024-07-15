using Questao5.Application.Commands.ContaCorrente;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Controllers.SwaggerExamples
{
    public class MovimentacaoCommandExample : IMultipleExamplesProvider<MovimentacaoCommandRequest>
    {
        public IEnumerable<SwaggerExample<MovimentacaoCommandRequest>> GetExamples()
        {
            yield return SwaggerExample.Create("Movimentação de Conta Corrente - Crédito", new MovimentacaoCommandRequest()
            {
               ChaveIdempotencia = Guid.NewGuid().ToString(),
               IdContaCorrente = Guid.NewGuid().ToString(),
               TipoMovimentacao = 'C',
               ValorMovimentacao = 300
            });

            yield return SwaggerExample.Create("Movimentação de Conta Corrente - Débito", new MovimentacaoCommandRequest()
            {
                ChaveIdempotencia = Guid.NewGuid().ToString(),
                IdContaCorrente = Guid.NewGuid().ToString(),
                TipoMovimentacao = 'D',
                ValorMovimentacao = -300
            });
        }
    }
}
