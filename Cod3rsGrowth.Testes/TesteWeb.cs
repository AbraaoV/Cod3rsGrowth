using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Servico.Servicos;
using Cod3rsGrowth.Web;
using Cod3rsGrowth.Web.Controllers;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;
using Cliente = Cod3rsGrowth.Dominio.Cliente;

namespace Cod3rsGrowth.Testes
{
    public class TesteWeb
    {
        private readonly WebApplicationFactory<Program> _factory;

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


            var builder = new WebHostBuilder()
             .UseStartup<Startup>(); // Startup é a classe onde você configura sua aplicação ASP.NET Core

            _server = new TestServer(builder);

            _client = _server.CreateClient();
        }

        [Fact]
        public void Relaziar_testes_opa()
        {

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://localhost:7205");

            Assert.Contains("Cod3rsGrowth", driver.Title);
        }
    }
}
