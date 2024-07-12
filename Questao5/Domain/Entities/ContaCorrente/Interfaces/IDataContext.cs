using System.Data;

namespace Questao5.Domain.Entities.ContaCorrente.Interfaces
{
    public interface IDataContext
    {
        public IDbConnection GetConnecion();
    }
}
