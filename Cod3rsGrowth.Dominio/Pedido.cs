using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationAttribute = LinqToDB.Mapping.AssociationAttribute;

namespace Cod3rsGrowth.Dominio
{
    [Table]
    public class Pedido
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }
        [Column]
        public int ClienteId { get; set; }
        [Column]
        public DateTime Data { get; set; }
        [Column(Length = 16)]
        public string NumeroCartao { get; set; }
        [Column]
        public decimal Valor {  get; set; }
        [Column]
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
