using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;

namespace Cod3rsGrowth.Infra
{
    public interface IClienteRepositorio
    {
        List<Cliente> ObterTodos();
        Cliente ObterPorId (int id);
        void Atualizar(int id,Cliente cliente);
        void Deletar(int id);
        int Adicionar(Cliente cliente);
    }
}
