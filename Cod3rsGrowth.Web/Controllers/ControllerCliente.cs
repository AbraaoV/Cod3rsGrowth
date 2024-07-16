using LinqToDB.Data;
using LinqToDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cod3rsGrowth.Dominio;
using System.Data;
using Cod3rsGrowth.Servico.Servicos;
using System.ComponentModel.DataAnnotations;

namespace Cod3rsGrowth.Web.Controllers
{
    [Route(ConstantesDaController.ROTA_CLIENTE)]
    [ApiController]
    public class ControllerCliente : ControllerBase
    {
        private readonly ServicoCliente _servicoCliente;
        public ControllerCliente(ServicoCliente servicoCliente)
        {
            _servicoCliente = servicoCliente;
        }

        [HttpGet]
        public IActionResult ObterTodos([FromQuery] FiltroCliente filtroCliente)
        {
            var todosClientes = _servicoCliente.ObterTodos(filtroCliente);
            return Ok(todosClientes);
        }

        [HttpGet(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult ObterPorId(int id)
        {
            var cliente = _servicoCliente.ObterPorId(id);
            if(cliente == null) {  return NotFound(); }
            return Ok(cliente);
        }
        [HttpPost]
        public IActionResult Adicionar(Cliente cliente)
        {
            _servicoCliente.Adicionar(cliente);
            return Created("Cliente", cliente);
        }
        [HttpPut(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult Atualizar(int id, Cliente cliente)
        {
            _servicoCliente.Atualizar(id, cliente);
            return Ok();
        }
        [HttpDelete(ConstantesDaController.PARAMETRO_ID)]
        public IActionResult Deletar(int id)
        {
            _servicoCliente.Deletar(id);
            return Ok();
        }
    }
}
