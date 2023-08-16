using API.Cadastro.Clientes.DKP.Business.Dto;
using API.Cadastro.Clientes.DKP.Business.Interface;
using API.Cadastro.Clientes.DKP.Data.Helpers;
using API.Cadastro.Clientes.DKP.Data.Interface;
using API.Cadastro.Clientes.DKP.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Cadastro.Clientes.DKP.Business.ClienteCases
{
    public class ClienteServico : IClienteServico
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServico(
            IClienteRepositorio clienteRepository
        )
        {
            _clienteRepositorio = clienteRepository;
        }

        public async Task<ClienteDto> CriarCliente(ClienteRequest clienteRequest)
        {
            var novoCliente = new ClienteModel()
            {
                CNPJ = clienteRequest.CNPJ,
                RazaoSocial = clienteRequest.RazaoSocial,
                NomeFantasia = clienteRequest.NomeFantasia,
                Status = clienteRequest.Status
            };

            if (!novoCliente.IsValid())
                throw new Exception(novoCliente.MensagemErro);

            bool cnpjJaExiste = await _clienteRepositorio.ObterCliente(novoCliente.CNPJ) != null;
            if (cnpjJaExiste)
                throw new Exception($"O CNPJ {novoCliente.CNPJ} já existe na base de dados.");

            ClienteDto clienteCriado = await _clienteRepositorio.CriarCliente(novoCliente);

            return clienteCriado;
        }
        public async Task<ClienteDto> ObterCliente(int id)
        {
            return await _clienteRepositorio.ObterCliente(id);
        }
        public async Task<ClienteDto> ObterCliente(string cnpj)
        {
            var cliente = new ClienteModel() { CNPJ = cnpj };

            if (!cliente.IsValid())
                throw new Exception(cliente.MensagemErro);

            return await _clienteRepositorio.ObterCliente(cnpj);
        }

        public List<ClienteDto> ObterClientes(bool? ativo = null)
        {
            List<ClienteDto> listaClientes = new List<ClienteDto>();

            var clientes = _clienteRepositorio.ObterClientes(ativo);
            listaClientes = (from c in clientes
                            select new ClienteDto()
                            {
                                Id = c.Id,
                                CNPJ = c.CNPJ,
                                RazaoSocial = c.RazaoSocial,
                                NomeFantasia = c.NomeFantasia,
                                DataInicio = c.DataInicio,
                                Status = c.Status
                            }).ToList();

            return listaClientes;
        }

        public async Task AtualizarCliente(ClienteRequest clienteRequest)
        {
            var cliente = await _clienteRepositorio.ObterCliente(clienteRequest.CNPJ);

            if (cliente == null || cliente.Id == 0)
                throw new Exception($"O CNPJ {clienteRequest.CNPJ} não foi encontrado.");

            var atualizacaoCliente = new ClienteModel(cliente.Id)
            {
                CNPJ = clienteRequest.CNPJ,
                RazaoSocial = clienteRequest.RazaoSocial,
                NomeFantasia = clienteRequest.NomeFantasia,
                DataInicio = cliente.DataInicio,
                Status = clienteRequest.Status
            };

            if (!atualizacaoCliente.IsValid())
                throw new Exception(atualizacaoCliente.MensagemErro);

            bool clienteAtualizado = _clienteRepositorio.AtualizarCliente(atualizacaoCliente);
            if (!clienteAtualizado)
                throw new Exception("Não foi possível atualizar as informações.");
        }

        public async Task DeletarCliente(int id)
        {
            var cliente = await _clienteRepositorio.ObterCliente(id);

            if (cliente == null)
                throw new Exception($"Não há registros na base de dados com o id {id}.");

            bool clienteDeletado = _clienteRepositorio.DeletarCliente(cliente.Id);
            if (!clienteDeletado)
                throw new Exception("Não foi possível excluir as informações.");
        }
        public async Task DeletarCliente(string cnpj)
        {
            var cliente = new ClienteModel() { CNPJ = cnpj };

            if (!new ClienteModel() { CNPJ = cnpj }.IsValid())
                throw new Exception(cliente.MensagemErro);

            cliente = await _clienteRepositorio.ObterCliente(cnpj);

            if (cliente == null)
                throw new Exception($"O CNPJ {cnpj} não foi encontrado.");

            bool clienteDeletado = _clienteRepositorio.DeletarCliente(cliente.Id);
            if (!clienteDeletado)
                throw new Exception("Não foi possível excluir as informações.");
        }
    }
}