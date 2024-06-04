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
using static Cod3rsGrowth.Dominio.Pedido;

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

        public virtual List<Pedido> ObterTodos(Pagamentos? FormaPagamento = null)
        {
            var pedidos = _dataConnection.GetTable<Pedido>();

            if (FormaPagamento.HasValue)
            {
                pedidos = (ITable<Pedido>)pedidos.Where(c => c.FormaPagamento == FormaPagamento.Value);
            }

            return pedidos.ToList();
        }
        public virtual Pedido ObterPorId(int id)
        {
            return new Pedido();
        }
        public virtual void Atualizar(int id, Pedido pedido)
        {
            Pedido procurarPedido = _dataConnection.GetTable<Pedido>().FirstOrDefault(c => c.Id == id);
            procurarPedido.Id = pedido.Id;
            procurarPedido.ClienteId = pedido.ClienteId;
            procurarPedido.Data = pedido.Data;
            procurarPedido.NumeroCartao = pedido.NumeroCartao;
            procurarPedido.Valor = pedido.Valor;
            procurarPedido.FormaPagamento = pedido.FormaPagamento;
        }
        public virtual void Deletar(int id)
        {

        }
        public virtual void Adicionar(Pedido pedido)
        {
            _dataConnection.Insert(pedido);
        }
    }
}
