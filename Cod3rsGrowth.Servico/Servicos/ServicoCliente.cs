using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Infra;

namespace Cod3rsGrowth.Dominio.Servicos
{
    public class ServicoCliente : IServicoCliente
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        public ServicoCliente(IClienteRepositorio ClienteRepositorio)
        {
            _clienteRepositorio = ClienteRepositorio;
        }
        public List<Cliente> ObterTodos()
        {
            var clientes = _clienteRepositorio.ObterTodos();
            return clientes;
        }
    }
}
