using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Testes
{
    public class MeuServico : ICliente
    {
        private readonly string _adicionarNome;
        private readonly int _adicionarId;
        private readonly string _adicionarCpf;
        private readonly string _adicionarCnpj;
        private readonly Tipo _definirTipo;

        public MeuServico()
        {
            _adicionarNome = "João";
            _adicionarId = 1234;
            _adicionarCpf = "000.000.000-00";
            _adicionarCnpj = "000.000.000/0000-00";
            _definirTipo = 1;
        }
        public string AdicionarNome()
        {
            return _adicionarNome;
        }
        public int AdicionarId()
        {
            return _adicionarId;
        }
        public string AdicionarCpf()
        {
            return _adicionarCpf;
        }
        public string AdicionarCnpj()
        {
            return _adicionarCnpj;
        }
        public enum DefinirTipo()
        {
            return _definarTipo;
        }

    }


}
