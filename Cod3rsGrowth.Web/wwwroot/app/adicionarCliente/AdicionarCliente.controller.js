sap.ui.define([
    "sap/ui/core/Messaging",
    "sap/ui/core/mvc/Controller",
    "sap/ui/core/routing/History",
    'sap/ui/model/json/JSONModel',
    "sap/m/MessageBox",
], (Messaging, Controller, History, JSONModel, MessageBox) => {
    "use strict";
    const ROTA_ADICIONAR_CLIENTE = "adicionarCliente"
    const ROTA_PAGINA_PRINCIPAL = "lista"
    const CAMINHO_PARA_API = "/api/Cliente";
    const CAMINHO_PARA_API_ENUM = "/api/EnumTipo"
    const ID_DA_PAGINA = "paginaAdicionar"
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
    const MSG_DE_ERRO_DE_VALIDACAO = "Ocorreu um ou mais erros de validação."
    const INDEX_CPF = 1
    const INDEX_CPNJ = 2
    const VALOR_PADRAO = "None"
    const VALOR_DE_ERRO = "Error"
    const VALOR_PROPRIEDAE = "value"
    const NOME_DO_MODELO_DOS_FILTROS = "modeloFiltro"

    return Controller.extend("ui5.codersgrowth.adicionarCliente.AdicionarCliente", {
        onInit: async function() {
            const oRota = this.getOwnerComponent().getRouter();
            oRota.getRoute(ROTA_ADICIONAR_CLIENTE).attachPatternMatched(this._prencherComboBox, this);
        },

        _prencherComboBox: function(){
            this._get(CAMINHO_PARA_API_ENUM)
            this.aoSelecionarTipoPessoa();
            this._registarModeloParaVailidacao()
        },

        _registarModeloParaVailidacao: function(){
            let oView = this.getView(),
            oMM = Messaging;

            oView.setModel(new JSONModel({ name: undefined, cpf: undefined, cnpj: undefined}), NOME_DO_MODELO_DOS_FILTROS);

            oMM.registerObject(oView.byId(ID_INPUT_NOME), true);
            oMM.registerObject(oView.byId(ID_INPUT_CPF), true);
            oMM.registerObject(oView.byId(ID_INPUT_CNPJ), true);
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

        _validarInput: function (oInput) {
			let sEstadoDoValor = VALOR_PADRAO;
			let bErroDeVaidacao = false;
			let oBinding = oInput.getBinding(VALOR_PROPRIEDAE);

            let sInputSemMascara = oInput.getValue();
            if (oInput === this.getView().byId(ID_INPUT_CPF) || oInput === this.getView().byId(ID_INPUT_CNPJ)) {
                sInputSemMascara = sInputSemMascara.replace(/\D/g, '');
            }
			try {
				oBinding.getType().validateValue(sInputSemMascara);
			} catch (oException) {
				sEstadoDoValor = VALOR_DE_ERRO;
				bErroDeVaidacao = true;
			}

			oInput.setValueState(sEstadoDoValor);

			return bErroDeVaidacao;
		},

        aoDigitarNoInpunt: function(oEvent) {
            this._exibirEspera(() => {
                let oInput = oEvent.getSource();
                this._validarInput(oInput);
            });
		},

        aoClicarEmVoltar: function(){
            this._exibirEspera(() => {
                const oHistorico = History.getInstance();
                const sHashAnterior = oHistorico.getPreviousHash();

                if (sHashAnterior !== undefined) {
                    window.history.go(-1);
                } else {
                    const oRota = this.getOwnerComponent().getRouter();
                    oRota.navTo(ROTA_PAGINA_PRINCIPAL, {}, true);
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
                    oView.byId(ID_INPUT_CPF).setValue(undefined);
                    oView.byId(ID_INPUT_CNPJ).setValue(undefined);
                    oLabelCPF.setVisible(key === KEY_PESSOA_FISICA);
                    oInputCPF.setVisible(key === KEY_PESSOA_FISICA);
                    oLabelCNPJ.setVisible(key === KEY_PESSOA_JURIDICA);
                    oInputCNPJ.setVisible(key === KEY_PESSOA_JURIDICA);
                });
            });
        },

        aoClicarEmSalvar: function(){
            this._exibirEspera(() => {
                let oView = this.getView()
                const nome = this.oView.byId(ID_INPUT_NOME);
                const cpf = this.oView.byId(ID_INPUT_CPF);
                const cnpj = this.oView.byId(ID_INPUT_CNPJ);
                let oComboBox = oView.byId(ID_COMBO_BOX);
                const tipoPessoa = parseInt(oComboBox.getSelectedKey(), 10);

				let aInputs = [
                    nome,
                    cpf,
                    cnpj
                ],
				bErroDeVaidacao = false;
                
                if(oComboBox.getSelectedKey() === KEY_PESSOA_FISICA){
                    delete aInputs[INDEX_CPNJ]
                }
                if(oComboBox.getSelectedKey() === KEY_PESSOA_JURIDICA){
                    delete aInputs[INDEX_CPF]
                }
                aInputs.forEach(function (oInput) {
                    bErroDeVaidacao = this._validarInput(oInput) || bErroDeVaidacao;
                }   , this);
                if (bErroDeVaidacao) {
                    MessageBox.alert(MSG_DE_ERRO_DE_VALIDACAO);
                    return;
                } 
                
                let corpo = {
                    nome: nome.getValue(),
                    cpf: cpf.getValue().replace(/\D/g, ''),
                    cnpj: cnpj.getValue().replace(/\D/g, ''),
                    tipo: tipoPessoa
                };
                
                this._post(CAMINHO_PARA_API, corpo);
            });    
        },
        
        _limparCampos: function() {
            const oView = this.getView();
            oView.byId(ID_INPUT_NOME).setValue(undefined);
            oView.byId(ID_INPUT_CPF).setValue(undefined);
            oView.byId(ID_INPUT_CNPJ).setValue(undefined);
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