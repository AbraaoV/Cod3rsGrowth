using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cod3rsGrowth.Web.Controllers
{
    [Route(ConstantesDaController.ROTA)]
    [ApiController]
    public class EnumTipo : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var tiposCliente = Enum.GetValues(typeof(Dominio.Cliente.TipoDeCliente))
                                 .Cast<Dominio.Cliente.TipoDeCliente>()
                                 .Select(e => new
                                 {
                                     Key = (int)e,
                                     Descricao = Dominio.Cliente.capturarDescricaoEnum(e)
                                 })
                                 .ToList();

            return Ok(tiposCliente);
        }
    }
}
