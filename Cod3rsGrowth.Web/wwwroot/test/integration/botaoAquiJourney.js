sap.ui.define([
	"sap/ui/test/opaQunit",
	"./pages/App"
], function (opaTest) {
	"use strict";

	QUnit.module("Posts");

	opaTest("Botao mostrar aqui deve mostrar a mensagem 'aqui'", function (Given, When, Then) {
		// Arrangements
		Given.iStartMyUIComponent({
			componentConfig: {
				name: "ui5.codersgrowth"
			}
		});

        //Actions
        When.noBotaoAqui.noMostrarAqui();
		// Assertions
		Then.noBotaoAqui.deveMostrarMsgAqui();
        // Cleanuo
        Then.iTeardownMyApp();
	});
});