using Cod3rsGrowth.Dominio;
using LinqToDB;
using LinqToDB.Data;
using System.Configuration;
using static Cod3rsGrowth.Dominio.Cliente;

namespace Cod3rsGrowth.Infra
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly DataConnection _dataConnection;

        public ClienteRepositorio()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[ConstantesDosRepositorios.CONNECTION_STRING];
            _dataConnection = new DataConnection(
           new DataOptions()
               .UseSqlServer(result));
        }

        public virtual List<Cliente> ObterTodos(TipoDeCliente? tipo = null) //Objetivo de filtrar por tipo clientes
        {
            var clientes = _dataConnection.GetTable<Cliente>();

            if (tipo.HasValue)
            {
                clientes = (ITable<Cliente>)clientes.Where(c => c.Tipo == tipo.Value);
            }

            return clientes.ToList();
        }
        public virtual Cliente ObterPorId(int id)
        {
            return new Cliente();
        }
        public virtual void Atualizar(int id, Cliente cliente)
        {

        }
        public virtual void Deletar(int id)
        {

        }
        public virtual void Adicionar(Cliente cliente)
        {

        }
    }
}
