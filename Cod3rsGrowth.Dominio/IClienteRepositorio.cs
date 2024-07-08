using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using static Cod3rsGrowth.Dominio.Cliente;

namespace Cod3rsGrowth.Dominio
{
    public interface IClienteRepositorio
    {
        List<Cliente> ObterTodos(FiltroCliente filtro);
        Cliente ObterPorId (int id);
        void Atualizar(int id,Cliente cliente);
        void Deletar(int id);
        int Adicionar(Cliente cliente);
    }
}
