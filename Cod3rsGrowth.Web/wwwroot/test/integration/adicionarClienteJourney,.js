sap.ui.define([
	"sap/ui/test/opaQunit",
	"./pages/AdicionarCliente",
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
        When.naListaCliente.aoApertarEmAdicionar();
		// Assertions
		Then.naTelaDeAdicionar.deveNavegarParaTelaDeAdicionar();

        Then.iTeardownMyApp();
	});
});