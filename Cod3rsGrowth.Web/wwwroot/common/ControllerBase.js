sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/ui/core/UIComponent",
    "sap/m/MessageBox",
    "sap/ui/model/json/JSONModel"
], function (Controller, History, UIComponent, MessageBox, JSONModel) {
    "use strict";

    const MSG_DE_ERRO = "Ocorreu um erro: "
    const ROTA_PAGINA_PRINCIPAL = "lista"
    const NOME_MODELO_DO_APP = "appView"

    return Controller.extend("ui5.codersgrowth.common.ControllerBase", {
        obterRota: function () {
            return this.getOwnerComponent().getRouter();
        },

        obterParametros: function () {
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

        mudarLayout: function (sLayout) {
            return this.obterModelo(NOME_MODELO_DO_APP).setProperty("/layout", sLayout);
        },

        obterModelo: function (sNome) {
            return this.getView().getModel(sNome);
        },

        _exibirEspera: function (funcao) {
            this.obterModelo(NOME_MODELO_DO_APP).setProperty("/busy", true);
        
            return Promise.resolve(funcao())
                .catch(error => {
                    if (error.body && error.body.getReader) {
                        const leitor = error.body.getReader();
                        let mensagemDeErro = new ReadableStream({
                            start(controller) {
                                function enfileirarValores() {
                                    leitor.read()
                                        .then(({ done, value }) => {
                                            if (done) {
                                                controller.close();
                                                return;
                                            }
                                            controller.enqueue(value);
                                            enfileirarValores();
                                        })
                                        .catch(err => {
                                            MessageBox.error(MSG_DE_ERRO + err)
                                        });
                                }
                                enfileirarValores();
                            }
                        });
                        return new Response(mensagemDeErro).json().then(error => {
                            this._formatarMensagemDeErro(error);
                        });
                    } else {
                        return MessageBox.error(MSG_DE_ERRO + error.message, {
                            details: error.stack,
                            contentWidth: "25%",
                        });
                    }
                })
                .finally(() => {
                    this.obterModelo(NOME_MODELO_DO_APP).setProperty("/busy", false);
                });
        },
        

        _modelo: function (oModel, sNomeModelo) {
            return this.getView().setModel(oModel, sNomeModelo);
        },

        _formatarMensagemDeErro: function(data) {
            let detalhesDoErro = '';
            if (data.extensions && data.extensions.errors) {
                detalhesDoErro = data.extensions.errors.join('\n');
            }
        
            const mensagemErro = `
                Erros: ${detalhesDoErro}<br>
                Detalhes: ${data.detail}<br>
            `;
        
            MessageBox.error(data.title, {
                details: mensagemErro,
                contentWidth: "40%",
            });
            
        },
    });
});