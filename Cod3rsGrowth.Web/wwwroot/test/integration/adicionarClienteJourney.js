sap.ui.define([
	"sap/ui/test/opaQunit",
	"./pages/AdicionarCliente",
	"./pages/Lista"
], function (opaTest) {
	"use strict";

	QUnit.module("Tela de cadastro");

	opaTest("Deve ser capaz de carregar mais itens", function (Given, When, Then) {
		// Arrangements
		Given.iStartMyApp();
        //Actions
        When.naListaCliente.aoApertarEmAdicionar();
		// Assertions
		Then.naTelaDeAdicionar.deveNavegarParaTelaDeAdicionar();
	});
	opaTest("Ao tentar adicionar um cliente com campos vazios deve retornar erros de validacao", function(Given, When, Then) {
		//Actions
        When.naTelaDeAdicionar.aoApertarEmSalvarSemPreencherOsCampos();
		// Assertions
		Then.naTelaDeAdicionar.deveAperecerUmaMessageBoxDeErro();

		Then.naTelaDeAdicionar.deveFecharMessageBoxAoApertarEmOk();
		// Cleanuo
		Then.iTeardownMyApp();
	});

});