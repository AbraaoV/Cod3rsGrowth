using static System.Net.WebRequestMethods;

namespace Cod3rsGrowth.Web
{
    public static class ConstantesDaController
    {
        public const string ROTA = "api/[controller]";
        public const string PARAMETRO_ID = "{id}";
        public const string TITULO = "Ocorreram um ou mais erros de validação.";
        public const string DETALHE = "Consulte a propriedade erros para obter detalhes adicionais.";
        public const string TIPO = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
        public const string NOME_EXTENCAO = "errors";
        public const string COMECO_MENSAGEM_ERRO_SQL = "A instrução DELETE conflitou com a restrição do REFERENCE";
        public const string MENSAGEM_ERRO_AO_DELETAR_CLIENTE_COM_PEDIDO = "Para remover um cliente, primeiro remova todos seus pedidos";

    }
}
