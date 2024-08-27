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
    const MSG_DE_SUCESSO_NO_CADASTRO_I18N = "sucessCustumerRegister"
    const MSG_DE_SUCESSO_NA_EDICAO_I18N = "sucessCustumerEdit"
    const ID_INPUT_CPF = "inputCpf"
    const ID_INPUT_CNPJ = "inputCnpj"
    const ID_INPUT_NOME = "inputNome"
    const CHAVE_PESSOA_FISICA = "1"
    const CHAVE_PESSOA_JURIDICA = "2"
    const MSG_DE_ERRO_DE_VALIDACAO = "validationErrorMenssage"
    const VALOR_PADRAO = "None"
    const VALOR_DE_ERRO = "Error"
    const INDEX_DO_ID_DO_CLIENTE_NA_ROTA = 1
    const TITULO_DO_PANEL_CADASTRO = "addCustomerPanelTittle"
    const TITULO_DO_PANEL_EDITAR = "editCustomerPanelTittle"
    const PROPRIEDADE_NOME = "/nome"
    const PROPRIEDADE_CPF = "/cpf"
    const PROPRIEDADE_CNPJ = "/cnpj"
    const ROTA_EDITAR = "editar"
    const INDEX_DO_NOME_DA_ROTA = 2
    const PROPRIEDADE_TITULO = "tituloDoPanel"
    const PROPRIEDADE_TIPO = "/tipo"

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

        _modeloControleDeTela: function(modelo){
            const nomeDoModelo = "controleDeTela"
            return this._modelo(nomeDoModelo, modelo)
        },

        _modeloCliente: function(modelo){
            const nomeDoModelo = "cliente"
            return this._modelo(nomeDoModelo, modelo)
        },

        _modeloComboBox: function(modelo){
            const nomeDoModelo = "comboxTipoDePessoa"
            return this._modelo(nomeDoModelo, modelo)
        },

        _aoCoincidirRotaEditar: async function(){
            this._exibirEspera(async () =>{
                await this._definirValoresPadroes()
                let resultado = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[INDEX_DO_ID_DO_CLIENTE_NA_ROTA]);
                let modeloCombox = this._modeloComboBox().getData();
                let tipoEncontrado = modeloCombox.find(x => x.descricao === resultado.tipo);
                resultado.tipo = tipoEncontrado.key;
                this._modeloCliente(new JSONModel(resultado));
                this._definirTituloEdicao();
            })
        },

        _definirValoresPadroes: async function(){
            let retorno = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, CAMINHO_PARA_API_ENUM);
            this._modeloComboBox(new JSONModel(retorno))
            this._registarModeloParaVailidacao()
            this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
            this._modeloControleDeTela(new JSONModel({
                controleVisibilidadeCpf: true,
                controleVisibilidadeCnpj: false,
                tituloDoPanel: this.obterTextoI18n(TITULO_DO_PANEL_CADASTRO)
            }))
            this._definitTituloCadastro();
        },

        _definirTituloEdicao: function(){
            let titulo = this.obterTextoI18n(TITULO_DO_PANEL_EDITAR)
            this._modeloControleDeTela().setProperty(PROPRIEDADE_TITULO, titulo)
            this._modeloControleDeTela().updateBindings();
        },

        _definitTituloCadastro: function(){
            let titulo = this.obterTextoI18n(TITULO_DO_PANEL_CADASTRO)
            this. _modeloControleDeTela().setProperty(PROPRIEDADE_TITULO, titulo)
            this._modeloControleDeTela().updateBindings();
        },

        _registarModeloParaVailidacao: function(){
            let cliente = {
                nome: "",
                cpf: "",
                cnpj: "",
                tipo: CHAVE_PESSOA_FISICA
            }

            this._modeloCliente(new JSONModel(cliente))

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
            
            if(sInputSemMascara.length !== 11){
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
            
            if(sInputSemMascara.length !== 14){
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
                let cpf = this._modeloCliente().getProperty(PROPRIEDADE_CPF)
                this._validarInputCpf(cpf);
            });
        },

        aoDigitarNoInpuntCnpj: function() {
            this._exibirEspera(() => {
                let cnpj = this._modeloCliente().getProperty(PROPRIEDADE_CNPJ)
                this._validarInputCnpj(cnpj);
            });
        },

        aoDigitarNoInpuntNome: function() {
            this._exibirEspera(() => {
                let nome = this._modeloCliente().getProperty(PROPRIEDADE_NOME)
                this._validarInputNome(nome);
            });
        },

        aoSelecionarTipoPessoa: function(){
            this._exibirEspera(() => {
                let cliente = this._modeloCliente().getData()
                let controleDeTela = this._modeloControleDeTela();

                cliente.cpf = "";
                cliente.cnpj = "";
                controleDeTela.getData().controleVisibilidadeCpf = cliente.tipo === CHAVE_PESSOA_FISICA;
                controleDeTela.getData().controleVisibilidadeCnpj = cliente.tipo === CHAVE_PESSOA_JURIDICA;

                controleDeTela.updateBindings();
            })
        },

        aoClicarEmSalvar: function(){
            this._exibirEspera(async () => {
                let cliente = this._modeloCliente().getData()

                let bErroDeVaidacao = false;
                if(cliente.tipo === CHAVE_PESSOA_FISICA){
                    bErroDeVaidacao = this._validarInputCpf(cliente.cpf)
                }
                if(cliente.tipo === CHAVE_PESSOA_JURIDICA){
                    bErroDeVaidacao = this._validarInputCnpj(cliente.cnpj)
                }
                bErroDeVaidacao = this._validarInputNome(cliente.nome)
                
                if (bErroDeVaidacao) {
                    MessageBox.alert(this.obterTextoI18n(MSG_DE_ERRO_DE_VALIDACAO));
                    return;
                } 

                cliente.cpf = cliente.cpf.replace(/\D/g, ''),
                cliente.cnpj = cliente.cnpj.replace(/\D/g, ''),
                cliente.tipo = parseInt(cliente.tipo)

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
            let cliente = {
                nome: "",
                cpf: "",
                cnpj: "",
                tipo: CHAVE_PESSOA_FISICA
            }
            this._modeloCliente(new JSONModel(cliente))

            this.getView().byId(ID_INPUT_NOME).setValueState(undefined);
            this.getView().byId(ID_INPUT_CPF).setValueState(undefined);
            this.getView().byId(ID_INPUT_CNPJ).setValueState(undefined);
        },
    });
});