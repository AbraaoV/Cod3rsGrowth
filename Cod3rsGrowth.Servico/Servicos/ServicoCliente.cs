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
        private readonly ValidacaoCliente _validarCliente;

        public ServicoCliente(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _validarCliente = new ValidacaoCliente();
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
            ValidationResult result = _validarCliente.Validate(cliente);
            if (result.IsValid)
            {
                _clienteRepositorio.Adicionar(cliente);
            }
            else if (!result.IsValid)
            {
                string mensagemErro = string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException("Falha na validação dos dados:" + Environment.NewLine + mensagemErro);
            }
        }

    }
}