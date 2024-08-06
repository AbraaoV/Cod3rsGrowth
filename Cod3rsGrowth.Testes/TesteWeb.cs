using Cod3rsGrowth.Dominio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Cod3rsGrowth.Testes
{
    public class TesteWeb : TesteBase
    {
        private readonly TestServer _server;

        public TesteWeb()
        {
            var cliente1 = new Cliente
            {
                Nome = "Teste",
                Id = 100,
                Cpf = "12345678910",
                Cnpj = "",
                Tipo = Cliente.TipoDeCliente.Fisica
            };

            var cliente2 = new Cliente
            {
                Nome = "Empresa Teste",
                Id = 200,
                Cpf = "",
                Cnpj = "12345678000190",
                Tipo = Cliente.TipoDeCliente.Juridica
            };
            TabelaCliente.Instance.Add(cliente1);
            TabelaCliente.Instance.Add(cliente2);
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<TesteWebStartup>());
        }

        [Fact]
        public void Relaziar_testes_opa()
        {
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:7205/test/integration/opaTests.qunit.html?#/cliente/38/");
        }
    }
}
