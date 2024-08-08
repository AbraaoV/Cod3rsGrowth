sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/test/actions/Press",
	"sap/ui/test/matchers/AggregationLengthEquals",
	"sap/ui/test/actions/EnterText",
	"sap/ui/test/matchers/I18NText",
	"sap/ui/test/matchers/PropertyStrictEquals",
	"sap/ui/test/matchers/BindingPath"
], (Opa5, Press, AggregationLengthEquals, EnterText, I18NText, PropertyStrictEquals, BindingPath) => {
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
						actions: function (oMenuItem) {
							oMenuItem.setValue(sNomeCliente);
							oMenuItem.fireLiveChange()
						},
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
							name: "text",
							value: "OK"
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
							name: "text",
							value: "Reinicializar"
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
				},
				
				aoClicarNaLista: function () {
					return this.waitFor({
						id: sIdLista,
						viewName: sViewName,
						actions: new Press(),
						errorMessage: "Lista de clientes não encontrada"
					});
				},

				aoClicarNoClienteDaPosicao: function(iPosicao){
					return this.waitFor({
						controlType: "sap.m.CustomListItem",
						matchers:  new BindingPath({
							path: "/" + iPosicao,
							modelName: "listaDeClientes",
						}),
						actions: new Press(),
						errorMessage: "A lista de clientes não contem um cliente na posição" + iPosicao
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
							length: 20
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
				},
				deveNavegarParaTelaDeLista: function(){
					return this.waitFor({
                        viewName: sViewName,
                        success: function () {
                            Opa5.assert.ok(true, "Sucesso ao navegar para tela de lista");
                        },
                        errorMessage: "Falha ao navegar a pagina de lista"
                    });
				}
			}
		}
	});
});