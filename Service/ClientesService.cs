using ApiClientes.Database.Models;
using ApiClientes.Service.DTOs;
using ApiClientes.Service.Exceptions;
using ApiClientes.Service.Parses;
using ApiClientes.Service.Validations;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ApiClientes.Service
{
    public class ClientesService
    {
        private readonly ClientesContext _dbcontext;
        public ClientesService(ClientesContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public ClienteDTO Criar(CriarClienteDTO dto)
        {
            
            ClienteValidation.ValidarCriarCliente(dto);
           
            TbCliente novoCliente = ClienteParser.ToTbCliente(dto);
            
            _dbcontext.TbClientes.Add(novoCliente);
            _dbcontext.SaveChanges();

            return ClienteParser.ToClienteDTO(novoCliente);

        }

        public ClienteDTO GetById(int id)
        {
            var cliente = _dbcontext.TbClientes.Find(id);
            return cliente != null ? ClienteParser.ToClienteDTO(cliente) : null;
        }

        public List<ClienteDTO> GetAll()
        {
            var clientes = _dbcontext.TbClientes.ToList();
            return clientes.Select(ClienteParser.ToClienteDTO).ToList();
        }

        public ClienteDTO Att(int id, CriarClienteDTO dto)
        {
            var cliente = _dbcontext.TbClientes.Find(id);
            if (cliente == null)
                throw new BadRequestException("Cliente não encontrado.");

            ClienteValidation.ValidarCriarCliente(dto);

            _dbcontext.Entry(cliente).CurrentValues.SetValues(dto);
            cliente.Alteradoem = DateTime.UtcNow;
            _dbcontext.SaveChanges();


            return ClienteParser.ToClienteDTO(cliente);
        }

        public void Delete(int id)
        {
            var cliente = _dbcontext.TbClientes.Find(id);
            if (cliente == null)
                throw new BadRequestException("Cliente não encontrado.");

            _dbcontext.TbClientes.Remove(cliente);
            _dbcontext.SaveChanges();
        }
    }
}
