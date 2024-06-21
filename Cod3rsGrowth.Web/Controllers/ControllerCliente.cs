using LinqToDB.Data;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cod3rsGrowth.Dominio;
using System.Data;
using Cod3rsGrowth.Servico.Servicos;

namespace Cod3rsGrowth.Web.Controllers
{
    [Route(ConstantesDaController.ROTA)]
    [ApiController]
    public class ControllerCliente : ControllerBase
    {
        private readonly ServicoCliente _servicoCliente;
        public ControllerCliente(ServicoCliente servicoCliente)
        {
            _servicoCliente = servicoCliente;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var todosClientes = _servicoCliente.ObterTodos(null);
            if (todosClientes == null || todosClientes.Count == 0) { return  BadRequest(); }
            return Ok(todosClientes);
        }

        [HttpGet(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult ObterPorId(int id)
        {
            var cliente = _servicoCliente.ObterPorId(id);
            if(cliente == null) {  return NotFound(id); }
            return Ok(cliente);
        }
        [HttpPost]
        public IActionResult Adicionar(Cliente cliente)
        {
            if (cliente == null) { return BadRequest(); }
            _servicoCliente.Adicionar(cliente);
            return Created("Cliente", cliente);
        }
        [HttpPut(ConstantesDaController.PARAMETRO_ID)]
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
