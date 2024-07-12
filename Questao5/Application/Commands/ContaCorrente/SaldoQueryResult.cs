using System.Text.Json.Serialization;

namespace Questao5.Application.Commands.ContaCorrente
{
    public class SaldoQueryResult
    {
        [JsonIgnore]
        public bool IsSucess { get; set; }
        public string NumeroContaCorrente { get; set; }
        public string Titular { get; set; }
        public string DataConsulta { get; set; }
        public decimal ValorSaldo { get; set; }
        public string Message { get; set; }
    }
}
