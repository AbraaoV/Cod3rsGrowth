sap.ui.define([
    "sap/ui/test/opaQunit",
    "./Tabela"
], function (opaTest) {
    "use strict";

    QUnit.module("Tabela de Pedidos");

    opaTest("Deve existir tabela de pedidos na tela de detalhes do cliente", function (Given, When, Then) {
        // Given
        Given.iStartMyApp({
            hash: "cliente/2"
        });
        // Assertions
        Then.naTabelaDePedido.tabelaDePedidosDeveEstarCarregada();
        Then.naTabelaDePedido.tabelaDePedidosDeveConterDezPedidos();
    });

    opaTest("Deve ser capaz de filtrar por forma de pagamento", function (Given, When, Then) {
        // Actions
        When.naTabelaDePedido.aoClicarNaComboBoxFormaDePagamento();
        When.naTabelaDePedido.aoSelecionarNaComboBoxOValor("Pagamento efetuado no cartão");
        // Assertions
        Then.naTabelaDePedido.tabelaDeveEstarFiltradaPor("formaPagamento", "Pagamento efetuado no cartão");
    });

    opaTest("Deve ser capaz de limpar o filtro de forma de pagamento", function (Given, When, Then) {
        // Actions
        When.naTabelaDePedido.aoClicarNaComboBoxFormaDePagamento();
        When.naTabelaDePedido.aoSelecionarNaComboBoxOValor("Todos pagamentos");
        // Assertions
        Then.naTabelaDePedido.tabelaDePedidosDeveConterDezPedidos();
    });

    opaTest("Deve ser capaz de filtrar pela data do pedido", function (Given, When, Then) {
        // Action
        When.naTabelaDePedido.aoFiltrarPelaData("12/06/2024");
        // Assertions
        Then.naTabelaDePedido.tabelaDeveEstarFiltradaPor("data", "2024-06-12T12:34:09.923");
    });

    opaTest("Deve ser capaz de resetar o filtro de data", function (Given, When, Then) {
        // Action
        When.naTabelaDePedido.aoFiltrarPelaData("");
        // Assertion
        Then.naTabelaDePedido.tabelaDePedidosDeveConterDezPedidos();
    });

    opaTest("Deve ser capaz de filtrar por valor min", function (Given, When, Then) {
        // Action
        When.naTabelaDePedido.aoFiltrarPeloValorMin("30.50");
        // Assertions
        Then.naTabelaDePedido.tabelaDeveEstarFiltradaPor("valor", "30.50");
    });

    opaTest("Deve ser capaz de filtrar por valor max", function (Given, When, Then) {
        // Actions
        When.naTabelaDePedido.aoFiltrarPeloValorMax("300.50");
        // Assertions
        Then.naTabelaDePedido.tabelaDeveEstarFiltradaPor("valor", undefined, "300.50");
    });

    opaTest("Deve ser capaz de filtrar por valor min e Max", function (Given, When, Then) {
        // Actions
        When.naTabelaDePedido.aoFiltrarPeloValorMin("30.50");
        When.naTabelaDePedido.aoFiltrarPeloValorMax("300.50");
        // Assertions
        Then.naTabelaDePedido.tabelaDeveEstarFiltradaPor("valor", "30.50", "300.50");
        // Cleanup
        Then.iTeardownMyApp();
    });
});
