sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/test/actions/Press",
	"sap/ui/test/matchers/AggregationLengthEquals",
	"sap/ui/test/actions/EnterText",
	"sap/ui/test/matchers/I18NText",
	"sap/ui/test/matchers/PropertyStrictEquals"
], (Opa5, Press, AggregationLengthEquals, EnterText, I18NText, PropertyStrictEquals) => {
	"use strict";

	const sViewName = "lista.Lista",
	sIdLista = "listaClientes"

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
						id: "botaoFiltro",
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
						matchers: new I18NText({
							propertyName: "title",
							key: "filterName"
						}),
						actions: function (oMenuItem) {
							oMenuItem.firePress();
						},
						errorMessage: "Não foi possível pressionar o campo Tipo de Pessoa."
					});
				},	
				aoSelecionarOTipoDePessoaAFiltrar: function(sTipoDePessoa){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.StandardListItem",
						matchers: new PropertyStrictEquals({
							name: "title",
							value: sTipoDePessoa
						}),
						actions: function (oMenuItem) {
							oMenuItem.$().trigger("tap");
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
				},
				aoResetarFiltroTipo: function(){
					return this.waitFor({
						searchOpenDialogs: true,
						controlType: "sap.m.Button",
						matchers: new PropertyStrictEquals({
							name: "id",
							value: "__component0---lista--filtroFragment-detailresetbutton"
						}),
						actions: function (oMenuItem) {
							oMenuItem.firePress();
						},
						errorMessage: "Falha ao apertar o botao de resetar"
					});	
				},
				aoApertarEmAdicionar: function(){
					return this.waitFor({
						id: "botaoAdicionar",
						viewName: sViewName,
						actions: function (oMenuItem) {
							oMenuItem.firePress();
						},
						errorMessage: "Não foi possível pressionar o botão de adicionar."
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
							length: 12
						}),
						success: function () {
							Opa5.assert.ok(true, "A lista tem 13 items");
						},
						errorMessage: "A lista não contem todos os items"
					});
				},
				listaDeveConterDezClientesNaPagina(){
					return this.waitFor({
						id: sIdLista,
						viewName: sViewName,
						matchers: new AggregationLengthEquals({
							name: "items",
							length: 10
						}),
						success: function () {
							Opa5.assert.ok(true, "A lista tem 10 items em sua pagiana");
						},
						errorMessage: "A lista não contem 10 items"
					});
				},
				listaDeveEstarFiltradaPorNome: function (sNomeFiltro) {
					function fnCheckFilter(oList) {
						var fnIsFiltered = function (oElement) {
							if (!oElement.getBindingContext("listaDeClientes")) {
								return false;
							} else {
								var sNome = oElement.getBindingContext("listaDeClientes").getProperty("nome");
								return sNome.includes(sNomeFiltro);
							}
						};
				
						return oList.getItems().every(fnIsFiltered);
					}
				
					return this.waitFor({
						id: sIdLista,
						viewName: sViewName,
						matchers: fnCheckFilter,
						success: function () {
							Opa5.assert.ok(true, "A lista foi filtrada pelo nome: " + sNomeFiltro);
						},
						errorMessage: "Erro ao filtrar"
					});
				},
				listaDeveEstarFiltradaPorTipoDePessoa: function (sTipoPessoa) {
					function fnCheckFilter(oList) {
						var fnIsFiltered = function (oElement) {
							if (!oElement.getBindingContext("listaDeClientes")) {
								return false;
							} else {
								var sTipo = oElement.getBindingContext("listaDeClientes").getProperty("tipo");
								return sTipo === sTipoPessoa;
							}
						};
				
						return oList.getItems().every(fnIsFiltered);
					}
				
					return this.waitFor({
						id: sIdLista,
						viewName: sViewName,
						matchers: fnCheckFilter,
						success: function () {
							Opa5.assert.ok(true, "A lista está filtrada corretamente por Pessoa Física.");
						},
						errorMessage: "A lista não está filtrada corretamente por Pessoa Física."
					});
				}
			}
		}
	});
});