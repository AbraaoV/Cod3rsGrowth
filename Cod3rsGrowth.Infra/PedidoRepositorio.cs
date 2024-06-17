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

        public virtual List<Pedido> ObterTodos(Pagamentos? FormaPagamento, int? clienteId, DateTime dataPedido, decimal? valorMin, decimal? valorMax)
        {
            var pedidosTabela = _dataConnection.GetTable<Pedido>();
            var pedidos = pedidosTabela.AsQueryable();

            if (FormaPagamento != null)
            {
                pedidos = pedidos.Where(c => c.FormaPagamento == FormaPagamento.Value);
            }
            if (clienteId != null)
            {
                pedidos = pedidos.Where(c => c.ClienteId == clienteId);
            }
            if(dataPedido != default)
            {
                pedidos = pedidos.Where(c => c.Data.Date == dataPedido.Date);
            }

            if(valorMin != 0 && valorMax != 0 && valorMin != null && valorMax != null)
            {
                pedidos = pedidos.Where(c => c.Valor >= valorMin && c.Valor <= valorMax);
            }
            if(valorMin != 0 && valorMin != null)
            {
                pedidos = pedidos.Where(c => c.Valor >= valorMin);
            }
            if(valorMax  != 0 && valorMax != null)
            {
                pedidos = pedidos.Where(c => c.Valor <= valorMax);
            }

            return pedidos.ToList();
        }
        public virtual Pedido ObterPorId(int id)
        {
            var pedidos = _dataConnection.GetTable<Pedido>().FirstOrDefault(c => c.Id == id);
            return pedidos;
        }
        public virtual void Atualizar(int id, Pedido pedido)
        {
            _dataConnection.GetTable<Pedido>()
                .Where(p => p.Id == id)
                .Set(p => p.ClienteId, pedido.ClienteId)
                .Set(p => p.Data, pedido.Data)
                .Set(p => p.NumeroCartao, pedido.NumeroCartao)
                .Set(P => P.Valor, pedido.Valor)
                .Set(p => p.FormaPagamento, pedido.FormaPagamento)
                .Update();
        }
        public virtual void Deletar(int id)
        {
            _dataConnection.GetTable<Pedido>()
                .Where(p => p.Id == id)
                .Delete();
        }
        public virtual void Adicionar(Pedido pedido)
        {
            _dataConnection.Insert(pedido);
        }
    }
}
