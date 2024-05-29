using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using LinqToDB;
using LinqToDB.Data;

namespace Cod3rsGrowth.Infra
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly DataConnection _dataConnection;
        public ClienteRepositorio()
        {
            _dataConnection = new DataConnection(
           new DataOptions()
               .UseSqlServer(@"Server=BOOK-DPKA6KUT4U\SQLEXPRESS.\;Database=Cod3rsGrowth.BancoDeDados;Trusted_Connection=True;"));
        }

        public virtual List<Cliente> ObterTodos()
        {
            return new List<Cliente>();
        }
        public virtual Cliente ObterPorId(int id)
        {
            return new Cliente();
        }
        public virtual void Atualizar(int id, Cliente cliente)
        {

        }
        public virtual void Deletar(int id)
        {

        }
        public virtual void Adicionar(Cliente cliente)
        {

        }
    }
}
