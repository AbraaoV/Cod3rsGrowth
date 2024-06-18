

namespace Cod3rsGrowth.Dominio
{
    public class FiltroPedido
    {
        public Pedido.Pagamentos? FormaPagamento {  get; set; }
        public int? ClienteId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal? ValorMin {  get; set; }
        public decimal? ValorMax { get; set; }
    }
}
