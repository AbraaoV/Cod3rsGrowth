using Cod3rsGrowth.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cod3rsGrowth.Web.Controllers
{
    [Route(ConstantesDaController.ROTA)]
    [ApiController]
    public class EnumFormaPagamento : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var tiposCliente = Enum.GetValues(typeof(Dominio.Pedido.Pagamentos))
                                 .Cast<Dominio.Pedido.Pagamentos> ()
                                 .Select(e => new
                                 {
                                     Key = (int)e,
                                     Descricao = ObterDescriacao.capturarDescricaoEnum(e)
                                 })
                                 .ToList();

            return Ok(tiposCliente);
        }
    }
}
