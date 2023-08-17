using API.Cadastro.Clientes.DKP.Business.Consts;
using API.Cadastro.Clientes.DKP.Business.Dto;
using API.Cadastro.Clientes.DKP.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Cadastro.Clientes.DKP.Controllers
{
    [ApiController]
    [Route("api/cadastro/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServico _clienteServico;
        public ClienteController(
            IClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        [HttpPost("criar-cliente")]
        public IActionResult CriarCliente([FromBody] ClienteRequest cliente)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var clienteCriado = _clienteServico.CriarCliente(cliente);

                return Created($"criar-cliente", clienteCriado.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obter-cliente-id")]
        public async Task<IActionResult> ObterClienteId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                ClienteDto clienteCriado = await _clienteServico.ObterCliente(id);

                if (clienteCriado.Id == 0)
                    return NotFound(ActionResultMessageConst.IdNaoEncontrado.SetMessageValue(id));

                return Ok(clienteCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obter-cliente-cnpj/{cnpj}")]
        public async Task<IActionResult> ObterClienteCnpj(string cnpj)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                ClienteDto clienteCriado = await _clienteServico.ObterCliente(cnpj);

                if (clienteCriado.Id == 0)
                    return NotFound(ActionResultMessageConst.CnpjNaoEncontrado.SetMessageValue(cnpj));

                return Ok(clienteCriado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obter-clientes-ativos")]
        public IActionResult ObterClientesAtivos()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                List<ClienteDto> clientes = _clienteServico.ObterClientes(true);

                if (clientes.Count == 0)
                    return NotFound(ActionResultMessageConst.SemDadosParaApresentacao);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obter-clientes-inativos")]
        public IActionResult ObterClientesInativos()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                List<ClienteDto> clientes = _clienteServico.ObterClientes(false);

                if (clientes.Count == 0)
                    return NotFound(ActionResultMessageConst.SemDadosParaApresentacao);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obter-todos-os-clientes")]
        public IActionResult ObterTodosOSClientes()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                List<ClienteDto> clientes = _clienteServico.ObterClientes();

                if (clientes.Count == 0)
                    return NotFound(ActionResultMessageConst.SemDadosParaApresentacao);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("atualizar-cliente")]
        public async Task<IActionResult> AtualizarCliente([FromBody] ClienteRequest clienteRequest)
        {
            try
            {
                var cliente = await _clienteServico.ObterCliente(clienteRequest.CNPJ);
                if (cliente.Id != 0)
                {
                    await _clienteServico.AtualizarCliente(clienteRequest);
                    return Created($"/obter-cliente-cnpj/{clienteRequest.CNPJ}", clienteRequest);
                }
                else
                    return NotFound(ActionResultMessageConst.CnpjNaoEncontrado.SetMessageValue(clienteRequest.CNPJ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletar-cliente-id")]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            try
            {
                var cliente = await _clienteServico.ObterCliente(id);
                if (cliente.Id != 0)
                {
                    await _clienteServico.DeletarCliente(id);
                    return NoContent();
                }
                else
                    return NotFound(ActionResultMessageConst.IdNaoEncontrado.SetMessageValue(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletar-cliente-cnpj/{cnpj}")]
        public async Task<IActionResult> DeletarCliente(string cnpj)
        {
            try
            {
                var cliente = await _clienteServico.ObterCliente(cnpj);
                if (cliente.Id != 0)
                {
                    await _clienteServico.DeletarCliente(cnpj);
                    return NoContent();
                }
                else
                    return NotFound(ActionResultMessageConst.CnpjNaoEncontrado.SetMessageValue(cnpj));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}