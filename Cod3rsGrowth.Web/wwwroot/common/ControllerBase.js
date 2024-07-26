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

        peloId: function(sId) {
            return this.getView().byId(sId)
        },

        aoClicarEmVoltar: function () {
			var oHistory, sPreviousHash;

			oHistory = History.getInstance();
			sPreviousHash = oHistory.getPreviousHash();

			if (sPreviousHash !== undefined) {
				window.history.go(-1);
			} else {
				this.getRota().navTo(ROTA_PAGINA_PRINCIPAL, {}, true);
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

        _get: async function(url, sNomeModelo){
            this._exibirEspera( async () => {
                const response = await fetch(url, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                },
                });
                if (response.ok) {
                const data = await response.json();
                const oModel = new JSONModel(data);
                    
                return this._modelo(oModel, sNomeModelo);
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
	});
});
