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
    const ID_INPUT_CPF = "inputCpf"
    const ID_INPUT_CNPJ = "inputCnpj"
    const ID_INPUT_NOME = "inputNome"
    const KEY_PESSOA_FISICA = "1"
    const KEY_PESSOA_JURIDICA = "2"
    const MSG_DE_ERRO_DE_VALIDACAO = "validationErrorMenssage"
    const VALOR_PADRAO = "None"
    const VALOR_DE_ERRO = "Error"
    const NOME_DO_MODELO_DO_CLIENTE = "clienteSelecionado"
    const INDEX_DO_ID_DO_CLIENTE_NA_ROTA = 1
    const TITULO_DO_PANEL_CADASTRO = "Cadastro de Cliente"
    const TITULO_DO_PANEL_EDITAR = "Editar Cliente"
    const ID_PANEL = "panelCliente"
    const PROPRIEDADE_NOME = "/nome"
    const PROPRIEDADE_CPF = "/cpf"
    const PROPRIEDADE_CNPJ = "/cnpj"
    const PROPRIEDADE_TIPO_DE_CLIENTE = "/tipoDeCliente"
    const PROPRIEDADE_TIPO = "/tipo"
    const ROTA_EDITAR = "editar"
    const INDEX_DO_NOME_DA_ROTA = 2
    const NOME_dO_MODELO_DOS_INPUTS = "modeloDosInputs"

    return ControllerBase.extend("ui5.codersgrowth.app.cliente.AdicionarEditarCliente", {
        formatter: formatter,
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
            let modeloDosInputs = this.obterModelo(NOME_dO_MODELO_DOS_INPUTS)

            let controleDoComboBox = this.getView().byId(ID_COMBO_BOX);
            const modeloCliente = this.obterModelo(NOME_DO_MODELO_DO_CLIENTE)
            let cpf = modeloCliente.getProperty(PROPRIEDADE_CPF)
            let cnpj = modeloCliente.getProperty(PROPRIEDADE_CNPJ)
            let nome = modeloCliente.getProperty(PROPRIEDADE_NOME)

            let itemSelecionado = controleDoComboBox.getItems().find(item => item.getText() === modeloCliente.getProperty(PROPRIEDADE_TIPO));
            controleDoComboBox.setSelectedItem(itemSelecionado, true);
            controleDoComboBox.fireSelectionChange({
                selectedItem: itemSelecionado,
                key: itemSelecionado.getKey()
            });
            
            modeloDosInputs.setProperty(PROPRIEDADE_NOME, nome)
            modeloDosInputs.setProperty(PROPRIEDADE_CPF, cpf)
            modeloDosInputs.setProperty(PROPRIEDADE_CNPJ, cnpj)
        },

        _registarModeloParaVailidacao: function(){
            let inputs = {
                nome: "",
                cpf: "",
                cnpj: "",
                tipoDeCliente: "1"
            }

            this._modelo(new JSONModel(inputs), NOME_dO_MODELO_DOS_INPUTS)

            let oView = this.getView(),
            oMM = Messaging;
            oMM.registerObject(oView.byId(ID_INPUT_NOME), true);
            oMM.registerObject(oView.byId(ID_INPUT_CPF), true);
            oMM.registerObject(oView.byId(ID_INPUT_CNPJ), true);
        },

        _validarInputCpf: function(cpf){
            let sEstadoDoValor = VALOR_PADRAO;
            let bErroDeVaidacao = false;

            let sInputSemMascara = cpf.replace(/\D/g, '');
            
            if(sInputSemMascara !== 11){
                sEstadoDoValor = VALOR_DE_ERRO;
                bErroDeVaidacao = true;
            }

            this.getView().byId(ID_INPUT_CPF).setValueState(sEstadoDoValor);

            return bErroDeVaidacao;
        },

        _validarInputCnpj: function(cnpj){
            let sEstadoDoValor = VALOR_PADRAO;
            let bErroDeVaidacao = false;

            let sInputSemMascara = cnpj.replace(/\D/g, '');
            
            if(sInputSemMascara !== 14){
                sEstadoDoValor = VALOR_DE_ERRO;
                bErroDeVaidacao = true;
            }

            this.getView().byId(ID_INPUT_CNPJ).setValueState(sEstadoDoValor);

            return bErroDeVaidacao;
        },

        _validarInputNome: function(nome){
            let sEstadoDoValor = VALOR_PADRAO;
            let bErroDeVaidacao = false;
            
            if(nome === ""){
                sEstadoDoValor = VALOR_DE_ERRO;
                bErroDeVaidacao = true;
            }

            this.getView().byId(ID_INPUT_NOME).setValueState(sEstadoDoValor);

            return bErroDeVaidacao;
        },

        aoDigitarNoInpuntCpf: function() {
            this._exibirEspera(() => {
                let cpf = this.obterModelo(NOME_dO_MODELO_DOS_INPUTS).getProperty(PROPRIEDADE_CPF)
                this._validarInputCpf(cpf);
            });
		},

        aoDigitarNoInpuntCnpj: function() {
            this._exibirEspera(() => {
                let cnpj = this.obterModelo(NOME_dO_MODELO_DOS_INPUTS).getProperty(PROPRIEDADE_CNPJ)
                this._validarInputCnpj(cnpj);
            });
		},

        aoDigitarNoInpuntNome: function() {
            this._exibirEspera(() => {
                let nome = this.obterModelo(NOME_dO_MODELO_DOS_INPUTS).getProperty(PROPRIEDADE_NOME)
                this._validarInputNome(nome);
            });
		},

        aoClicarEmSalvar: function(){
            this._exibirEspera(async () => {
                let modeloDosInputs = this.obterModelo(NOME_dO_MODELO_DOS_INPUTS);

                debugger
                const nome = modeloDosInputs.getProperty(PROPRIEDADE_NOME)
                const cpf = modeloDosInputs.getProperty(PROPRIEDADE_CPF)
                const cnpj = modeloDosInputs.getProperty(PROPRIEDADE_CNPJ)
                let oComboBox = this.getView().byId(ID_COMBO_BOX);
                const tipoPessoa = modeloDosInputs.getProperty(PROPRIEDADE_TIPO_DE_CLIENTE);

				let bErroDeVaidacao = false;
                if(oComboBox.getSelectedKey() === KEY_PESSOA_FISICA){
                    bErroDeVaidacao = this._validarInputCpf(cpf)
                }
                if(oComboBox.getSelectedKey() === KEY_PESSOA_JURIDICA){
                    bErroDeVaidacao = this._validarInputCnpj(cnpj)
                }
                bErroDeVaidacao = this._validarInputNome(nome)
                
                if (bErroDeVaidacao) {
                    MessageBox.alert(this.obterTextoI18n(MSG_DE_ERRO_DE_VALIDACAO));
                    return;
                } 

                let cliente = {
                    nome: nome,
                    cpf: cpf.replace(/\D/g, ''),
                    cnpj: cnpj.replace(/\D/g, ''),
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