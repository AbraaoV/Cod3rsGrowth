sap.ui.define([
	"sap/ui/test/opaQunit",
    "./Tabela"

], function (opaTest) {
	"use strict";

	QUnit.module("Deletar Pedido");

    opaTest("Deve aparecer uma messageBox de aviso ao tentar deletar um pedido", function (Given, When, Then) {
        // Arrangements
        Given.iStartMyApp({
			hash: "cliente/2/"
		})
		//Actions
        When.naTabelaDePedido.aoClicarNoPedidoDaPosicao(4);
        //Actions
        When.naTabelaDePedido.aoClicarNaOpcao("Deletar");
        // Assertions
        Then.naTabelaDePedido.deveAparecerMessagemBoxDeAvisoComOTexto("Tem certeza que deseja excluir este pedido?");
    });
    opaTest("Deve ser possivel cancelar a remocao do pedido ao clicar em nao opcao 'nao' da messageBox", function (Given, When, Then) {
        //Actions
        When.naTabelaDePedido.aoClicarNaOpacaoDaMessageBox("Não");
        // Assertions
        Then.naTabelaDePedido.tabelaDePedidosDeveEstarCarregada();
    });
    opaTest("Deve ser possivel deletar o pedido ao clicar na opcao 'Sim' da messageBox", function (Given, When, Then) {
        //Actions
        When.naTabelaDePedido.aoClicarNoPedidoDaPosicao(4);
        //Actions
        When.naTabelaDePedido.aoClicarNaOpcao("Deletar");
        //Actions
        When.naTabelaDePedido.aoClicarNaOpacaoDaMessageBox("Sim");
        //Actions
        When.naTabelaDePedido.aoClicarNaOpacaoDaMessageBox("OK")
        // Assertions
        Then.naTabelaDePedido.pedidoDeveEstarRemovidoDaLista(40.15);

        Then.iTeardownMyApp();
    });
})