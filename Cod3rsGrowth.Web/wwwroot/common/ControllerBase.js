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
                .catch(x => {
                const reader = x.body.getReader()
                    let a = new ReadableStream({
                        start(controller) {
                            function enqueueValues() {
                                reader.read()
                                .then(({ done, value }) => {
                                    if (done) {
                                        controller.close()
                                        return
                                    }
                                    controller.enqueue(value)
    
                                    enqueueValues();
                                })
                            }
                            enqueueValues()
                        }})
                        return new Response(a).json().then((x)=>{
                            this._falhaNaRequicao(x)
                        })
                
                })
                .finally(()=>{
                    this.obterModelo(NOME_MODELO_DO_APP).setProperty("/busy", false);
                });
            
        },

        _modelo: function (oModel, sNomeModelo) {
            return this.getView().setModel(oModel, sNomeModelo);
        },

        _falhaNaRequicao: function(data){
            const detalhesDoErro = data.extensions.errors.join('\n');
            const mensagemErro = `
            Tipo: ${data.type}
            Título: ${data.title}
            Status: ${data.status}
            Detalhes: ${data.detail}
            Erros: ${detalhesDoErro}`;
    
            MessageBox.error(`${MSG_DE_ERRO}\n${mensagemErro}`);
        },
    });
});
