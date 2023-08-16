using System.Data;

namespace API.Cadastro.Clientes.DKP.Data.Interface
{
    public interface IConnectionString
    {
        IDbConnection Connection();
    }
}