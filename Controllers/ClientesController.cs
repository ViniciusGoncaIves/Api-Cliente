using ApiClientes.Database.Models;
using ApiClientes.Service;
using ApiClientes.Service.DTOs;
using ApiClientes.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace ApiClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClientesService _service;
        public ClientesController(ClientesService service) 
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<ClienteDTO> Adicionar([FromBody] CriarClienteDTO body)
        {
            try
            {
                var Response = _service.Criar(body);
                return Ok(body);
            }
            catch (BadRequestException B)
            {
                return BadRequest(B.Message);
            }
            catch (System.Exception E)
            {
                return StatusCode(500, E.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<ClienteDTO> ObterPorId(int id)
        {
            try
            {
                var cliente = _service.GetById(id);
                if (cliente == null)
                    return NotFound("Cliente não encontrado.");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> ObterTodos()
        {
            try
            {
                var clientes = _service.GetAll();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ClienteDTO> Atualizar(int id, [FromBody] CriarClienteDTO body)
        {
            try
            {
                var cliente = _service.Att(id, body);
                return Ok(cliente);
            }
            catch (BadRequestException b)
            {
                return BadRequest(b.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Deletar(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (BadRequestException b)
            {
                return BadRequest(b.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
