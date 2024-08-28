sap.ui.define([
    "sap/ui/test/Opa5",
    "sap/ui/test/actions/Press",
    "sap/ui/test/matchers/AggregationLengthEquals",
    "sap/ui/test/actions/EnterText",
    "sap/ui/test/matchers/PropertyStrictEquals",
    "sap/ui/test/matchers/BindingPath",
    "sap/ui/test/matchers/Properties",
	"sap/ui/test/matchers/Ancestor"
], (Opa5, Press, AggregationLengthEquals, EnterText, PropertyStrictEquals, BindingPath, Properties, Ancestor) => {
    "use strict";

    const sViewName = "cliente.pedido.Tabela",
    sIdTabela = "tabelaPedidos"

    Opa5.createPageObjects({
        naTabelaDePedido: {
            actions: {
                aoClicarNaComboBoxFormaDePagamento: function(){
                    return this.waitFor({
                        id: "comboBoxFormaDePagamento",
                        viewName: sViewName,
                        actions: new Press(),
                        errorMessage: "Combobox não encontrada."
                    })
                },

                aoSelecionarNaComboBoxOValor: function(sFormaDePagamento){
                    return this.waitFor({
                        controlType: "sap.m.StandardListItem",
                        viewName: sViewName,
                        matchers: new PropertyStrictEquals({
                            name: "title",
                            value: sFormaDePagamento
                        }),
                        actions: new Press(),
                        errorMessage: "Combobox não encontrada."
                    })
                },

                aoFiltrarPelaData: function(sData){
                    return this.waitFor({
                        id: "filtroDataPicker",
                        viewName: sViewName,
                        actions: new EnterText({
                            text: sData,
                            pressEnterKey: true
                        }),
                        errorMessage: "Input de data não encontrado"
                    })
                },

                aoFiltrarPeloValorMin: function(sValorMin){
                    return this.waitFor({
                        id: "inputValorMin",
                        viewName: sViewName,
                        actions: function (oMenuItem) {
                            oMenuItem.setValue(sValorMin);
                            oMenuItem.fireLiveChange()
                        },
                        errorMessage: "Input de data não encontrado"
                    })
                },

                aoFiltrarPeloValorMax: function(sValorMax){
                    return this.waitFor({
                        id: "inputValorMax",
                        viewName: sViewName,
                        actions: function (oMenuItem) {
                            oMenuItem.setValue(sValorMax);
                            oMenuItem.fireLiveChange()
                        },
                        errorMessage: "Input de data não encontrado"
                    })
                },

                aoClicarEmAdicionar: function(){
                    return this.waitFor({
						id: "botaoAdicionar",
						viewName: sViewName,
						actions: function (oMenuItem) {
                            oMenuItem.firePress()
                        },
						errorMessage: "Não foi possível pressionar o botão de adicionar."
					});
                },
                
                aoClicarNaOpcao: function(sTextoBotao){
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

                aoClicarNoPedidoDaPosicao: function(iPosicao){
                    return this.waitFor({
                        controlType: "sap.m.ColumnListItem",
                        matchers:  new BindingPath({
                            path: "/" + iPosicao,
                            modelName: "listaDePedidos",
                        }),
                        actions: new Press(),
                        errorMessage: "A lista de clientes não contem um cliente na posição" + iPosicao
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
                tabelaDePedidosDeveEstarCarregada: function(){
                    return this.waitFor({
                        id: sIdTabela ,
                        viewName: sViewName,
                        controlType: "sap.m.Table",
                        errorMessage: "Tabela não encontrada"
                    });
                },

                tabelaDePedidosDeveConterDezPedidos() {
					return this.waitFor({
						id: sIdTabela ,
						viewName: sViewName,
						matchers: new AggregationLengthEquals({
							name: "items",
							length: 10
						}),
						success: function () {
							Opa5.assert.ok(true, "A tabela tem 10 pedidos");
						},
						errorMessage: "A tabela não contem 10 pedidos"
					});
				},

                tabelaDeveEstarFiltradaPor: function(sPropriedade, sFiltro, sValorMax) {
                    function fnCheckFilter(oList) {
                        
                        var fnIsFiltered = function(oElement) {
                            if (!oElement.getBindingContext("listaDePedidos")) {
                                return false;
                            } else {
                                var sValor = oElement.getBindingContext("listaDePedidos").getProperty(sPropriedade);
                                if (sPropriedade === "valor") {
                                    if (sFiltro !== undefined && sValorMax !== undefined) {
                                        return sValor >= sFiltro && sValor <= sValorMax;
                                    } else if (sFiltro !== undefined) {
                                        return sValor >= sFiltro;
                                    } else if (sValorMax !== undefined) {
                                        return sValor <= sValorMax;
                                    }
                                } else {
                                    return sValor === sFiltro;
                                }
                            }
                            return false;
                        };
                
                        return oList.getItems().every(fnIsFiltered);
                    }
                
                    return this.waitFor({
                        id: sIdTabela,
                        viewName: sViewName,
                        matchers: fnCheckFilter,
                        success: function() {
                            Opa5.assert.ok(true, "A lista está filtrada corretamente com os parâmetros fornecidos.");
                        },
                        errorMessage: "A lista não está filtrada corretamente com os parâmetros fornecidos."
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

                pedidoDeveEstarRemovidoDaLista: function(sValorDoPedido){
					function fnCheckFilter(oList) {
						var fnIsFiltered = function (oElement) {
							if (!oElement.getBindingContext("listaDePedidos")) {
								return false;
							} else {
								var sValor = oElement.getBindingContext("listaDePedidos").getProperty("valor");
								return sValor === sValorDoPedido;
							}
						};
				
						return oList.getItems().every(fnIsFiltered);
					}

					return this.waitFor({
						id: sIdTabela,
						viewName: sViewName,
						matchers: !fnCheckFilter,
						success: function () {
							Opa5.assert.ok(true, "Pedido removido com sucesso");
						},
						errorMessage: "Falha ao remover pedido"
					});
				},
            }
        }
    });
});