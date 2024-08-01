sap.ui.define([
	"sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
	"sap/ui/core/UIComponent",
    "sap/m/MessageBox",
    "sap/ui/model/json/JSONModel"
], function(Controller, History, UIComponent, MessageBox, JSONModel) {
	"use strict";

    const MSG_DE_ERRO = "Ocorreu um erro: "
    const ROTA_PAGINA_PRINCIPAL = "lista"
    const NOME_MODELO_DO_APP = "appView"

	return Controller.extend("ui5.codersgrowth.common.ControllerBase", {
        obterRota: function () {
			return this.getOwnerComponent().getRouter();
		},

        obterParametros: function() {
            return this.obterRota().getHashChanger().getHash().split("/");
        },

        aoClicarEmVoltar: function () {
			var oHistory, sPreviousHash;

			oHistory = History.getInstance();
			sPreviousHash = oHistory.getPreviousHash();

			if (sPreviousHash !== undefined) {
				window.history.go(-1);
			} else {
				this.obterRota().navTo(ROTA_PAGINA_PRINCIPAL, {}, true);
			}
		},

        mudarLayout: function(sLayout){
            return this.obterModelo(NOME_MODELO_DO_APP).setProperty("/layout", sLayout);
        },
        
        obterModelo : function (sNome) {
			return this.getView().getModel(sNome);
		},

        _exibirEspera: function(funcao) {
            this.obterModelo(NOME_MODELO_DO_APP).setProperty("/busy", true);
            
            try {
                funcao();
            } catch(error) {
                MessageBox.error(MSG_DE_ERRO + error.message);
            } finally {
                this.obterModelo(NOME_MODELO_DO_APP).setProperty("/busy", false);
            }
        },

        _modelo: function(oModel, sNomeModelo){
            return this.getView().setModel(oModel, sNomeModelo);
        },

        _get: function(url) {
            return new Promise(async (resolve, reject) => {
                this.obterModelo(NOME_MODELO_DO_APP).setProperty("/busy", true);
                try {
                    const response = await fetch(url, {
                        method: "GET",
                        headers: {
                            "Content-Type": "application/json",
                        },
                    });

                    if (response.ok) {
                        const data = await response.json();
                        const oModel = new JSONModel(data);
                        

                        return resolve(oModel);
                    }
                } catch (error) {
                    MessageBox.error(MSG_DE_ERRO + error.message);
                    reject(error);
                } finally {
                    this.obterModelo(NOME_MODELO_DO_APP).setProperty("/busy", false);
                }
            });
        },

        
        _post: async function(url, corpo, respostaSucesso, respostaErro){
            this._exibirEspera( async () => {
                const response = await fetch(url, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(corpo)
                    
                });
                    const data = await response.json()
                if (response.ok) {
                    respostaSucesso();
                }
                else{ 
                    respostaErro(data);                    
                }
            });
        },

        _put: async function(url, corpo, respostaSucesso, respostaErro){
            this._exibirEspera( async () => {
                const response = await fetch(url, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(corpo)
                    
                });
                    const data = response.json()
                if (response.ok) {
                    respostaSucesso();
                }
                else{ 
                    respostaErro(data);                    
                }
            });
        },
	});
});
