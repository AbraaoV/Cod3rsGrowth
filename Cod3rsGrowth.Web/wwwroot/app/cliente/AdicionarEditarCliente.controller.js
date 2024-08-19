sap.ui.define([
    "sap/ui/core/Messaging",
    "ui5/codersgrowth/common/ControllerBase",
    'sap/ui/model/json/JSONModel',
    "sap/m/MessageBox",
    "ui5/codersgrowth/common/ConstantesDoBanco",
    "ui5/codersgrowth/common/ConstantesLayoutDoApp",
    "ui5/codersgrowth/common/ConstantesDaRota",
    "../model/formatter",
    "ui5/codersgrowth/common/HttpRequest",
    "ui5/codersgrowth/common/ConstatesDasRequests",
    "sap/ui/core/routing/History",

], (Messaging, ControllerBase, JSONModel, MessageBox, ConstantesDoBanco, ConstantesLayoutDoApp, ConstantesDaRota, formatter, HttpRequest, ConstatesDasRequests, History) => {
    "use strict";
    const CAMINHO_PARA_API_ENUM = "/api/EnumTipo"
    const NOME_DO_MODELO_DA_COMBOX_BOX = "comboxTipoDePessoa"
    const MSG_DE_SUCESSO_NO_CADASTRO_I18N = "sucessCustumerRegister"
    const MSG_DE_SUCESSO_NA_EDICAO_I18N = "sucessCustumerEdit"
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
    const NOME_DO_MODELO_DO_CLIENTE = "clienteSelecionado"
    const INDEX_DO_ID_DO_CLIENTE_NA_ROTA = 1
    const TITULO_DO_PANEL_CADASTRO = "Cadastro de Cliente"
    const TITULO_DO_PANEL_EDITAR = "Editar Cliente"
    const ID_PANEL = "panelCliente"
    const PROPRIEDADE_NOME = "/nome"
    const PROPRIEDADE_CPF = "/cpf"
    const PROPRIEDADE_CNPJ = "/cnpj"
    const PROPRIEDADE_TIPO = "/tipo"
    const ROTA_EDITAR = "editar"
    const INDEX_DO_NOME_DA_ROTA = 2

    return ControllerBase.extend("ui5.codersgrowth.app.cliente.AdicionarEditarCliente", {
        onInit: async function() {
            this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_ADICIONAR_CLIENTE).attachPatternMatched(this._aoCoincidirRota, this);
            this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_EDITAR_CLIENTE).attachPatternMatched(this._aoCoincidirRotaEditar, this);
        },

        _aoCoincidirRota: async function(){
            this._exibirEspera(async () => {
                await this._definirValoresPadroes();
                this._limparCampos();
            })
        },

        _aoCoincidirRotaEditar: async function(){
            this._exibirEspera(async () =>{
                await this._definirValoresPadroes()
                let resultado = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[INDEX_DO_ID_DO_CLIENTE_NA_ROTA]);
                this._modelo(new JSONModel(resultado), NOME_DO_MODELO_DO_CLIENTE);
                this._prencherCliente();
                this._definirTituloEdicao();
            })
        },

        _definirValoresPadroes: async function(){
            let retorno = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, CAMINHO_PARA_API_ENUM);
            this._modelo(new JSONModel(retorno), NOME_DO_MODELO_DA_COMBOX_BOX)
            this.aoSelecionarTipoPessoa();
            this._registarModeloParaVailidacao()
            this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
            this.getView().byId(ID_COMBO_BOX).setSelectedKey(KEY_PESSOA_FISICA);
            this._definitTituloCadastro();
        },

        _definirTituloEdicao: function(){
            this.getView().byId(ID_PANEL).setHeaderText(TITULO_DO_PANEL_EDITAR)
        },

        _definitTituloCadastro: function(){
            this.getView().byId(ID_PANEL).setHeaderText(TITULO_DO_PANEL_CADASTRO)
        },

        _prencherCliente: function(){
            let controleDoComboBox = this.getView().byId(ID_COMBO_BOX);

            const modeloCliente = this.obterModelo(NOME_DO_MODELO_DO_CLIENTE)
            this.getView().byId(ID_INPUT_NOME).setValue(modeloCliente.getProperty(PROPRIEDADE_NOME));
            for(var i = 0; i < 2; i++){
                if(controleDoComboBox.getItems()[i].getText() == modeloCliente.getProperty(PROPRIEDADE_TIPO)){
                    controleDoComboBox.setSelectedItem(controleDoComboBox.getItems()[i], true)
                }
            }
            let item = controleDoComboBox.getSelectedItem()
            controleDoComboBox.setSelectedItem(modeloCliente.getProperty(PROPRIEDADE_TIPO)).fireSelectionChange({
                selectedItem: item,
                key: item.getKey()
            });
            this.getView().byId(ID_INPUT_CPF).setValue(formatter.formatarCpf(modeloCliente.getProperty(PROPRIEDADE_CPF)));
            this.getView().byId(ID_INPUT_CNPJ).setValue(formatter.formatarCnpj(modeloCliente.getProperty(PROPRIEDADE_CNPJ)));
        },

        _registarModeloParaVailidacao: function(){
            let oView = this.getView(),
            oMM = Messaging;

            oView.setModel(new JSONModel({ name: "", cpf: "", cnpj: ""}), NOME_DO_MODELO_DOS_FILTROS);

            oMM.registerObject(oView.byId(ID_INPUT_NOME), true);
            oMM.registerObject(oView.byId(ID_INPUT_CPF), true);
            oMM.registerObject(oView.byId(ID_INPUT_CNPJ), true);
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
                let controleDoComboBox = this.getView().byId(ID_COMBO_BOX);
                let controleDaLabelCPF = this.getView().byId(ID_LABEL_CPF);
                let controleDoInputCPF = this.getView().byId(ID_INPUT_CPF);
                let controleDaLabelCNPJ = this.getView().byId(ID_LABEL_CPNJ);
                let controleDoInputCNPJ = this.getView().byId(ID_INPUT_CNPJ);
                controleDoComboBox.attachSelectionChange(function(oEvent) {
                    let itemSelecionado = oEvent.getParameter(PARAMETRO_ITEM_SELECIONADO);
                    let key = itemSelecionado.getKey();
                    controleDoInputCPF.setValue(undefined);
                    controleDoInputCNPJ.setValue(undefined);
                    controleDaLabelCPF.setVisible(key === KEY_PESSOA_FISICA);
                    controleDoInputCPF.setVisible(key === KEY_PESSOA_FISICA);
                    controleDaLabelCNPJ.setVisible(key === KEY_PESSOA_JURIDICA);
                    controleDoInputCNPJ.setVisible(key === KEY_PESSOA_JURIDICA);
                });
            });
        },

        aoClicarEmSalvar: function(){
            this._exibirEspera(async () => {
                const nome = this.getView().byId(ID_INPUT_NOME);
                const cpf = this.getView().byId(ID_INPUT_CPF);
                const cnpj = this.getView().byId(ID_INPUT_CNPJ);
                let oComboBox = this.getView().byId(ID_COMBO_BOX);
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
                    tipo: parseInt(tipoPessoa)
                };
                if(this.obterParametros()[INDEX_DO_NOME_DA_ROTA] === ROTA_EDITAR){
                    await this._atualizarCliente(cliente);
                }else{
                    await this._adicionarCliente(cliente);
                }
                
                
            });    
        },

        _adicionarCliente: async function(cliente){
            const MSG_SUCESSO_CADASATRO_CLIENTE = this.obterTextoI18n(MSG_DE_SUCESSO_NO_CADASTRO_I18N);
            await HttpRequest._request(ConstatesDasRequests.REQUISICAO_POST, ConstantesDoBanco.CAMINHO_PARA_API, cliente);
            this._sucessoNaRequicao(MSG_SUCESSO_CADASATRO_CLIENTE, true)
        },

        _atualizarCliente: async function(cliente){
            const MSG_SUCESSO_EDITAR_CLIENTE = this.obterTextoI18n(MSG_DE_SUCESSO_NA_EDICAO_I18N);
            await HttpRequest._request(ConstatesDasRequests.REQUISICAO_PUT, ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[INDEX_DO_ID_DO_CLIENTE_NA_ROTA], cliente);
            this._sucessoNaRequicao(MSG_SUCESSO_EDITAR_CLIENTE)
        },  
        
        _limparCampos: function() {
            this.getView().byId(ID_INPUT_NOME).setValue(undefined).setValueState(undefined);
            this.getView().byId(ID_INPUT_CPF).setValue(undefined).setValueState(undefined);
            this.getView().byId(ID_INPUT_CNPJ).setValue(undefined).setValueState(undefined);
            this.getView().byId(ID_COMBO_BOX).setSelectedKey(KEY_PESSOA_FISICA);
        },
        
    });
});