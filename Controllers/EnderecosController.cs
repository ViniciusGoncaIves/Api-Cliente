using ApiClientes.Service.Exceptions;
using ApiClientes.Service;
using ApiClientes.Service.DTOs;
using ApiClientes.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecosController : ControllerBase
    {
        private readonly EnderecoService _service;

        public EnderecosController(EnderecoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnderecoDTO>> ObterTodos()
        {
            try
            {
                var enderecos = _service.GetAllAddress();
                return Ok(enderecos);
            }
            catch (BadRequestException e)
            {
               return BadRequest(e.Message);
            }
        }


        [HttpGet("cliente/{clienteId}/enderecos")]
        public ActionResult<List<EnderecoDTO>> GetEnderecosCliente(int clienteId)
        {
            var enderecos = _service.GetEnderecoByClienteId(clienteId);
            if (enderecos == null)
                return NotFound("Nenhum endereço encontrado para o cliente.");

            return Ok(enderecos);
        }

        [HttpPost]
        public ActionResult<EnderecoDTO> PostEndereco(int id, [FromBody] CriarEnderecoDTO body)
        {
            try
            {
                var Response = _service.CadastrarEndereco(id, body);
                return Ok(Response);
            }
            catch (BadRequestException B)
            {
                return BadRequest(B.Message);
            }
        }


        [HttpDelete("{idCliente}")]
        public ActionResult DeleteEnderecosPorCliente(int idCliente)
        {
            try
            {
                _service.DeleteEndereco(idCliente);
                return NoContent(); 
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{idCliente}")]
        public ActionResult<EnderecoDTO> Atualizar(int idCliente, [FromBody] CriarEnderecoDTO body)
        {
            try
            {
                var response = _service.AttEndereco(idCliente, body);
                return Ok(response);
            }
            catch (BadRequestException b)
            {
                return BadRequest(b.Message);
            }
        }
    }
}