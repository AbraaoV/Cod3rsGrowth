sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/test/actions/Press",
	"sap/ui/test/matchers/Properties",
	"sap/ui/test/matchers/Ancestor"
], (Opa5, Press, Properties, Ancestor) => {
	"use strict";

	const sViewName = "adicionarCliente.AdicionarCliente",
	sIdPagina = "paginaAdicionar"

	Opa5.createPageObjects({
		naTelaDeAdicionar: {
			actions: {
				aoApertarEmSalvarSemPreencherOsCampos: function(){
					return this.waitFor({
                        id: "botaoSalvar",
                        viewName: sViewName,
                        actions: new Press(),
                        errorMessage: "Falha ao apertar o botão de salvar"
                    });
				}
			},

			assertions: {
                deveNavegarParaTelaDeAdicionar: function(){
                    return this.waitFor({
                        id: sIdPagina,
                        viewName: sViewName,
                        success: function () {
                            Opa5.assert.ok(true, "Sucesso ao navegar para tela de adicioanar");
                        },
                        errorMessage: "Falha ao navegar a pagina de adicionar"
                    });
                },

				deveAperecerUmaMessageBoxDeErro: function(){
					return this.waitFor({
						controlType: "sap.m.Dialog",
						matchers: new Properties({ title: "Alerta" }),
						success: function () {
							Opa5.assert.ok("A MessageBox apareceu");
						},
						errorMessage: "A MessageBox não apareceu"
					});
				},

				deveFecharMessageBoxAoApertarEmOk: function(){
					return this.waitFor({
						controlType: "sap.m.Button",
						matchers: [
							new Properties({ text: "OK" }),
							new Ancestor(Opa5.getContext().dialog, false) 
						],
						actions: new Press(),
						success: function () {
							Opa5.assert.ok(true, "Sucesso ao clicar no botao Ok");
						},
						errorMessage: "Falhar ao clicar no botao Ok"
                    });
				}
				
			}
		}
	});
});