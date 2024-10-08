sap.ui.define([
	"../model/formatter",
	"ui5/codersgrowth/common/ControllerBase",
	"ui5/codersgrowth/common/ConstantesDoBanco",
	"ui5/codersgrowth/common/ConstantesLayoutDoApp",
	"ui5/codersgrowth/common/ConstantesDaRota",
	"ui5/codersgrowth/common/HttpRequest",
    "ui5/codersgrowth/common/ConstatesDasRequests",
	'sap/ui/model/json/JSONModel',
	"sap/m/MessageBox",
], function (formatter, ControllerBase, ConstantesDoBanco, ConstantesLayoutDoApp, ConstantesDaRota, HttpRequest, ConstatesDasRequests, JSONModel, MessageBox){
	"use strict";

	const ID_BOTAO_TELA_CHEIA = "botaoTelaCheia"
	const ID_BOTAO_SAIR_TELA_CHEIA = "botaoSairTelaCheia"
	const NOME_DO_MODELO_DO_APP = "appView"
	const ID_DO_CLIENTE_NA_ROTA = 1
	const PROPRIEDADE_ID_DO_CLIENTE = "/id"
	const MSG_DE_SUCESSO_AO_DELETAR_I18N = "sucessOnCustumerDelete"
	const MSG_DE_AVISO_AO_DELETAR_I18N = "warningMessageWhenDeleteCustomer"
	const OPCAO_SIM = "Sim"
	const OPCAO_NAO = "Não"
	
	return ControllerBase.extend("ui5.codersgrowth.app.cliente.DetalhesCliente", {
		formatter: formatter,
		onInit: function () {
			this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_DETALHE).attachPatternMatched(this._aoCoincidirRota, this);
		},

		_modeloCliente: function(modelo){
            const nomeDoModelo = "clienteSelecionado"
            return this._modelo(nomeDoModelo, modelo)
        },

		_aoCoincidirRota: async function () {
			this._exibirEspera(async () => {
				this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_DUAS_COLUNAS_DIVIDAS)
				let retorno = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[ID_DO_CLIENTE_NA_ROTA])
				this._modeloCliente(new JSONModel(retorno));
			})
		},

        aoFecharDetalhes: function () {
			this._exibirEspera(() => {
				this._modelo(NOME_DO_MODELO_DO_APP).setProperty(ConstantesLayoutDoApp.LAYOUT_SAIR_TELA_CHEIA, false);
				this.obterRota().navTo(ConstantesDaRota.NOME_DA_ROTA_DA_LISTA_CLIENTE);
			});
		},

        aoClicarEmTelaCheia: function () {
			this._exibirEspera(() => {
				this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_MEIO_TELA_CHEIA)
				this.getView().byId(ID_BOTAO_SAIR_TELA_CHEIA).setVisible(true);
				this.getView().byId(ID_BOTAO_TELA_CHEIA ).setVisible(false);
			});	
		},
		aoClicarEmFecharTelaCheia: function () {
			this._exibirEspera(() => {
				this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_DUAS_COLUNAS_DIVIDAS)
				this.getView().byId(ID_BOTAO_SAIR_TELA_CHEIA).setVisible(false);
				this.getView().byId(ID_BOTAO_TELA_CHEIA).setVisible(true);
			});	
		},

		aoClicarEmEditar: function(){
            this._exibirEspera(() => {
                this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
                this.obterRota().navTo(ConstantesDaRota.NOME_DA_ROTA_DE_EDITAR_CLIENTE, {
                    clienteId: this._modeloCliente().getProperty(PROPRIEDADE_ID_DO_CLIENTE)
                });
            });
        },

		aoClicarEmDeletar: function(){ 
			this._exibirEspera( () => {
				MessageBox.warning(this.obterTextoI18n(MSG_DE_AVISO_AO_DELETAR_I18N), {
					actions: [ OPCAO_SIM,  OPCAO_NAO],
					onClose: async (sAction) => {
						this._exibirEspera(async () => {
							if (sAction ===  OPCAO_SIM) {
									await HttpRequest._request(ConstatesDasRequests.REQUISICAO_DELETE, ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[ID_DO_CLIENTE_NA_ROTA])
									this._sucessoNaRequicao(this.obterTextoI18n(MSG_DE_SUCESSO_AO_DELETAR_I18N))
							} 
						});
					}
				})
			});	
		}
	});
});
