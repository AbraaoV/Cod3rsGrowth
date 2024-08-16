sap.ui.define([
	"sap/ui/test/Opa5",
    "sap/ui/test/actions/Press",
	"sap/ui/test/matchers/Properties",
	"sap/ui/test/matchers/Ancestor"
], (Opa5, Press, Properties, Ancestor) => {
	"use strict";

	const sViewName = "cliente.DetalhesCliente"

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

				aoClicarNaOpacaoDaMessageBox: function(sTextoBotao){
					return this.waitFor({
						controlType: "sap.m.Button",
						matchers: [
							new Properties({ text: sTextoBotao }),
							new Ancestor(Opa5.getContext().dialog, false) 
						],
						actions: new Press(),
						success: function () {
							Opa5.assert.ok(true, "Sucesso ao clicar no botao");
						},
						errorMessage: "Falhar ao clicar no botao"
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
                },

				deveAparecerMessagemBoxDeAvisoComOTexto: function(sTexto){
					return this.waitFor({
						controlType: "sap.m.Dialog",
						controlType: "sap.m.Text",
						matchers: new Properties({ text: sTexto}),
						success: function () {
							Opa5.assert.ok("A MessageBox apareceu");
						},
						errorMessage: "A MessageBox não apareceu"
					});
				},
			}
		}
	});
});