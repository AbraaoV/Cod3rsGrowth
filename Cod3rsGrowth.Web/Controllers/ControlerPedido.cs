﻿using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cod3rsGrowth.Web.Controllers
{
    [Route(ConstantesDaController.ROTA)]
    [ApiController]
    public class ControllerPedido : ControllerBase
    {
        private readonly ServicoPedido _servicoPedido;
        public ControllerPedido(ServicoPedido servicoPedido)
        {
            _servicoPedido = servicoPedido;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var todosPedidos = _servicoPedido.ObterTodos(null);
            if (todosPedidos == null) { return BadRequest(); }
            return Ok(todosPedidos);
        }

        [HttpGet(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult ObterPorId(int id)
        {
            return Ok(_servicoPedido.ObterPorId(id));
        }
        [HttpPost]
        public IActionResult Adicionar(Pedido pedido)
        {
            if (pedido == null) { return BadRequest(); }
            _servicoPedido.Adicionar(pedido);
            return Created("Pedido", pedido);
        }
        [HttpPut(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult Atualizar(int id, Pedido pedido)
        {
            if (pedido == null) { return BadRequest(); }
            _servicoPedido.Atualizar(id, pedido);
            return Ok(pedido);
        }
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            _servicoPedido.Deletar(id);
            return Ok();
        }
    }
}