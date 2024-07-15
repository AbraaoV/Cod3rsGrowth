sap.ui.define([
	"sap/ui/test/Opa5",
	'sap/ui/test/matchers/AggregationLengthEquals',
	"sap/ui/test/actions/Press",
	"sap/ui/test/actions/EnterText",
	"sap/ui/test/matchers/PropertyStrictEquals",
	"sap/ui/test/matchers/Properties"
], (Opa5, Press, AggregationLengthEquals, EnterText, PropertyStrictEquals, Properties) => {
	"use strict";

	const sViewName = "ui5.codersgrowth.app.lista.Lista",
	sIdLista = "listaCliente"

	Opa5.createPageObjects({
		naListaCliente: {
			actions: {
				aoApertarEmMais() {
					return this.waitFor({
						id: sIdLista,
						viewName: sViewName,
						actions: new Press(),
						errorMessage: "Lista não tem o botão para mostrar mais."
					});
				},
				aoPesquisarNome: function(sNomeCliente){
					return this.waitFor({
						id: "filtroPesquisa",
						viewName: sViewName,
						actions: new EnterText({
							text: sNomeCliente
						}),
						errorMessage: "Barra de pesquisa não encontrada."
					});
				},
				aoApertarBotaoFiltro: function () {
					return this.waitFor({
						id: "botaoFiltros",
						viewName: sViewName,
						actions: function (oMenuItem) {
							oMenuItem.firePress();
						},
						errorMessage: "Não foi possível pressionar o botão de filtro."
					});
				},
				aoSelecionarFiltroTipoDePessoa: function (){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.StandardListItem",
						matchers: new PropertyStrictEquals({
							name: "id",
							value: "__component0---lista--filterItems-list-item"
						}),
						actions: function (oMenuItem) {
							oMenuItem.firePress();
						},
						errorMessage: "Não foi possível pressionar o campo Tipo de Pessoa."
					});
				},	
				aoSelecionarOTipoDePessoaAFiltrar: function(){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.StandardListItem",
						matchers: new PropertyStrictEquals({
							name: "title",
							value: "Pessoa Jurídica"
						}),
						actions: function (oControl) {
							oControl.setSelected() = true;
						},
						errorMessage: "Falha ao escolher o tipo de pesssoa para filtrar."
					});
				},
				aoApertarBotaoOkNoFiltro: function(){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.Button",
						matchers: new PropertyStrictEquals({
							name: "id",
							value: "__component0---lista--filtroFragment-acceptbutton"
						}),
						actions: function (oMenuItem) {
							oMenuItem.firePress();
						},
						errorMessage: "Falha ao apertar o botão ok."
					});
				}		
			},

			assertions: {
				listaDeveMostrarTodosOsClientes() {
					return this.waitFor({
						id: sIdLista,
						viewName: sViewName,
						matchers: new AggregationLengthEquals({
							name: "items",
							length: 13
						}),
						success: function () {
							Opa5.assert.ok(true, "A lista tem 13 items");
						},
						errorMessage: "A lista não contem todos os items"
					});
				},
				aListaTemDoisItems: function(){
					return this.waitFor({
						id: sIdLista,
						viewName: sViewName,
						matchers: new AggregationLengthEquals({
							name: "items",
							length: 2
						}),
						success: function () {
							Opa5.assert.ok(true, "A lista contem quatidade correto de items");
						},
						errorMessage: "A lista não tem dois items."
					});
				},
				listaDeveEstarFiltradaPorTipoDePessoaFisica: function () {
					function fnCheckFilter (oList){
						var fnIsFiltered = function (oElement) {
							if (!oElement.getBindingContext()) {
								return false;
							} else {
								var sDate = oElement.getBindingContext().getProperty("tipo");
								if (!sDate) {
									return false;
								} else {
									return true;
								}
							}
						};

						return oList.getItems().every(fnIsFiltered);
					}

					return this.waitFor({
						id: sIdLista,
						matchers: fnCheckFilter,
						success: function() {
							Opa5.assert.ok(true, "Master list has been filtered correctly");
						},
						errorMessage: "Master list has not been filtered correctly"
					});
				},

			}
		}
	});
});