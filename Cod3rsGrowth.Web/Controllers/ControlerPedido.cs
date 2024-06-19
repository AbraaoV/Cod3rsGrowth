using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cod3rsGrowth.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlerPedido : ControllerBase
    {
        private readonly ServicoPedido _servicoPedido;

        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(_servicoPedido.ObterTodos(null));
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            return Ok(_servicoPedido.ObterPorId(id));
        }
        [HttpPost("{id}")]
        public IActionResult Adicionar(Pedido pedido)
        {
            if (pedido == null) { return BadRequest(); }
            _servicoPedido.Adicionar(pedido);
            return Created("Pedido", pedido);
        }
        [HttpPut]
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
