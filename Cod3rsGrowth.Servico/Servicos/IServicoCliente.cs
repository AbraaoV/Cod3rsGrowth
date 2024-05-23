using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
namespace Cod3rsGrowth.Servico.Servicos
{
    public interface IServicoCliente
    {
        List<Cliente> ObterTodos();
        Cliente ObterPorId(int id);
        void Adicionar(Cliente cliente);
        void Atualizar(int id, Cliente cliente);
    }
}
