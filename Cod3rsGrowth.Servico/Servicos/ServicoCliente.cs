using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using Cod3rsGrowth.Servico.Servicos;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ServicoCliente : IServicoCliente
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        public ServicoCliente(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
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

    }
}
