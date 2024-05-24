﻿using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using FluentValidation;
using FluentValidation.Results;

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
        public void Atualizar(int id, Cliente cliente)
        {
            ValidationResult result = _validarCliente.Validate(cliente, options => options.IncludeRuleSets(ConstantesDoValidador.ATUALIZAR, "default"));
            if (result.IsValid)
            {
                _clienteRepositorio.Atualizar(id, cliente);
            }
            else if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}