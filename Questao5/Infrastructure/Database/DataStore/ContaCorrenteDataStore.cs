using Dapper;
using Questao5.Application.Commands.ContaCorrente;
using Questao5.Domain.Entities.ContaCorrente.Interfaces;
using System.Net;

namespace Questao5.Infrastructure.Database.DataStore
{
    public class ContaCorrenteDataStore : IContaCorrenteDataStore
    {
        private readonly IDataContext _dataContext;
        public ContaCorrenteDataStore(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<MovimentacaoCommandResult> GravarMovimentacao(MovimentacaoCommandRequest command)
        {
            var validacao_conta = await ValidarConta(command.IdContaCorrente);

            if (validacao_conta == "VALID_ACCOUNT")
            {
                var id_movimento = await InserirMovimentacao(command);
                return new MovimentacaoCommandResult { Message = HttpStatusCode.OK.ToString(), IdMovimentacao = id_movimento.ToString() };
            }
            else
                return new MovimentacaoCommandResult { Message = $"{validacao_conta}"};
        }

        private async Task<string> ValidarConta(string idContaCorrente)
        {
            return await _dataContext.GetConnecion().QueryFirstOrDefaultAsync<string>(
                  SQL_VERIFICA_CONTA_VALIDA,
                  new
                  {
                      idContaCorrente = idContaCorrente
                  });
        }

        private async Task<Guid> InserirMovimentacao(MovimentacaoCommandRequest command)
        {
            var id_movimento = Guid.NewGuid();
            await _dataContext.GetConnecion().ExecuteAsync(
                  SQL_REGISTRA_DADOS_MOVIMENTACAO,
                  new
                  {
                      idmovimento = id_movimento,
                      idcontacorrente = command.IdContaCorrente,
                      datamovimento = DateTime.UtcNow.ToString("dd/MM/yyyy"),
                      tipomovimento = command.TipoMovimentacao,
                      valor = command.ValorMovimentacao
                  });
            return id_movimento;
        }

        public async Task<string> VerificarChaveIdempotencia(string chaveIdempotencia)
        {
            return await _dataContext.GetConnecion().QueryFirstOrDefaultAsync<string>(
                SQL_CONSULTA_EXISTENCIA_IDEMPOTENCIA,
                new 
                { 
                    chave_idempotencia = chaveIdempotencia 
                });
        }

        public async void GravarIdempotencia(string chaveIdempotencia, string requisicao, string resultado)
        {
            await _dataContext.GetConnecion().ExecuteAsync(
                  SQL_REGISTRA_DADOS_IDEMPOTENCIA,
                  new
                  {
                      chave_idempotencia = chaveIdempotencia,
                      requisicao = requisicao,
                      resultado = resultado,
                  });
        }
        public async Task<SaldoQueryResult> ConsultarSaldo(SaldoQueryRequest command)
        {
            var validacao_conta = await ValidarConta(command.IdContaCorrente);

            if (validacao_conta == "VALID_ACCOUNT")
            {
               return await ConsultarSaldoTotal(command);
            }
            else
                return new SaldoQueryResult { Message = $"{validacao_conta}" };
        }

        private async Task<SaldoQueryResult> ConsultarSaldoTotal(SaldoQueryRequest command)
        {
            var saldoTotal = await _dataContext.GetConnecion().QueryFirstOrDefaultAsync<SaldoQueryResult>(
                  SQL_VERIFICA_SALDO,
                  new
                  {
                      idContaCorrente = command.IdContaCorrente
                  });
            saldoTotal.DataConsulta = DateTime.Now.ToString("HH:mm");
            saldoTotal.Message = HttpStatusCode.OK.ToString();
            return saldoTotal;
        }

        private const string SQL_CONSULTA_EXISTENCIA_IDEMPOTENCIA =
        @"SELECT a.resultado 
          FROM idempotencia a
          WHERE a.chave_idempotencia = @chave_idempotencia";

        private const string SQL_REGISTRA_DADOS_MOVIMENTACAO =
        @"INSERT INTO movimento(idmovimento,idcontacorrente,datamovimento,tipomovimento,valor)
          VALUES (@idmovimento,@idcontacorrente,@datamovimento,@tipomovimento,@valor)";

        private const string SQL_REGISTRA_DADOS_IDEMPOTENCIA =
        @"INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) 
          VALUES (@chave_idempotencia,@requisicao,@resultado)";

        private const string SQL_VERIFICA_CONTA_VALIDA =
        @"SELECT CASE WHEN EXISTS (SELECT 1 FROM contacorrente WHERE idcontacorrente = @idContaCorrente) THEN
                        CASE
                            WHEN (SELECT ativo FROM contacorrente WHERE idcontacorrente = @idContaCorrente) = 0 THEN 'INACTIVE_ACCOUNT'
                            ELSE 'VALID_ACCOUNT'
                        END
                        ELSE 'INVALID_ACCOUNT'
          END AS CONTA_CORRENTE";

        private const string SQL_VERIFICA_SALDO =
        @"SELECT c.Numero AS NumeroContaCorrente,c.Nome AS Titular,
          SUM(CASE WHEN m.tipomovimento = 'C' THEN m.valor ELSE -m.valor END) AS ValorSaldo
          FROM contacorrente c
          LEFT JOIN movimento m ON c.idcontacorrente = m.idcontacorrente
          WHERE c.idcontacorrente = @idContaCorrente
          GROUP BY c.idcontacorrente;";
    }
}
