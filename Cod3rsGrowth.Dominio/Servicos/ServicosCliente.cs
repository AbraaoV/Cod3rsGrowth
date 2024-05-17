using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Dominio.Servicos
{
    public class ServicosCliente : IServicosCliente
    {

        public List<Cliente> ObterTodos()
        {
            return new List<Cliente>()
            {
               
            };
        }
    }
}
