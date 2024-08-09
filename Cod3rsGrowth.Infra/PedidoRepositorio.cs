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
            _dataConnection = new DataConnection(
           new DataOptions()
               .UseSqlServer(ConnectionString.connectionString));
        }

        public virtual List<Pedido> ObterTodos(FiltroPedido? filtro)
        {
            var pedidosTabela = _dataConnection.GetTable<Pedido>();
            var pedidos = pedidosTabela.AsQueryable();

            if (filtro == null)
            {
                return pedidos.ToList();
            }

            if (filtro.FormaPagamento != null)
            {
                pedidos = pedidos.Where(c => c.FormaPagamento == filtro.FormaPagamento);
            }
            if (filtro.ClienteId != null)
            {
                pedidos = pedidos.Where(c => c.ClienteId == filtro.ClienteId);
            }
            if(filtro.DataPedido != default)
            {
                pedidos = pedidos.Where(c => c.Data.Date == filtro.DataPedido.Date);
            }

            if(filtro.ValorMin != ConstantesDosRepositorios.VALOR_INICIAL && filtro.ValorMax != ConstantesDosRepositorios.VALOR_INICIAL && filtro.ValorMin != null && filtro.ValorMax != null)
            {
                pedidos = pedidos.Where(c => c.Valor >= filtro.ValorMin && c.Valor <= filtro.ValorMax);
            }
            if(filtro.ValorMin != ConstantesDosRepositorios.VALOR_INICIAL && filtro.ValorMin != null)
            {
                pedidos = pedidos.Where(c => c.Valor >= filtro.ValorMin);
            }
            if(filtro.ValorMax != ConstantesDosRepositorios.VALOR_INICIAL && filtro.ValorMax != null)
            {
                pedidos = pedidos.Where(c => c.Valor <= filtro.ValorMax);
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
