using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities.ContaCorrente.Interfaces;
using System.Data;

namespace Questao5.Infrastructure.Database.Context
{
    public class DataContext : IDataContext
    {
        public IDbConnection GetConnecion()
        {
            return new SqliteConnection("Data Source=database.sqlite");
        }
    }
}
