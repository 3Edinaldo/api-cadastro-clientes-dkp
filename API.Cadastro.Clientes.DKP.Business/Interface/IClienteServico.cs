using API.Cadastro.Clientes.DKP.Business.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Cadastro.Clientes.DKP.Business.Interface
{
    public interface IClienteServico
    {
        Task<ClienteDto> CriarCliente(ClienteRequest clienteRequest);
        Task<ClienteDto> ObterCliente(int id);
        Task<ClienteDto> ObterCliente(string cnpj);
        List<ClienteDto> ObterClientes(bool? ativo = null);
        Task AtualizarCliente(ClienteRequest clienteRequest);
        Task DeletarCliente(int id);
        Task DeletarCliente(string cnpj);
    }
}