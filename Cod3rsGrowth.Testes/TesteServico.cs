using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Dominio.Servicos;
using Microsoft.Extensions.DependencyInjection;
namespace Cod3rsGrowth.Testes
{
    public class TesteServicoCliente : TesteBase
    {
        private readonly IServicosCliente servicosCliente;

        public TesteServicoCliente()
        {
          servicosCliente = ServiceProvider.GetService<IServicosCliente>();
        }

        [Fact]
        public void TesteObterTodos_Clientes()
        {
            var obterTodos = servicosCliente.ObterTodos();

            
        }
    }
}