using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Infra
{
    public static class ConstantesDosRepositorios
    {
        public const string CONNECTION_STRING = "Data Source=BOOK-DPKA6KUT4U\\SQLEXPRESS;Initial Catalog=Cod3rsGrowth.BancoDeDados;Persist Security Info=True;User ID=sa;Password=sap@123;Trust Server Certificate=True";
        public const string CONNECTION_STRING_TESTE = "Data Source=BOOK-DPKA6KUT4U\\SQLEXPRESS;Initial Catalog=Cod3rsGrowthDbTeste;Persist Security Info=True;User ID=sa;Password=sap@123;Trust Server Certificate=True";
        public const int VALOR_INICIAL = 0;
    }
}
