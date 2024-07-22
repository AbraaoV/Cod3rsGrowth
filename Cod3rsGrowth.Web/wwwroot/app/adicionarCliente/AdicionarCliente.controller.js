sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    'sap/ui/model/json/JSONModel',
    "sap/m/MessageBox"
], (Controller, History, JSONModel, MessageBox) => {
    "use strict";
    const ROTA_ADICIONAR_CLIENTE = "adicionarCliente"
    const ROTA_PAGINA_PRINCIPAL = "lista"
    const CAMINHO_PARA_API = "/api/Cliente";
    const CAMINHO_PARA_API_ENUM = "/api/EnumTipo"
    const ID_DA_PAGINA = "pagina"
    const NOME_DO_MODELO_DA_COMBOX_BOX = "comboxTipoDePessoa"
    const MSG_DE_ERRO = "Ocorreu um erro: "
    const MSG_SUCESSO_CADASATRO_CLIENTE = "Cliente cadastrado com sucesso"
    const MSG_ERRO_ADICIONAR_CLIENTE = "Erro ao adicionar cliente:"
    const OPCAO_NOVO_CADASTRO = "Novo Cadastro"
    const OPCAO_VOLTAR_PARA_PAGINA_INICIAL = "Voltar à Página Inicial"
    const ID_COMBO_BOX = "comboxTipo"
    const ID_LABEL_CPF = "labelCpf"
    const ID_INPUT_CPF = "inputCpf"
    const ID_LABEL_CPNJ = "labelCnpj"
    const ID_INPUT_CNPJ = "inputCnpj"
    const ID_INPUT_NOME = "inputNome"
    const KEY_PESSOA_FISICA = "1"
    const KEY_PESSOA_JURIDICA = "2"
    const PARAMETRO_ITEM_SELECIONADO = "selectedItem"

    return Controller.extend("ui5.codersgrowth.adicionarCliente.AdicionarCliente", {
        onInit: async function() {
            const oRota = this.getOwnerComponent().getRouter();
            oRota.getRoute(ROTA_ADICIONAR_CLIENTE).attachPatternMatched(this._prencherComboBox, this);

           
        },

        _prencherComboBox: function(){
            this._get(CAMINHO_PARA_API_ENUM)
            this.aoSelecionarTipoPessoa();
        },

        _modeloComboBox: function(oModel,){
            this.getView().setModel(oModel, NOME_DO_MODELO_DA_COMBOX_BOX);
        },
        _get: async function(url){
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
            
                    return this._modeloComboBox(oModel);
                }
               
            });
        },
        _post: async function(url, corpo){
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
                    this._sucessoNaRequicaoPost();
                }
                else{ 
                    this._falhaNaRequicaoPost(data);                    
                }
            });
        },
        _sucessoNaRequicaoPost: function(){
            MessageBox.success(MSG_SUCESSO_CADASATRO_CLIENTE, {
                actions: [OPCAO_NOVO_CADASTRO, OPCAO_VOLTAR_PARA_PAGINA_INICIAL],
                onClose: (sAction) => {
                    if (sAction === OPCAO_NOVO_CADASTRO) {
                        this._limparCampos(); 
                    } else if (sAction === OPCAO_VOLTAR_PARA_PAGINA_INICIAL) {
                        this._limparCampos(); 
                        this.getOwnerComponent().getRouter().navTo(ROTA_PAGINA_PRINCIPAL);
                    }
                }
            });
        },
        _falhaNaRequicaoPost: function(data){
            const detalhesDoErro = data.extensions.errors.join('\n');
            const mensagemErro = `
            Tipo: ${data.type}
            Título: ${data.title}
            Status: ${data.status}
            Detalhes: ${data.detail}
            Erros: ${detalhesDoErro}`;
    
            MessageBox.error(`${MSG_ERRO_ADICIONAR_CLIENTE}\n${mensagemErro}`);
        },

        aoClicarEmVoltar: function(){
            this._exibirEspera(() => {
                const oHistory = History.getInstance();
                const sPreviousHash = oHistory.getPreviousHash();

                if (sPreviousHash !== undefined) {
                    window.history.go(-1);
                } else {
                    const oRouter = this.getOwnerComponent().getRouter();
                    oRouter.navTo(ROTA_PAGINA_PRINCIPAL, {}, true);
                }
            });
        },
        aoSelecionarTipoPessoa: function(){
            this._exibirEspera(() => {
                let oView = this.getView();
                let oComboBox = oView.byId(ID_COMBO_BOX);
                let oLabelCPF = oView.byId(ID_LABEL_CPF);
                let oInputCPF = oView.byId(ID_INPUT_CPF);
                let oLabelCNPJ = oView.byId(ID_LABEL_CPNJ);
                let oInputCNPJ = oView.byId(ID_INPUT_CNPJ);

                oComboBox.attachSelectionChange(function(oEvent) {
                    let itemSelecionado = oEvent.getParameter(PARAMETRO_ITEM_SELECIONADO);
                    let key = itemSelecionado.getKey();
                
                    oLabelCPF.setVisible(key === KEY_PESSOA_FISICA);
                    oInputCPF.setVisible(key === KEY_PESSOA_FISICA);
                    oLabelCNPJ.setVisible(key === KEY_PESSOA_JURIDICA);
                    oInputCNPJ.setVisible(key === KEY_PESSOA_JURIDICA);
                });
            });
        },
        aoClicarEmSalvar: function(){
            this._exibirEspera(() => {
                let oView = this.getView();
                let oComboBox = oView.byId(ID_COMBO_BOX);
                const tipoPessoa = parseInt(oComboBox.getSelectedKey(), 10);
                
                function removerMascara(value) {
                    return value.replace(/\D/g, '');
                }
                const cpfSemMascara = removerMascara(this.oView.byId(ID_INPUT_CPF).getValue())
                const cnpjSemMascara = removerMascara(this.oView.byId(ID_INPUT_CNPJ).getValue())
                let corpo = {
                    nome: this.oView.byId(ID_INPUT_NOME).getValue(),
                    cpf: cpfSemMascara,
                    cnpj: cnpjSemMascara,
                    tipo: tipoPessoa
                }
                debugger

                this._post(CAMINHO_PARA_API, corpo)
            });    
        },
        _limparCampos: function() {
            const oView = this.getView();
            oView.byId(ID_INPUT_NOME).setValue("");
            oView.byId(ID_INPUT_CPF).setValue("");
            oView.byId(ID_INPUT_CNPJ).setValue("");
            oView.byId(ID_COMBO_BOX).setSelectedKey(KEY_PESSOA_FISICA);
        },
        
        _exibirEspera: function(funcao) {
            let oPagina = this.byId(ID_DA_PAGINA);
            oPagina.setBusy(true);
            
            try {
               funcao();
            } catch(error) {
               MessageBox.error(MSG_DE_ERRO + error.message);
            } finally {
               oPagina.setBusy(false)
            }
         },
    });
});