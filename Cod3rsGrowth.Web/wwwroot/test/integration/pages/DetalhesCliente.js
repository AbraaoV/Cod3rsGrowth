sap.ui.define([
	"sap/ui/test/Opa5",
    "sap/ui/test/actions/Press",

], (Opa5, Press) => {
	"use strict";

	const sViewName = "detalhesCliente.DetalhesCliente"

	Opa5.createPageObjects({
		naTelaDeDetalhes: {
			actions: {
				aoClicarNoBotaoDe: function(sBotaoId){
                    return this.waitFor({
						id: sBotaoId,
						viewName: sViewName,
						actions: new Press(),
						errorMessage: "Não foi possível encontrar o botão de id: " + sBotaoId
					});
                },
                
			},
			
			assertions: {
				deveEstarNaTelaDeDetalhes: function(){
                    return this.waitFor({
                        viewName: sViewName,
                        success: function () {
                            Opa5.assert.ok(true, "Sucesso ao navegar para tela de detalhes");
                        },
                        errorMessage: "Falha ao navegar a pagina de detalhes"
                    });
                }
			}
		}
	});
});