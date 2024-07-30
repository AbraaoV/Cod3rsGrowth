sap.ui.define([
    "sap/ui/core/Messaging",
    "ui5/codersgrowth/common/ControllerBase",
    'sap/ui/model/json/JSONModel',
    "sap/m/MessageBox",
    "ui5/codersgrowth/common/ConstantesDoBanco",
    "ui5/codersgrowth/common/ConstantesLayoutDoApp",
    "ui5/codersgrowth/common/ConstantesDaRota"
], (Messaging, ControllerBase, JSONModel, MessageBox, ConstantesDoBanco, ConstantesLayoutDoApp, ConstantesDaRota) => {
    "use strict";
    const CAMINHO_PARA_API_ENUM = "/api/EnumTipo"
    const NOME_DO_MODELO_DA_COMBOX_BOX = "comboxTipoDePessoa"
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

    return ControllerBase.extend("ui5.codersgrowth.app.adicionarCliente.AdicionarCliente", {
        onInit: async function() {
            this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_ADICIONAR_CLIENTE).attachPatternMatched(this._prencherComboBox, this);
        },

        _prencherComboBox: function(){
            this._get(CAMINHO_PARA_API_ENUM, NOME_DO_MODELO_DA_COMBOX_BOX)
            this.aoSelecionarTipoPessoa();
            this._registarModeloParaVailidacao()
            this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
        },

        _registarModeloParaVailidacao: function(){
            let oView = this.getView(),
            oMM = Messaging;

            oView.setModel(new JSONModel({ name: undefined, cpf: undefined, cnpj: undefined}), NOME_DO_MODELO_DOS_FILTROS);

            oMM.registerObject(oView.byId(ID_INPUT_NOME), true);
            oMM.registerObject(oView.byId(ID_INPUT_CPF), true);
            oMM.registerObject(oView.byId(ID_INPUT_CNPJ), true);
        },

        

        _sucessoNaRequicaoPost: function(){
            MessageBox.success(MSG_SUCESSO_CADASATRO_CLIENTE, {
                actions: [OPCAO_NOVO_CADASTRO, OPCAO_VOLTAR_PARA_PAGINA_INICIAL],
                onClose: (sAction) => {
                    if (sAction === OPCAO_NOVO_CADASTRO) {
                        this._limparCampos(); 
                    } else if (sAction === OPCAO_VOLTAR_PARA_PAGINA_INICIAL) {
                        this._limparCampos(); 
                        this.getOwnerComponent().getRouter().navTo(ConstantesDaRota.NOME_DA_ROTA_DA_LISTA_CLIENTE);
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
                
                this._post(ConstantesDoBanco.CAMINHO_PARA_API, corpo, () => this._sucessoNaRequicaoPost(), this._falhaNaRequicaoPost);
            });    
        },
        
        _limparCampos: function() {
            this.peloId(ID_INPUT_NOME).setValue(undefined);
            this.peloId(ID_INPUT_CPF).setValue(undefined);
            this.peloId(ID_INPUT_CNPJ).setValue(undefined);
            this.peloId(ID_COMBO_BOX).setSelectedKey(KEY_PESSOA_FISICA);
        },
        
    });
});