using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Questao5.Application.Commands.ContaCorrente
{
    public class MovimentacaoCommandResult : ActionResult
    {
        [JsonIgnore]
        public bool IsSucess {get;set;}
        public string Message { get; set; }
        public string IdMovimentacao { get; set; }
    }
}
