sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/m/MessageBox",
    "ui5/codersgrowth/common/ConstantesDaRota",
], function (Controller, History, MessageBox, ConstantesDaRota) {
    "use strict";

    const MSG_DE_ERRO_I18N = "errorMenssage"
    const ROTA_PAGINA_PRINCIPAL = "lista"
    const NOME_MODELO_DO_APP = "appView"
    const TEXTO_VOLTAR_PARA_PAGINA_INCIAL_I18N = "returnToInitialPage"
    const TEXTO_NOVO_CADASTRO_I18N = "newRegistrationMessage"
    const NOME_DO_MODELO_I18N = "i18n"
    

    return Controller.extend("ui5.codersgrowth.common.ControllerBase", {
        obterRota: function () {
            return this.getOwnerComponent().getRouter();
        },

        obterParametros: function () {
            return this.obterRota().getHashChanger().getHash().split("/");
        },

        obterTextoI18n: function(tituloDoTexto){
            return this.getView().getModel(NOME_DO_MODELO_I18N).getResourceBundle().getText(tituloDoTexto);
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
                        return MessageBox.error(this.obterTextoI18n(MSG_DE_ERRO_I18N) + error.message, {
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
                detalhesDoErro = data.extensions.errors
                if(data.extensions.errors.join){
                    detalhesDoErro = data.extensions.errors.join('\n');
                }
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

        _sucessoNaRequicao: function(msgSucesso, requicaoPost){
            let opcoes = [this.obterTextoI18n(TEXTO_VOLTAR_PARA_PAGINA_INCIAL_I18N)]
            if(requicaoPost){
                opcoes = [this.obterTextoI18n(TEXTO_NOVO_CADASTRO_I18N), this.obterTextoI18n(TEXTO_VOLTAR_PARA_PAGINA_INCIAL_I18N)]
            }
            MessageBox.success(msgSucesso, {
                actions: opcoes,
                onClose: (sAction) => {
                    if (sAction === this.obterTextoI18n(TEXTO_NOVO_CADASTRO_I18N)) {
                        () => this._limparCampos(); 
                    } else if (sAction === this.obterTextoI18n(TEXTO_VOLTAR_PARA_PAGINA_INCIAL_I18N)) {
                        () => this._limparCampos(); 
                        this.obterRota().navTo(ConstantesDaRota.NOME_DA_ROTA_DA_LISTA_CLIENTE);
                    }
                }
            });
        },
    });
});