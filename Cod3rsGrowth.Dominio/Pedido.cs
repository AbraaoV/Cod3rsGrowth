using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Dominio
{
    public class Pedido
    {
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public decimal NumeroCartao { get; set; }
        public enum FormaPagamento
        {
            Cartao = 1,
            Pix = 2,
            Boleto = 3
        }
    }
}
