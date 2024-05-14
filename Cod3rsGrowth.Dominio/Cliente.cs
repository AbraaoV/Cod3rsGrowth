using System.Data;

namespace Cod3rsGrowth.Dominio
{
     public class Cliente
    {
        public string Nome { get; set; }
        public int Id { get; set; }
        public string Cfp { get; set; }
        public string Cnpj { get; set; }
        public enum Tipo 
        {
            Fisico = 1,
            Juridico = 2
        }
    }
}
