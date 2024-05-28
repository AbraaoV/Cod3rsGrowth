using Cod3rsGrowth.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Infra;
namespace Cod3rsGrowth.Testes
{
    public class ClienteRepositorioMock : IClienteRepositorio
    {
        public List<Cliente> ObterTodos()
        {
            return TabelaCliente.Instance;
        }
        public Cliente ObterPorId(int id)
        {
           return TabelaCliente.Instance.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Atualizar(int id, Cliente cliente)
        {
            Cliente procurarCliente = TabelaCliente.Instance.FirstOrDefault(c => c.Id == id);
            procurarCliente.Nome = cliente.Nome;
            procurarCliente.Id = cliente.Id;
            procurarCliente.Cpf = cliente.Cpf;
            procurarCliente.Cnpj = cliente.Cnpj;
            procurarCliente.Tipo = cliente.Tipo;
        }
        public void Deletar(int id)
        {
            Cliente clienteParaRemover = TabelaCliente.Instance.Where(i => i.Id == id).FirstOrDefault();
            TabelaCliente.Instance.Remove(clienteParaRemover);
        }
        public void Adicionar(Cliente cliente)
        {
            TabelaCliente.Instance.Add(cliente);
        }
    }

}
