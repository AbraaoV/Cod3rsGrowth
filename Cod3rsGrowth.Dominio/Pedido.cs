using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public string NumeroCartao { get; set; }
        public decimal Valor {  get; set; }
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
    }
}
