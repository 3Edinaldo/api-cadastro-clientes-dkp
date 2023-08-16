using API.Cadastro.Clientes.DKP.Data.Interface;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace API.Cadastro.Clientes.DKP
{
    public class ConnectionString : IConnectionString
    {
        private readonly IConfiguration _configuracoes;

        public ConnectionString(IConfiguration config)
        {
            _configuracoes = config;
        }

        public IDbConnection Connection()
        {
            var conexao = _configuracoes.GetConnectionString("BaseDKP");
            return new SqlConnection(conexao);
        }
    }
}