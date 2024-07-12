using Questao5.Application.Commands.ContaCorrente;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.Infrastructure.Services.Controllers.SwaggerExamples
{
    public class SaldoCommandCommandResultExamples : IMultipleExamplesProvider<SaldoQueryResult>
    {
        public IEnumerable<SwaggerExample<SaldoQueryResult>> GetExamples()
        {
            yield return SwaggerExample.Create("Retorno da consulta de saldo", new SaldoQueryResult()
            {
                NumeroContaCorrente = "D94B7E6A-4D44-4E2A-91F5-9DA5E8C7B7BC",
                Titular = "João da Silva",
                ValorSaldo = 350,
                DataConsulta = DateTime.Now.ToString("dd/mm/yyyy")
            });
        }
    }
}
