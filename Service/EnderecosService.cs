using ApiClientes.Service.Exceptions;
using System.Collections.Generic;
using System.Linq;
using ApiClientes.Database.Models;
using ApiClientes.Service.DTOs;
using ApiClientes.Service.Parses;
using ApiClientes.Service.Validations;
using System;

namespace ApiClientes.Service
{
    public class EnderecoService
    {
        private readonly ClientesContext _dbcontext;

        public EnderecoService(ClientesContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public List<EnderecoDTO> GetAllAddress()
        {
            var enderecos = _dbcontext.TbEnderecos.ToList();
            return enderecos.Select(EnderecoParser.ToEnderecoDTO).ToList();
        }


        public EnderecoDTO GetEnderecoByClienteId(int clienteId)
        {
            var endereco = _dbcontext.TbEnderecos.FirstOrDefault(e => e.Clienteid == clienteId);
            return endereco != null ? EnderecoParser.ToEnderecoDTO(endereco) : null;
        }


        public EnderecoDTO CadastrarEndereco(int idCliente, CriarEnderecoDTO dto)
        {
            EnderecoValidation.ValidarCriarEndereco(dto);

            TbEndereco novoEndereco = EnderecoParser.ToTbEndereco(dto);

            novoEndereco.Clienteid = idCliente;

            _dbcontext.TbEnderecos.Add(novoEndereco);
            _dbcontext.SaveChanges();

            return EnderecoParser.ToEnderecoDTO(novoEndereco);

        }

        public void DeleteEndereco(int idCliente)
        {
            var enderecos = _dbcontext.TbEnderecos
                .Where(e => e.Clienteid == idCliente)
                .ToList();

            if (!enderecos.Any())
            {
               throw new BadRequestException("Nenhum endereço encontrado para o cliente informado!");
            }

            _dbcontext.TbEnderecos.RemoveRange(enderecos);
            _dbcontext.SaveChanges();
        }

        public EnderecoDTO AttEndereco(int idCliente, CriarEnderecoDTO dto)
        {
            EnderecoValidation.ValidarCriarEndereco(dto);

            var enderecoAtualizado = _dbcontext.TbEnderecos.FirstOrDefault(e => e.Clienteid == idCliente);

            if (enderecoAtualizado == null)
                throw new BadRequestException("Endereço não encontrado para o cliente informado.");
            enderecoAtualizado.Logradouro = dto.logradouro;
            enderecoAtualizado.Numero = dto.numero;
            enderecoAtualizado.Bairro = dto.bairro;
            enderecoAtualizado.Cidade = dto.cidade;
            enderecoAtualizado.Uf = dto.uf;
            enderecoAtualizado.Cep = dto.cep;
            enderecoAtualizado.Status = dto.status;
            
            _dbcontext.SaveChanges();

            return EnderecoParser.ToEnderecoDTO(enderecoAtualizado);
        }
    }
}