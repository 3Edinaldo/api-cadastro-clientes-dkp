using API.Cadastro.Clientes.DKP.Business.Consts;
using API.Cadastro.Clientes.DKP.Business.Dto;
using API.Cadastro.Clientes.DKP.Business.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Cadastro.Clientes.DKP.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServico _clienteServico;
        public ClienteController(
            IClienteServico clienteServico)
        {
            _clienteServico = clienteServico;
        }

        /// <summary>
        /// Retorna todos os clientes. Ativos e inativos.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        /// [
        ///     {
        ///         "id": 1,
        ///         "cnpj": "95650878000189",
        ///         "razaoSocial": "XPTO SA",
        ///         "nomeFantasia": "XPTO",
        ///         "dataInicio": "2023-08-16T15:06:18.92",
        ///         "status": false
        ///     },
        ///     {
        ///         "id": 6,
        ///         "cnpj": "21887004000103",
        ///         "razaoSocial": "JB12 LTDA",
        ///         "nomeFantasia": "JB12",
        ///         "dataInicio": "2023-08-17T18:05:33.197",
        ///         "status": false
        ///     }
        /// ]
        ///     
        /// </remarks>
        /// <returns>Lista de clientes.</returns>
        /// <response code="200">Retorna todos os clientes. Ativos e inativos.</response>
        /// <response code="400">Se ocorrer alguma exceção.</response>
        /// <response code="404">Se não encontrar registros.</response>
        [HttpGet("consulta/obter-todos-os-clientes")]
        public IActionResult ObterTodosOSClientes()
        {
            try
            {
                var clientes = _clienteServico.ObterClientes();

                if (clientes.Count == 0)
                    return NotFound(ActionResultMessageConst.SemDadosParaApresentacao);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os clientes ativos
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        /// [
        ///     {
        ///         "id": 1,
        ///         "cnpj": "95650878000189",
        ///         "razaoSocial": "XPTO SA",
        ///         "nomeFantasia": "XPTO",
        ///         "dataInicio": "2023-08-16T15:06:18.92",
        ///         "status": true
        ///     },
        ///     {
        ///         "id": 6,
        ///         "cnpj": "21887004000103",
        ///         "razaoSocial": "JB12 LTDA",
        ///         "nomeFantasia": "JB12",
        ///         "dataInicio": "2023-08-17T18:05:33.197",
        ///         "status": true
        ///     }
        /// ]
        ///     
        /// </remarks>
        /// <returns>Lista de clientes.</returns>
        /// <response code="200">Retorna todos os clientes ativos.</response>
        /// <response code="400">Se ocorrer alguma exceção.</response>
        /// <response code="404">Se não encontrar registros.</response>
        [HttpGet("consulta/obter-clientes-ativos")]
        public IActionResult ObterClientesAtivos()
        {
            try
            {
                var clientes = _clienteServico.ObterClientes(true);

                if (clientes.Count == 0)
                    return NotFound(ActionResultMessageConst.SemDadosParaApresentacao);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os clientes inativos
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        /// [
        ///     {
        ///         "id": 1,
        ///         "cnpj": "95650878000189",
        ///         "razaoSocial": "XPTO SA",
        ///         "nomeFantasia": "XPTO",
        ///         "dataInicio": "2023-08-16T15:06:18.92",
        ///         "status": false
        ///     },
        ///     {
        ///         "id": 6,
        ///         "cnpj": "21887004000103",
        ///         "razaoSocial": "JB12 LTDA",
        ///         "nomeFantasia": "JB12",
        ///         "dataInicio": "2023-08-17T18:05:33.197",
        ///         "status": false
        ///     }
        /// ]
        ///     
        /// </remarks>
        /// <returns>Lista de clientes.</returns>
        /// <response code="200">Retorna todos os clientes inativos.</response>
        /// <response code="400">Se ocorrer alguma exceção.</response>
        /// <response code="404">Se não encontrar registros.</response>
        [HttpGet("consulta/obter-clientes-inativos")]
        public IActionResult ObterClientesInativos()
        {
            try
            {
                var clientes = _clienteServico.ObterClientes(false);

                if (clientes.Count == 0)
                    return NotFound(ActionResultMessageConst.SemDadosParaApresentacao);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retorna um clinte através do id
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET /Todo
        ///     {
        ///         "id": 1,
        ///         "cnpj": "95650878000189",
        ///         "razaoSocial": "XPTO SA",
        ///         "nomeFantasia": "XPTO",
        ///         "dataInicio": "2023-08-16T15:06:18.92",
        ///         "status": false
        ///     }
        ///
        /// </remarks>
        /// <returns>Um cliente.</returns>
        /// <response code="200">Retorna um cliente ativo.</response>
        /// <response code="400">Se ocorrer alguma exceção.</response>
        /// <response code="404">Se não encontrar nenhum registro.</response>
        [HttpGet("consulta/obter-cliente-id")]
        public async Task<IActionResult> ObterClienteId(int id)
        {
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

        /// <summary>
        /// Retorna um clinte através do CNPJ
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     GET /Todo
        ///     {
        ///         "id": 1,
        ///         "cnpj": "95650878000189",
        ///         "razaoSocial": "XPTO SA",
        ///         "nomeFantasia": "XPTO",
        ///         "dataInicio": "2023-08-16T15:06:18.92",
        ///         "status": false
        ///     }
        ///
        /// </remarks>
        /// <returns>Um cliente.</returns>
        /// <response code="200">Retorna um cliente ativo.</response>
        /// <response code="400">Se ocorrer alguma exceção.</response>
        /// <response code="404">Se não encontrar nenhum registro.</response>
        [HttpGet("consulta/obter-cliente-cnpj/{cnpj}")]
        public async Task<IActionResult> ObterClienteCnpj(string cnpj)
        {
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

        /// <summary>
        /// Cria um cliente
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /Todo
        ///     {
        ///         "cnpj": "95650878000189",
        ///         "razaoSocial": "XPTO SA",
        ///         "nomeFantasia": "XPTO",
        ///         "status": false
        ///     }
        ///
        /// </remarks>
        /// <returns>O cliente criado.</returns>
        /// <response code="201">Retorna o cliente criado.</response>
        /// <response code="400">Se ocorrer alguma exceção ou o ModelState for inválido.</response>
        [HttpPost("escrita/criar-cliente")]
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

        /// <summary>
        /// Atualiza um cliente
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /Todo
        ///     {
        ///         "cnpj": "95650878000189",
        ///         "razaoSocial": "XPTO SA",
        ///         "nomeFantasia": "XPTO",
        ///         "status": false
        ///     }
        ///
        /// </remarks>
        /// <returns>O cliente atualizado.</returns>
        /// <response code="200">Retorna o cliente atualizado.</response>
        /// <response code="400">Se ocorrer alguma exceção.</response>
        /// <response code="404">Se não encontrar nehum registro.</response>
        [HttpPut("atualizacao/atualizar-cliente")]
        public async Task<IActionResult> AtualizarCliente([FromBody] ClienteRequest clienteRequest)
        {
            try
            {
                var cliente = await _clienteServico.ObterCliente(clienteRequest.CNPJ);
                if (cliente.Id != 0)
                {
                    await _clienteServico.AtualizarCliente(clienteRequest);
                    return Ok(clienteRequest);
                }
                else
                    return NotFound(ActionResultMessageConst.CnpjNaoEncontrado.SetMessageValue(clienteRequest.CNPJ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um cliente através do Id
        /// </summary>
        /// <returns>Mensagem informando a exclusão.</returns>
        /// <response code="200">Mensagem informando a exclusão do Id informado.</response>
        /// <response code="400">Se ocorrer alguma exceção.</response>
        /// <response code="404">Se não encontrar nehum registro através do id informado.</response>
        [HttpDelete("exclusao/deletar-cliente-id")]
        public async Task<IActionResult> DeletarCliente(int id)
        {
            try
            {
                var cliente = await _clienteServico.ObterCliente(id);
                if (cliente.Id != 0)
                {
                    await _clienteServico.DeletarCliente(id);
                    return Ok(ActionResultMessageConst.IdExcluido.SetMessageValue(id));
                }
                else
                    return NotFound(ActionResultMessageConst.IdNaoEncontrado.SetMessageValue(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um cliente através do CNPJ
        /// </summary>
        /// <returns>Mensagem informando a exclusão.</returns>
        /// <response code="200">Mensagem informando a exclusão do cnpj informado.</response>
        /// <response code="400">Se ocorrer alguma exceção.</response>
        /// <response code="404">Se não encontrar nehum registro através do cnpj informado.</response>
        [HttpDelete("exclusao/deletar-cliente-cnpj/{cnpj}")]
        public async Task<IActionResult> DeletarCliente(string cnpj)
        {
            try
            {
                var cliente = await _clienteServico.ObterCliente(cnpj);
                if (cliente.Id != 0)
                {
                    await _clienteServico.DeletarCliente(cnpj);
                    return Ok(ActionResultMessageConst.CnpjExcluido.SetMessageValue(cnpj));
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