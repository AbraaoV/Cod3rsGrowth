sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/test/actions/Press",
	"sap/ui/test/matchers/Properties",
	"sap/ui/test/matchers/Ancestor",
	"sap/ui/test/actions/EnterText",
	"sap/ui/test/matchers/PropertyStrictEquals"
], (Opa5, Press, Properties, Ancestor, EnterText, PropertyStrictEquals) => {
	"use strict";

	const sViewName = "cliente.AdicionarEditarCliente",
	sIdPagina = "paginaAdicionar"

	Opa5.createPageObjects({
		naTelaDeAdicionarEditar: {
			actions: {
				aoApertarEmSalvar: function(){
					return this.waitFor({
                        id: "botaoSalvar",
                        viewName: sViewName,
                        actions: new Press(),
                        errorMessage: "Falha ao apertar o botão de salvar"
                    });
				},

				aoPreencherNome: function(sNome){
					return this.waitFor({
						id: "inputNome",
						viewName: sViewName,
						actions: new EnterText({
							text: sNome
						}),
						errorMessage: "Input não encontrado."
					});
				},

				aoPreencherCpf: function(sCpf){
					return this.waitFor({
						id: "inputCpf",
						viewName: sViewName,
						actions: new EnterText({
							text: sCpf
						}),
						errorMessage: "Input não encontrado."
					})
				},

				aoPreencherCnpj: function(sCnpj){
					return this.waitFor({
						id: "inputCnpj",
						viewName: sViewName,
						actions: new EnterText({
							text: sCnpj
						}),
						errorMessage: "Input não encontrado."
					})
				},

				aoClicarNaComboxPessoa: function(){
					return this.waitFor({
						id: "comboxTipo",
						viewName: sViewName,
						actions: new Press(),
						errorMessage: "Combobox não encontrada."
					})
				},
				aoSelecionarNaComboBox: function(sPessoa){
					return this.waitFor({
						controlType: "sap.m.StandardListItem",
						viewName: sViewName,
						matchers: new PropertyStrictEquals({
							name: "title",
							value: sPessoa
						}),
						actions: new Press(),
						errorMessage: "Combobox não encontrada."
					})
				},

				aoApertaEmVoltar: function(){
					return this.waitFor({
						id: "botaoVoltar",
						viewName: sViewName,
						actions: new Press(),
						errorMessage: "Falha ao apertar o botão de Voltar"
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

				deveAperecerUmaMessageBoxDe: function(sTitulo){
					return this.waitFor({
						controlType: "sap.m.Dialog",
						matchers: new Properties({ title: sTitulo}),
						success: function () {
							Opa5.assert.ok("A MessageBox apareceu");
						},
						errorMessage: "A MessageBox não apareceu"
					});
				},

				deveFecharMessageBoxAoApertarEm: function(sTextoBotao){
					return this.waitFor({
						controlType: "sap.m.Button",
						matchers: [
							new Properties({ text: sTextoBotao }),
							new Ancestor(Opa5.getContext().dialog, false) 
						],
						actions: new Press(),
						success: function () {
							Opa5.assert.ok(true, "Sucesso ao clicar no botao Ok");
						},
						errorMessage: "Falhar ao clicar no botao Ok"
                    });
				},

				deveEstarNaTelaDeEditar: function(sIdCliente){
					return this.waitFor({
						id: sIdPagina,
						viewName: sViewName,
						success: function () {
							sap.ui.test.Opa5.getWindow().location.hash = "cliente/2/editar";
							Opa5.assert.ok(true, "Sucesso ao navegar para tela editar");
						},
						errorMessage: "Falha ao navegar a pagina de adicionar"
					});
				},
				
			}
		}
	});
});