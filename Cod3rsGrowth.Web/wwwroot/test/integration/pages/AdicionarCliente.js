sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/test/actions/Press"

], (Opa5, Press) => {
	"use strict";

	const sViewName = "ui5.codersgrowth.adicionarCliente.AdicionarCliente",
	sIdPagina = "paginaAdicionar"

	Opa5.createPageObjects({
		naTelaDeAdicionar: {
			actions: {
				
			},

			assertions: {
                deveNavegarParaTelaDeAdicionar: function(){
                    return this.waitFor({
                        id: sIdPagina,
                        viewName: sViewName,
                        success: function () {
                            Opa5.assert.ok(true, "The table is visible");
                        },
                        errorMessage: "Was not able to see the table."
                    });
                }
			}
		}
	});
});