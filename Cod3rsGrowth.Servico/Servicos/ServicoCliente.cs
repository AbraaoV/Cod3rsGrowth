using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.OutputCaching;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ServicoCliente : IServicoCliente
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IValidator<Cliente> _validarCliente;

        public ServicoCliente(IClienteRepositorio clienteRepositorio, IValidator<Cliente> validator)
        {
            _clienteRepositorio = clienteRepositorio;
            _validarCliente = validator;
        }
        public List<Cliente> ObterTodos()
        {
            var clientes = _clienteRepositorio.ObterTodos();
            return clientes;
        }
        public Cliente ObterPorId(int id)
        {
            var clientes = _clienteRepositorio.ObterPorId(id);
            return clientes;
        }
        public void Adicionar(Cliente cliente)
        {
            _validarCliente.ValidateAndThrow(cliente);
            _clienteRepositorio.Adicionar(cliente);
        }
    }
}