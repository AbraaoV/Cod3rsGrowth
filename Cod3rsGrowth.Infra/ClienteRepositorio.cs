﻿using Cod3rsGrowth.Dominio;
using LinqToDB;
using LinqToDB.Data;
using System.Configuration;

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

        public virtual List<Cliente> ObterTodos()
        {
            return new List<Cliente>();
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
