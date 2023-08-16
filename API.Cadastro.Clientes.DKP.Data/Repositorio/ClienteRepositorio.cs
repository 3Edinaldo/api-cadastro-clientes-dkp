using API.Cadastro.Clientes.DKP.Data.Helpers;
using API.Cadastro.Clientes.DKP.Data.Interface;
using API.Cadastro.Clientes.DKP.Data.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.Cadastro.Clientes.DKP.Data.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private const bool ok = true;
        private readonly IConnectionString _connection;
        private IDbConnection db;

        public ClienteRepositorio(IConnectionString connection)
        {
            _connection = connection;
            db = _connection.Connection();
        }

        public async Task<ClienteModel> CriarCliente(ClienteModel cliente)
        {
            var sql = "INSERT INTO TB_Cliente ([CNPJ], [RazaoSocial], [NomeFantasia], [DataInicio], [Status]) " +
                     $"VALUES(@CNPJ,@RazaoSocial,@NomeFantasia,GETDATE(),@Status)";

            try
            {
                int affectedRows = db.Execute(sql, cliente);

                if (affectedRows == 0)
                    throw new Exception("Não foi possível efetuar o registro.");

                return await ObterCliente(cliente.CNPJ);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        public async Task<ClienteModel> ObterCliente(int id)
        {
            var sql = $"SELECT * FROM TB_Cliente WHERE Id = {id}";

            return await db.QuerySingleOrDefaultAsync<ClienteModel>(sql);
        }

        public async Task<ClienteModel> ObterCliente(string cnpj)
        {
            var sql = $"SELECT * FROM TB_Cliente WHERE CNPJ = {cnpj}";

            return await db.QuerySingleOrDefaultAsync<ClienteModel>(sql);
        }

        public List<ClienteModel> ObterClientes(bool? ativo)
        {
            try
            {
                string status = (ativo.HasValue ? ativo.Value.ToInt().ToString() : "null");
                var sql = $"SELECT * FROM TB_Cliente WHERE (Status = {status} OR {status} IS NULL)";

                return db.Query<ClienteModel>(sql).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AtualizarCliente(ClienteModel cliente)
        {

            var sql = "UPDATE TB_Cliente SET " +
                      "[CNPJ] = @CNPJ, [RazaoSocial] = @RazaoSocial, [NomeFantasia] = @NomeFantasia, [DataInicio] = @DataInicio, [Status] = @Status " +
                      "WHERE Id = @Id";

            try
            {
                if (db.Execute(sql, cliente) == 0)
                    throw new Exception("Não foi possível atualizar as informações.");
                else
                    return ok;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Close();
            }
        }
        public bool DeletarCliente(int id)
        {
            try
            {
                var sql = "DELETE TB_Cliente " +
                          $"WHERE Id = {id}";

                if (db.Execute(sql) == 0)
                    throw new Exception("Não foi possível excluir as informações.");
                else
                    return ok;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível excluir as informações.");
            }
            finally
            {
                db.Close();
            }
        }
    }
}