using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Forms
{
    public static class Constantes
    {
        public const int INDICE_PRIMEIRA_LINHA = 0;
        public const int INDICE_COLUNA_CPF = 2;
        public const int INDICE_COLUNA_CNPJ = 3;
        public const int INDICE_COLUNA_CARTAO = 3;
        public const int ENUM_INDEFINIDO = 0;
        public const int INDICE_CARTAO = 0;
        public const int INDICE_PIX = 1;
        public const int INDICE_BOLETO = 2;
        public const int INDICE_PESSOA_FISICA = 1;
        public const int INDICE_PESSOA_JURIDICA = 2;
        public const int INDICE_TODOS_TIPOS = 0;
        public const int INDICE_TODOS_PAGAMENTOS = 3;
        public const string COLUNA_VALOR_TABELA_PEDIDO = "valorDataGridViewTextBoxColumn";
        public const string DUAS_CASAS_APOS_VIRGULA = "N2";
        public const string COLUNA_ID = "idDataGridViewTextBoxColumn";
        public const string OPCAO_DO_TOOL_STRIP_MENU = "Pedidos";
        public const string MENSAGEM_CONFIRMACAO_REMOCAO_CLIENTE = "Tem certeza de que deseja remover este cliente?";
        public const string MENSAGEM_CONFIRMACAO = "Confirmação de Remoção";
        public const string COMECO_MENSAGEM_ERRO_SQL = "A instrução DELETE conflitou com a restrição do REFERENCE";
        public const string MENSAGEM_ERRO_AO_DELETAR_CLIENTE_COM_PEDIDO = "Para remover um cliente, primeiro remova todos seus pedidos";
        public const string MENSAGEM_ERRO_AO_REMOVER_NENHUM_CLIENTE = "Por favor, selecione um cliente para remover.";
        public const string AVISO = "Aviso";
        public const string MENSAGEM_CONFIRMACAO_REMOCAO_PEDIDO = "Tem certeza de que deseja remover este pedido?";
        public const string MENSAGEM_ERRO_AO_REMOVER_NENHUM_PEDIDO = "Por favor, selecione um pedido para remover.";
    }
}
