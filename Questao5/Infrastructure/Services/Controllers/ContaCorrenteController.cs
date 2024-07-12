using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.ContaCorrente;
using Questao5.Infrastructure.Services.Controllers.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly ILogger<ContaCorrenteController> _logger;
        private readonly IMediator _mediator;

        public ContaCorrenteController(ILogger<ContaCorrenteController> logger,IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("movimentacao")]
        [SwaggerRequestExample(typeof(MovimentacaoCommandRequest), typeof(MovimentacaoCommandExample))]
        [SwaggerResponseExample(200, typeof(MovimentacaoCommandResultExamples))]
        [ProducesResponseType(typeof(MovimentacaoCommandResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RequestMovimentacao([FromBody] MovimentacaoCommandRequest commandRequest)
        {
            var movimentacao = await _mediator.Send(commandRequest);

            return movimentacao.IsSucess ? Ok(movimentacao) : BadRequest(movimentacao);
        }

        [HttpGet]
        [Route("saldo")]
        [SwaggerResponseExample(200, typeof(SaldoCommandCommandResultExamples))]
        [ProducesResponseType(typeof(SaldoQueryResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RequestSaldo([FromQuery] SaldoQueryRequest commandRequest)
        {
            var movimentacao = await _mediator.Send(commandRequest);

            return movimentacao.IsSucess ? Ok(movimentacao) : BadRequest(movimentacao);
        }
    }
}