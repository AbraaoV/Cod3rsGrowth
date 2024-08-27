sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/test/actions/Press",
	"sap/ui/test/matchers/Properties",
	"sap/ui/test/matchers/Ancestor",
	"sap/ui/test/actions/EnterText",
	"sap/ui/test/matchers/PropertyStrictEquals",
	"sap/ui/test/matchers/I18NText"
], (Opa5, Press, Properties, Ancestor, EnterText, PropertyStrictEquals, I18NText) => {
	"use strict";

	const idDilogo = "adicionarPedidoDialgo"
    const controleDialog = "sap.m.Dialog"

	Opa5.createPageObjects({
		naTelaDeAdicionarEditarPedido: {
			actions: {
				aoClicarEmSalvar: function(){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.Button",
						matchers: new I18NText({
							propertyName: "text",
							key: "addBtnText"
						}),
                        actions: new Press(),
                        errorMessage: "Falha ao apertar o botão de salvar"
                    });
				},

				aoClicarEmCancelar: function(){
					return this.waitFor({
						searchOpenDialogs: true,
                        controlType: "sap.m.Button",
						matchers: new I18NText({
							propertyName: "text",
							key: "cancelBtnText"
						}),
                        actions: new Press(),
                        errorMessage: "Falha ao apertar o botão de salvar"
                    });
				},

				aoPreencherInputDeData: function(sValor){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.DatePicker",
						actions: new EnterText({
							text: sValor
						}),
						errorMessage: "Input não encontrado."
					});
				},

				aoPreencherInputDeNumeroCartao: function(sValor){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.MaskInput",
						actions: new EnterText({
							text: sValor
						}),
						errorMessage: "Input não encontrado."
					});
				},

				aoPreencherInputDeValor: function(sValor){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.Input",
						actions: new EnterText({
							text: sValor
						}),
						errorMessage: "Input não encontrado."
					});
				},

				aoClicarNaComboBoxFormaDePagamento: function(){
                    return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.Select",
                        actions: new Press(),
                        errorMessage: "Combobox não encontrada."
                    })
                },

                aoSelecionarNaComboBoxOValor: function(sFormaDePagamento){
                    return this.waitFor({
						searchOpenDialogs: true,
                        controlType: "sap.ui.core.Item",
                        matchers: new PropertyStrictEquals({
                            name: "text",
                            value: sFormaDePagamento
                        }),
                        actions: new Press(),
                        errorMessage: "Combobox não encontrada."
                    })
                },

			},

			assertions: {
                deveAbrirFormularioDeAdicionarEditarPedido: function(){
                    return this.waitFor({
						searchOpenDialogs: true,
                        controlType: controleDialog,
                        success: function () {
                            Opa5.assert.ok(true, "Sucesso ao abrir dialgo");
                        },
                        errorMessage: "Falha ao abrir dialgo"
                    });
                },

				deveAperecerUmaMessageBoxDe: function(sTitulo){
					return this.waitFor({
						controlType: controleDialog,
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

				dialogDeAdicionarDeveEstarFechado: function(){
					return this.waitFor({
                        controlType: !controleDialog,
                        id:!idDilogo,
                        success: function () {
                            Opa5.assert.ok(true, "Dialogo fechado");
                        },
                        errorMessage: "O dialogo não está fechado"
                    });
				}
				
			}
		}
	});
});