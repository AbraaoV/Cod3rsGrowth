﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;

namespace Cod3rsGrowth.Testes
{
    public class PedidoRepositorioMock : IPedidoRepositorio
    {
        public List<Pedido> ObterTodos()
        {
            return TabelaPedido.Instance;
        }
        public Pedido ObterPorId(int id)
        {
            return TabelaPedido.Instance.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Atualizar(int id, Pedido pedido)
        {

        }
        public void Deletar (int id)
        {

        }
        public void Adicionar(Pedido pedido)
        {


        }


    }
}
