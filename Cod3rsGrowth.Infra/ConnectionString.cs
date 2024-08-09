using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Infra
{
    public sealed class ConnectionString
    {
        public static string connectionString { get; set; } = ConstantesDosRepositorios.CONNECTION_STRING;
    }
}
