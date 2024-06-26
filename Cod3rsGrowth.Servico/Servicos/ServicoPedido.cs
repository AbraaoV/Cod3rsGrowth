﻿using Cod3rsGrowth.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Dominio;
using FluentValidation.Results;
using FluentValidation;
using static Cod3rsGrowth.Dominio.Pedido;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ServicoPedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly IValidator<Pedido> _validarPedido;
        public ServicoPedido(IPedidoRepositorio pedidoRepositorio, IValidator<Pedido> validator)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _validarPedido = validator;
        }
        public List<Pedido> ObterTodos(FiltroPedido filtro)
        {
            var pedidos = _pedidoRepositorio.ObterTodos(filtro);
            return pedidos;
        }
        public Pedido ObterPorId(int id)
        {
            var pedidos = _pedidoRepositorio.ObterPorId(id);
            return pedidos;
        }
        public void Adicionar(Pedido pedido)
        {
            _validarPedido.ValidateAndThrow(pedido);
            _pedidoRepositorio.Adicionar(pedido);
        }
        public void Atualizar(int id, Pedido pedido)
        {
            pedido.Id = id;
            ValidationResult result = _validarPedido.Validate(pedido, options => options.IncludeRuleSets(ConstantesDoValidador.ATUALIZAR, "default"));
            if (result.IsValid)
            {
                _pedidoRepositorio.Atualizar(id, pedido);
            }
            else if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
        public void Deletar(int id)
        {
            Pedido pedido1 = new Pedido();
            pedido1.Id = id;

            ValidationResult result = _validarPedido.Validate(pedido1, options => options.IncludeRuleSets(ConstantesDoValidador.REMOVER));
            if (result.IsValid)
            {
                _pedidoRepositorio.Deletar(id);
            }
            else if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }
    }
}
