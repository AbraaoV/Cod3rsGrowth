using LinqToDB.Data;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cod3rsGrowth.Dominio;
using System.Data;
using Cod3rsGrowth.Servico.Servicos;

namespace Cod3rsGrowth.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerCliente : ControllerBase
    {
        private readonly ServicoCliente _servicoCliente;

        [HttpGet]
        public IActionResult ObterTodos()
        {
            return Ok(_servicoCliente.ObterTodos(null));
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            return Ok(_servicoCliente.ObterPorId(id));
        }
        [HttpPost("{id}")]
        public IActionResult Adicionar(Cliente cliente)
        {
            if (cliente == null) { return BadRequest(); }
            _servicoCliente.Adicionar(cliente);
            return Created("Cliente", cliente);
        }
        [HttpPut]
        public IActionResult Atualizar(int id, Cliente cliente)
        {
            if (cliente == null) { return BadRequest(); }
            _servicoCliente.Atualizar(id, cliente);
            return Ok(cliente);
        }
        [HttpDelete]
        public IActionResult Deletar(int id)
        {
            _servicoCliente.Deletar(id);
            return Ok();
        }
    }
}
