sap.ui.define([
	"sap/ui/test/opaQunit",
	"./pages/Lista"
], function (opaTest) {
	"use strict";

	QUnit.module("Posts");

	opaTest("Deve ser capaz de carregar mais itens", function (Given, When, Then) {
		// Arrangements
		Given.iStartMyUIComponent({
			componentConfig: {
				name: "ui5.codersgrowth"
			}
		});

        //Actions
        When.naListaCliente.aoApertarEmMais();
		// Assertions
		Then.naListaCliente.listaDeveMostrarTodosOsClientes();
	});
	opaTest("Deve ser capaz de filtrar por nome", function(Given, When, Then) {
		//Actions
		When.naListaCliente.aoPesquisarNome("João");
		//Assertions
		Then.naListaCliente.aListaTemDoisItems();
		// Cleanuo
        Then.iTeardownMyApp();
	});
	opaTest("Lista cliente filtrado por pessoa física", function(Given, When, Then) {
		// Action
		When.naListaCliente.aoFiltrarPorTipoDePessoa("Pessoa Física");

		// Assertion
		Then.naListaCliente.listaDeveEstarFiltradaPorTipoDePessoaFisica();
	});

});