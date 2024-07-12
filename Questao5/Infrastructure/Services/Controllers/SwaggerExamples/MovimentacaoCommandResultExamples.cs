using Questao5.Application.Commands.ContaCorrente;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Controllers.SwaggerExamples
{
    public class MovimentacaoCommandResultExamples : IMultipleExamplesProvider<MovimentacaoCommandResult>
    {

        public IEnumerable<SwaggerExample<MovimentacaoCommandResult>> GetExamples()
        {
            yield return SwaggerExample.Create("Retorno movimentação de crédito", new MovimentacaoCommandResult()
            {
                IdMovimentacao = "9F648A70-6C18-4E4B-B036-9A6C11D67F1C",
                Message = "Ok"
            });
        }
    }
}
