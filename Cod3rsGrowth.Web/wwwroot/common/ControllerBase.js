sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    "sap/m/MessageBox",
    "ui5/codersgrowth/common/ConstantesDaRota",
], function (Controller, History, MessageBox, ConstantesDaRota) {
    "use strict";

    const MSG_DE_ERRO = "Ocorreu um erro: "
    const ROTA_PAGINA_PRINCIPAL = "lista"
    const NOME_MODELO_DO_APP = "appView"
    const OPCAO_VOLTAR_PARA_PAGINA_INICIAL = "Voltar à Página Inicial"
    const OPCAO_NOVO_CADASTRO = "Novo Cadastro"
    

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
            let opcoes = [OPCAO_VOLTAR_PARA_PAGINA_INICIAL]
            if(requicaoPost){
                opcoes = [OPCAO_NOVO_CADASTRO, OPCAO_VOLTAR_PARA_PAGINA_INICIAL]
            }
            MessageBox.success(msgSucesso, {
                actions: opcoes,
                onClose: (sAction) => {
                    if (sAction === OPCAO_NOVO_CADASTRO) {
                        () => this._limparCampos(); 
                    } else if (sAction === OPCAO_VOLTAR_PARA_PAGINA_INICIAL) {
                        () => this._limparCampos(); 
                        this.obterRota().navTo(ConstantesDaRota.NOME_DA_ROTA_DA_LISTA_CLIENTE);
                    }
                }
            });
        },
    });
});