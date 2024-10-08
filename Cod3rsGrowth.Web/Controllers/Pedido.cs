﻿using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Cod3rsGrowth.Web.Controllers
{
    [Route(ConstantesDaController.ROTA)]
    [ApiController]
    public class Pedido : ControllerBase
    {
        private readonly ServicoPedido _servicoPedido;
        public Pedido(ServicoPedido servicoPedido)
        {
            _servicoPedido = servicoPedido;
        }

        [HttpGet]
        public IActionResult ObterTodos([FromQuery] FiltroPedido filtroPedido)
        {
            var todosPedidos = _servicoPedido.ObterTodos(filtroPedido);
            return Ok(todosPedidos);
        }

        [HttpGet(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult ObterPorId(int id)
        {
            var pedido = _servicoPedido.ObterPorId(id);
            if(pedido == null ) { return NotFound(); }
            return Ok(_servicoPedido.ObterPorId(id));
        }
        [HttpPost]
        public IActionResult Adicionar(Dominio.Pedido pedido)
        {
            _servicoPedido.Adicionar(pedido);
            return Created("Pedido", pedido);
        }
        [HttpPut(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult Atualizar(int id, Dominio.Pedido pedido)
        {
            _servicoPedido.Atualizar(id, pedido);
            return Ok();
        }
        [HttpDelete(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult Deletar(int id)
        {
            _servicoPedido.Deletar(id);
            return Ok();
        }
    }
}
