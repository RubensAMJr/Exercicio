using Questao5.Application.Commands.ContaCorrente;

namespace Questao5.Domain.Entities.ContaCorrente.Interfaces
{
    public interface IContaCorrenteDataStore
    {
        Task<MovimentacaoCommandResult> GravarMovimentacao(MovimentacaoCommand command);
        Task<string> VerificarChaveIdempotencia(string chaveIdempotencia);

        void GravarIdempotencia(string chaveIdempotencia, string requisicao, string resultado);
        Task<SaldoCommandResult> ConsultarSaldo(SaldoCommand command);
    }
}
