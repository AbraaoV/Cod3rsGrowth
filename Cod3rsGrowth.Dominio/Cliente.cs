using System.ComponentModel;
using System.Data;

namespace Cod3rsGrowth.Dominio
{
     public class Cliente
    {
        public string Nome { get; set; }
        public int Id { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public TipoDeCliente Tipo { get; set; }
        public enum TipoDeCliente 
        {
            [Description("Pessoa Física")]
            Fisica = 1,
            [Description("Pessoa Jurídica")]
            Juridica = 2
        }
    }
}
