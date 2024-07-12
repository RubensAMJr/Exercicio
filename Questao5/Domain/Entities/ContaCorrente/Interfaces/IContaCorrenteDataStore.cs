using Questao5.Application.Commands.ContaCorrente;

namespace Questao5.Domain.Entities.ContaCorrente.Interfaces
{
    public interface IContaCorrenteDataStore
    {
        Task<MovimentacaoCommandResult> GravarMovimentacao(MovimentacaoCommandRequest command);
        Task<string> VerificarChaveIdempotencia(string chaveIdempotencia);

        void GravarIdempotencia(string chaveIdempotencia, string requisicao, string resultado);
        Task<SaldoQueryResult> ConsultarSaldo(SaldoQueryRequest command);
    }
}
