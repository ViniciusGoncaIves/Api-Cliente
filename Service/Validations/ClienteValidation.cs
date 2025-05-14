using ApiClientes.Service.DTOs;
using ApiClientes.Service.Exceptions;
using System;
using System.Linq;

namespace ApiClientes.Service.Validations
{
    public class ClienteValidation
    {
        public static void ValidarCriarCliente(CriarClienteDTO criarClienteDTO)
        {
            if (string.IsNullOrEmpty(criarClienteDTO.Nome))
            {
                throw new BadRequestException("Nome é obrigatório");
            }
            if (string.IsNullOrEmpty(criarClienteDTO.Documento))
            {
                throw new BadRequestException("Telefone é obrigatório");
            }

            if (!new[] {0 , 1, 2, 3, 99}.Contains(criarClienteDTO.Tipodoc))
            {
                throw new BadRequestException("Tipo de documento não suportado");
            }
        }
    }
}
