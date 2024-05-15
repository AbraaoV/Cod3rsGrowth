using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Testes
{
    public sealed class PedidoFalso
    {
        private static readonly PedidoFalso instance = new PedidoFalso();
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public string NumeroCartao { get; set; }
        public decimal Valor { get; set; }
        public Pagamentos FormaPagamento { get; set; }
        public enum Pagamentos
        {
            [Description("Pagamento efetuado no cartão")]
            Cartao = 1,
            [Description("Pagamento efetuado no pix")]
            Pix = 2,
            [Description("Pagamento efetuado no boleto")]
            Boleto = 3
        }
        private PedidoFalso() { }
        public static PedidoFalso Instance { get { return instance; } }
    }
    class ProgramPedido
    {
        static void Main(string[] args)
        {
            PedidoFalso _cliente1 = PedidoFalso.Instance;
            _cliente1.Id = 987;
            _cliente1.ClienteId = 1234;
            _cliente1.Data = DateTime.Now;
            _cliente1.NumeroCartao = "0000.1111.2222.3333";
            _cliente1.Valor = 150.49m;
            _cliente1.FormaPagamento = (PedidoFalso.Pagamentos)1;
        }
    }
}
