sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/test/actions/Press"
], (Opa5, Press) => {
	"use strict";

	const sViewName = "ui5.codersgrowth.view.App";

	Opa5.createPageObjects({
		noBotaoAqui: {
			actions: {
				noMostrarAqui() {
					return this.waitFor({
						id: "bntMostrarAqui",
						viewName: sViewName,
						actions: new Press(),
						errorMessage: "Não foi encontrado o botao 'Mostrar Aqui'"
					});
				}
			},

			assertions: {
				deveMostrarMsgAqui() {
					return this.waitFor({
						controlType: "sap.m.MessageToast",
						success() {
							Opa5.assert.ok(true, "Aqui");
						},
						errorMessage: "Dialogo não encontrado"
					});
				}
			}
		}
	});
});