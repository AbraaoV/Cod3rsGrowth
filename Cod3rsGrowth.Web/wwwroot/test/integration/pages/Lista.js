sap.ui.define([
	"sap/ui/test/Opa5",
	'sap/ui/test/matchers/AggregationLengthEquals',
	"sap/ui/test/actions/Press",
	'sap/ui/test/actions/EnterText'
], (Opa5, Press, AggregationLengthEquals, EnterText) => {
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
				aoFiltrarPorTipoDePessoa: function (sOption) {
					return this.waitFor({
						id: "botaoFiltro",
						viewName: sViewName,
						actions: new Press(),
						success: function () {
							this.waitFor({
								searchOpenDialogs: true,
								controlType: "sap.m.StandardListItem",
								matchers: function(oControl){
									return this.I18NTextExtended(oControl, "filterName", "title");
								}.bind(this),
								actions: new Press(),
								success: function () {
									this.waitFor({
										searchOpenDialogs: true,
										controlType: "sap.m.StandardListItem",
										matchers: function(oControl){
											return this.I18NTextExtended(oControl, sOption, "title");
										}.bind(this),
										actions: new Press(),
										success: function () {
											this.waitFor({
												searchOpenDialogs: true,
												controlType: "sap.m.Button",
												matchers: function(oControl){
													return this.I18NTextExtended(oControl, "VIEWSETTINGS_ACCEPT", "text", "sap.m");
												}.bind(this),
												actions: new Press(),
												errorMessage: "Erro ao apertar o botão OK"
											});
										},
										errorMessage: "Não foi encontrado " +  sOption + " no filtro tipo de pessoa"
									});
								},
								errorMessage: "Não foi entrado o filtro tipo de pessoa"
							});
						},
						errorMessage: "Botão de filtro não encontrado"
					});
				},
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