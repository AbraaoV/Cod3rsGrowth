using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Testes
{
    public sealed class ClienteFalso
    {
        private static readonly ClienteFalso instance = new ClienteFalso ();
        public string Nome { get; set; }
        public int Id { get; set; }
        public string Cfp { get; set; }
        public string Cnpj { get; set; }
        public TipoDeCliente Tipo { get; set; }
        public enum TipoDeCliente
        {
            [Description("Pessoa Física")]
            Fisica = 1,
            [Description("Pessoa Jurídica")]
            Juridica = 2
        }
        private ClienteFalso() { }
        public static ClienteFalso Instance {  get { return instance; } }
    
    }
    class ProgramCliente
    {
        static void Main(string[] args)
        {
            ClienteFalso _cliente1 = ClienteFalso.Instance;
            _cliente1.Nome = "João";
            _cliente1.Id = 1234;
            _cliente1.Cfp = "000.000.000-00";
            _cliente1.Cnpj = "12.345.678/0001-00";
            _cliente1.Tipo = (ClienteFalso.TipoDeCliente)1;
        }
    }
    
}