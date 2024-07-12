using Questao5.Application.Commands.ContaCorrente;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Controllers.SwaggerExamples
{
    public class MovimentacaoCommandExample : IMultipleExamplesProvider<MovimentacaoCommand>
    {
        public IEnumerable<SwaggerExample<MovimentacaoCommand>> GetExamples()
        {
            yield return SwaggerExample.Create("Movimentação de Conta Corrente - Crédito", new MovimentacaoCommand()
            {
               ChaveIdempotencia = "A7689C18-2377-4B7D-8D7F-0FE655C1A289",
               IdContaCorrente = "E58F9B3F-4F4F-47B0-A2E0-0E6A0F30A5AD",
               TipoMovimentacao = 'c',
               ValorMovimentacao = 300
            });
        }
    }
}
