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
    [ExcludeFromCodeCoverage]
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
        [SwaggerRequestExample(typeof(MovimentacaoCommand), typeof(MovimentacaoCommandExample))]
        [SwaggerResponseExample(200, typeof(MovimentacaoCommandResultExamples))]
        [ProducesResponseType(typeof(MovimentacaoCommandResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(MovimentacaoCommandResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RequestMovimentacao([FromBody] MovimentacaoCommand commandRequest)
        {
            var movimentacao = await _mediator.Send(commandRequest);

            if (movimentacao.IsSucess)
                return Ok(movimentacao);
            else
                return BadRequest(movimentacao);
        }

        [HttpGet]
        [Route("saldo")]
        [SwaggerRequestExample(typeof(SaldoCommand), typeof(SaldoCommandExample))]
        [SwaggerResponseExample(200, typeof(SaldoCommandCommandResultExamples))]
        [ProducesResponseType(typeof(SaldoCommandResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SaldoCommandResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RequestSaldo([FromQuery] SaldoCommand commandRequest)
        {
            var movimentacao = await _mediator.Send(commandRequest);

            if (movimentacao.IsSucess)
                return Ok(movimentacao);
            else
                return BadRequest(movimentacao);
        }
    }
}