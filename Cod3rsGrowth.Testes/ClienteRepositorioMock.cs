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
        
        }
        public void Deletar(int id)
        {

        }
        public int Adicionar(Cliente cliente)
        {
            int adicionar = new int();
            return adicionar;
        }
    }

}
