using Cod3rsGrowth.Dominio;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Infra
{
    public class PedidoRepositorio : IPedidoRepositorio
    {
        private readonly DataConnection _dataConnection;

        public PedidoRepositorio()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[ConstantesDosRepositorios.CONNECTION_STRING];
            _dataConnection = new DataConnection(
           new DataOptions()
               .UseSqlServer(result));
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
