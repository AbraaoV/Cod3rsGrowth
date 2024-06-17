using Cod3rsGrowth.Dominio;
using FluentValidation;
using FluentValidation.Results;
using static Cod3rsGrowth.Dominio.Cliente;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ServicoCliente
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IValidator<Cliente> _validarCliente;

        public ServicoCliente(IClienteRepositorio clienteRepositorio, IValidator<Cliente> validator)
        {
            _clienteRepositorio = clienteRepositorio;   
            _validarCliente = validator;
        }
        public List<Cliente> ObterTodos(TipoDeCliente? tipo, string nome)
        {
            var clientes = _clienteRepositorio.ObterTodos(tipo, nome);
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
            cliente.Id = id;
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
        public void Deletar(int id)
        {   
            Cliente cliente1 = new Cliente();
            cliente1.Id = id;
            ValidationResult result = _validarCliente.Validate(cliente1, options => options.IncludeRuleSets(ConstantesDoValidador.REMOVER));
            if (result.IsValid)
            {
                _clienteRepositorio.Deletar(id);
            }
            else if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}