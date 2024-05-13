using System.Data;

namespace Cod3rsGrowth.Dominio
{
     internal class Cliente
    {
        string nome;
        int id;
        string cfp;
        string cnpj;
        enum tipo
        {
            Fisico = 1,
            Juridico = 2
        }
    }
     internal class Pedido
    {
        int clienteid;
        DateTime data;
        decimal numeroCartao;
        enum formaPagamento
        {
            Cartao = 1,
            Pix = 2,
            Boleto = 3
        }
    }
}
