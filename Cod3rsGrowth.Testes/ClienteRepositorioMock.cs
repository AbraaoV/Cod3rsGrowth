﻿using Cod3rsGrowth.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Testes
{
    public class ClienteRepositorioMock : IClienteRepositorio
    {
        public List<Cliente> ObterTodos()
        {
            List<Cliente>clientes = new List<Cliente>();
            return clientes;
        }
        public Cliente ObterPorId(int id)
        {
            Cliente cliente = new Cliente();
            return cliente;
        }
        public void Atualizar(int id, Cliente cliente)
        {
        
        }
        public void Deletar(int id)
        {

        }
        public int Adicionar(Cliente cliente)
        {
            int adicionar = new int();
            return adicionar;
        }
    }

}
