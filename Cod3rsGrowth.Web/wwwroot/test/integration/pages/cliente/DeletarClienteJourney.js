sap.ui.define([
	"sap/ui/test/opaQunit",
    "./DetalhesCliente"

], function (opaTest) {
	"use strict";

	QUnit.module("Deletar Cliente");

    opaTest("Deve aparecer uma messageBox de aviso ao tentar deletar um cliente", function (Given, When, Then) {
        Given.iStartMyApp({
			hash: "cliente/1"
		})
        //Actions
        When.naTelaDeDetalhes.aoClicarNoBotaoDe("botaoDeletar");
        // Assertions
        Then.naTelaDeDetalhes.deveAparecerMessagemBoxDeAvisoComOTexto("Tem certeza que deseja excluir este cliente?");
    });
    opaTest("Deve ser possivel cancelar a remocao do cliente ao clicar em nao opcao 'nao' da messageBox", function (Given, When, Then) {
        //Actions
        When.naTelaDeDetalhes.aoClicarNaOpacaoDaMessageBox("Não");
        // Assertions
        Then.naTelaDeDetalhes.deveEstarNaTelaDeDetalhes();
    });
    opaTest("Deve ser possivel deletar o cliente ao clicar na opcao 'Sim' da messageBox", function (Given, When, Then) {
        //Actions
        When.naTelaDeDetalhes.aoClicarNoBotaoDe("botaoDeletar");
        When.naTelaDeDetalhes.aoClicarNaOpacaoDaMessageBox("Sim");
        When.naTelaDeDetalhes.aoClicarNaOpacaoDaMessageBox("Voltar à Página Inicial")
        // Assertions
        Then.naListaCliente.clienteDeveEstarRemovidoDaLista("João Silva");

        Then.iTeardownMyApp();
    });
})