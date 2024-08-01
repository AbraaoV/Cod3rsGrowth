sap.ui.define([
    "sap/ui/core/Messaging",
    "ui5/codersgrowth/common/ControllerBase",
    'sap/ui/model/json/JSONModel',
    "sap/m/MessageBox",
    "ui5/codersgrowth/common/ConstantesDoBanco",
    "ui5/codersgrowth/common/ConstantesLayoutDoApp",
    "ui5/codersgrowth/common/ConstantesDaRota",
    "../model/formatter"
], (Messaging, ControllerBase, JSONModel, MessageBox, ConstantesDoBanco, ConstantesLayoutDoApp, ConstantesDaRota, formatter) => {
    "use strict";
    const CAMINHO_PARA_API_ENUM = "/api/EnumTipo"
    const NOME_DO_MODELO_DA_COMBOX_BOX = "comboxTipoDePessoa"
    const MSG_SUCESSO_CADASATRO_CLIENTE = "Cliente cadastrado com sucesso."
    const MSG_SUCESSO_EDITAR_CLIENTE = "Cliente editado com sucesso."
    const MSG_ERRO_ADICIONAR_CLIENTE = "Erro ao adicionar cliente:"
    const OPCAO_NOVO_CADASTRO = "Novo Cadastro"
    const OPCAO_VOLTAR_PARA_PAGINA_INICIAL = "Voltar à Página Inicial"
    const ID_COMBO_BOX = "comboxTipo"
    const ID_LABEL_CPF = "labelCpf"
    const ID_INPUT_CPF = "inputCpf"
    const ID_LABEL_CPNJ = "labelCnpj"
    const ID_INPUT_CNPJ = "inputCnpj"
    const ID_INPUT_NOME = "inputNome"
    const KEY_PESSOA_FISICA = "Fisica"
    const KEY_PESSOA_JURIDICA = "Juridica"
    const PARAMETRO_ITEM_SELECIONADO = "selectedItem"
    const MSG_DE_ERRO_DE_VALIDACAO = "Ocorreu um ou mais erros de validação."
    const INDEX_CPF = 1
    const INDEX_CPNJ = 2
    const VALOR_PADRAO = "None"
    const VALOR_DE_ERRO = "Error"
    const VALOR_PROPRIEDAE = "value"
    const NOME_DO_MODELO_DOS_FILTROS = "modeloFiltro"
    const NOME_DO_MODELO_DO_CLIENTE = "clienteSelecionado"
    const INDEX_DO_ID_DO_CLIENTE_NA_ROTA = 1
    const TITULO_DO_PANEL_CADASTRO = "Cadastro de Cliente"
    const TITULO_DO_PANEL_EDITAR = "Editar Cliente"
    const ID_PANEL = "container-codersgrowth---adicionarEditarCliente--panelCliente"
    const PROPRIEDADE_NOME = "/nome"
    const PROPRIEDADE_CPF = "/cpf"
    const PROPRIEDADE_CNPJ = "/cnpj"
    const PROPRIEDADE_TIPO = "/tipo"
    const ROTA_EDITAR = "editar"
    const INDEX_DO_NOME_DA_ROTA = 2

    return ControllerBase.extend("ui5.codersgrowth.app.adicionarEditarCliente.AdicionarEditarCliente", {
        onInit: async function() {
            this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_ADICIONAR_CLIENTE).attachPatternMatched(this._aoCoincidirRota, this);
            this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_EDITAR_CLIENTE).attachPatternMatched(this._aoCoincidirRota, this);
            this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_EDITAR_CLIENTE).attachPatternMatched(this._aoCoincidirRotaEditar, this);
        },

        _aoCoincidirRota: async function(){
            this._modelo(await this._get(CAMINHO_PARA_API_ENUM), NOME_DO_MODELO_DA_COMBOX_BOX)
            this.aoSelecionarTipoPessoa();
            this._registarModeloParaVailidacao()
            this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
            this.peloId(ID_COMBO_BOX).setSelectedKey(KEY_PESSOA_FISICA);
            this._setarTituloDoPanel();
        },

        _aoCoincidirRotaEditar: async function(){
            this._modelo(await this._get(ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[INDEX_DO_ID_DO_CLIENTE_NA_ROTA]), NOME_DO_MODELO_DO_CLIENTE);
            this._prencherCliente();
        },

        _setarTituloDoPanel: function(){
            if(this.obterParametros()[INDEX_DO_NOME_DA_ROTA] === ROTA_EDITAR){
                sap.ui.getCore().byId(ID_PANEL).setHeaderText(TITULO_DO_PANEL_EDITAR)
            }else{
                sap.ui.getCore().byId(ID_PANEL).setHeaderText(TITULO_DO_PANEL_CADASTRO)
            }
        },

        _prencherCliente: function(){
            let comboBox = this.peloId(ID_COMBO_BOX);

            const modeloCliente = this.obterModelo(NOME_DO_MODELO_DO_CLIENTE)
            this.peloId(ID_INPUT_NOME).setValue(modeloCliente.getProperty(PROPRIEDADE_NOME));
            comboBox.setSelectedKey(modeloCliente.getProperty(PROPRIEDADE_TIPO)).fireSelectionChange({
                selectedItem: comboBox.getSelectedItem(),
                key: comboBox.getSelectedItem().getKey()
            });
            this.peloId(ID_INPUT_CPF).setValue(formatter.formatarCpf(modeloCliente.getProperty(PROPRIEDADE_CPF)));
            this.peloId(ID_INPUT_CNPJ).setValue(formatter.formatarCpf(modeloCliente.getProperty(PROPRIEDADE_CNPJ)));
        },

        _registarModeloParaVailidacao: function(){
            let oView = this.getView(),
            oMM = Messaging;

            oView.setModel(new JSONModel({ name: undefined, cpf: undefined, cnpj: undefined}), NOME_DO_MODELO_DOS_FILTROS);

            oMM.registerObject(oView.byId(ID_INPUT_NOME), true);
            oMM.registerObject(oView.byId(ID_INPUT_CPF), true);
            oMM.registerObject(oView.byId(ID_INPUT_CNPJ), true);
        },

        _sucessoNaRequicao: function(msgSucesso, requicaoPuT){
            let opcoes = [OPCAO_NOVO_CADASTRO, OPCAO_VOLTAR_PARA_PAGINA_INICIAL]
            if(requicaoPuT){
                opcoes = [OPCAO_VOLTAR_PARA_PAGINA_INICIAL]
            }
            MessageBox.success(msgSucesso, {
                actions: opcoes,
                onClose: (sAction) => {
                    if (sAction === OPCAO_NOVO_CADASTRO) {
                        () => this._limparCampos(); 
                    } else if (sAction === OPCAO_VOLTAR_PARA_PAGINA_INICIAL) {
                        () => this._limparCampos(); 
                        () => this.getOwnerComponent().getRouter().navTo(ConstantesDaRota.NOME_DA_ROTA_DA_LISTA_CLIENTE);
                    }
                }
            });
        },

        _falhaNaRequicao: function(data){
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
            if(sInputSemMascara === ""){
				sEstadoDoValor = VALOR_DE_ERRO;
                bErroDeVaidacao = true;
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

        aoSelecionarTipoPessoa: function(){
            this._exibirEspera(() => {
                let oComboBox = this.peloId(ID_COMBO_BOX);
                let oLabelCPF = this.peloId(ID_LABEL_CPF);
                let oInputCPF = this.peloId(ID_INPUT_CPF);
                let oLabelCNPJ = this.peloId(ID_LABEL_CPNJ);
                let oInputCNPJ = this.peloId(ID_INPUT_CNPJ);
                oComboBox.attachSelectionChange(function(oEvent) {
                    let itemSelecionado = oEvent.getParameter(PARAMETRO_ITEM_SELECIONADO);
                    let key = itemSelecionado.getKey();
                    oInputCPF.setValue(undefined);
                    oInputCNPJ.setValue(undefined);
                    oLabelCPF.setVisible(key === KEY_PESSOA_FISICA);
                    oInputCPF.setVisible(key === KEY_PESSOA_FISICA);
                    oLabelCNPJ.setVisible(key === KEY_PESSOA_JURIDICA);
                    oInputCNPJ.setVisible(key === KEY_PESSOA_JURIDICA);
                });
            });
        },

        aoClicarEmSalvar: function(){
            this._exibirEspera(() => {
                const nome = this.peloId(ID_INPUT_NOME);
                const cpf = this.peloId(ID_INPUT_CPF);
                const cnpj = this.peloId(ID_INPUT_CNPJ);
                let oComboBox = this.peloId(ID_COMBO_BOX);
                const tipoPessoa = oComboBox.getSelectedKey();

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
                
                let cliente = {
                    nome: nome.getValue(),
                    cpf: cpf.getValue().replace(/\D/g, ''),
                    cnpj: cnpj.getValue().replace(/\D/g, ''),
                    tipo: tipoPessoa
                };
                if(this.obterParametros()[INDEX_DO_NOME_DA_ROTA] === ROTA_EDITAR){
                    this._atualizarCliente(cliente);
                }else{
                    this._adicionarCliente(cliente);
                }
                
                
            });    
        },

        _adicionarCliente: function(cliente){
            this._post(ConstantesDoBanco.CAMINHO_PARA_API, cliente, () => this._sucessoNaRequicao(MSG_SUCESSO_CADASATRO_CLIENTE), this._falhaNaRequicao);
        },

        _atualizarCliente: function(cliente){
            this._put(ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[INDEX_DO_ID_DO_CLIENTE_NA_ROTA], cliente, () => this._sucessoNaRequicao(MSG_SUCESSO_EDITAR_CLIENTE, true), this._falhaNaRequicao);
        },  
        
        _limparCampos: function() {
            this.peloId(ID_INPUT_NOME).setValue(undefined);
            this.peloId(ID_INPUT_CPF).setValue(undefined);
            this.peloId(ID_INPUT_CNPJ).setValue(undefined);
            this.peloId(ID_COMBO_BOX).setSelectedKey(KEY_PESSOA_FISICA);
        },
        
    });
});