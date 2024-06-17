﻿using Cod3rsGrowth.Dominio;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.IdentityModel.Tokens;
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

        public virtual List<Cliente> ObterTodos(TipoDeCliente? tipo, string nome)
        {
            var clientesTabela = _dataConnection.GetTable<Cliente>();
            List<Cliente> clientes = clientesTabela.ToList();

            if (tipo != null)
            {
                clientes = clientes.Where(c => c.Tipo == tipo.Value).ToList();
            }
            if(!nome.IsNullOrEmpty())
            {
                clientes = clientes.Where(c => c.Nome.Contains(nome)).ToList();
            }

            return clientes;
        }
        public virtual Cliente ObterPorId(int id)
        {
            var clientes = _dataConnection.GetTable<Cliente>().FirstOrDefault(c => c.Id == id);
            return clientes;
        }
        public virtual void Atualizar(int id, Cliente cliente)
        {
            _dataConnection.GetTable<Cliente>()
              .Where(p => p.Id == id)
              .Set(p => p.Nome, cliente.Nome)
              .Set(p => p.Cpf, cliente.Cpf)
              .Set(p => p.Cnpj, cliente.Cnpj)
              .Set(P => P.Tipo, cliente.Tipo)
              .Update();
        }
        public virtual void Deletar(int id)
        {
            _dataConnection.GetTable<Cliente>()
                .Where(p => p.Id == id)
                .Delete();
        }
        public virtual void Adicionar(Cliente cliente)
        {
            _dataConnection.InsertWithInt32Identity(cliente);
        }

    }
}
