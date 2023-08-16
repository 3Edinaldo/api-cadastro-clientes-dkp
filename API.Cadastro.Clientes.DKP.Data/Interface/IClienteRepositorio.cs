using API.Cadastro.Clientes.DKP.Data.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Cadastro.Clientes.DKP.Data.Interface
{
    public interface IClienteRepositorio
    {
        Task<ClienteModel> CriarCliente(ClienteModel cliente);
        Task<ClienteModel> ObterCliente(int id);
        Task<ClienteModel> ObterCliente(string cnpj);
        List<ClienteModel> ObterClientes(bool? ativo);
        bool AtualizarCliente(ClienteModel cliente);
        bool DeletarCliente(int id);
    }
}