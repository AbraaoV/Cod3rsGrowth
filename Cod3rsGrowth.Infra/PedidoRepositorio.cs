using Cod3rsGrowth.Dominio;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Infra
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly DataConnection _dataConnection;
        private SqlServerVersion connectionString;

        public PedidoRepositorio()
        {
            _dataConnection = new DataConnection(
           new DataOptions()
               .UseSqlServer(connectionString));
        }

        public virtual List<Pedido> ObterTodos()
        {
            return new List<Pedido>();
        }
        public virtual Pedido ObterPorId(int id)
        {
            return new Pedido();
        }
        public virtual void Atualizar(int id, Pedido cliente)
        {

        }
        public virtual void Deletar(int id)
        {

        }
        public virtual void Adicionar(Pedido cliente)
        {

        }
    }
}
