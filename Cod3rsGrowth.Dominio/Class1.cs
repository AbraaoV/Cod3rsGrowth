using System.Data;

namespace Cod3rsGrowth.Dominio
{
     public class Cliente
    {
        private string nome;
        private int id;
        private string cfp;
        private string cnpj;
        private enum tipo
        {
            Fisico = 1,
            Juridico = 2
        }
    }
     public class Pedido
    {
        private int clienteid;
        private DateTime data;
        private decimal numeroCartao;
        private enum formaPagamento
        {
            Cartao = 1,
            Pix = 2,
            Boleto = 3
        }
    }
}
