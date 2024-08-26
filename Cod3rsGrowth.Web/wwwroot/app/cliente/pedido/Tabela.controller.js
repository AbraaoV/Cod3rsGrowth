sap.ui.define([
    "sap/ui/core/Messaging",
    "ui5/codersgrowth/app/model/formatter",
    "ui5/codersgrowth/common/ControllerBase",
    "ui5/codersgrowth/common/ConstantesDoBanco",
    "ui5/codersgrowth/common/ConstantesDaRota",
    "ui5/codersgrowth/common/HttpRequest",
    "ui5/codersgrowth/common/ConstatesDasRequests",
    "sap/ui/model/json/JSONModel",
    "sap/m/MessageBox"
], function (Messaging, formatter, ControllerBase, ConstantesDoBanco, ConstantesDaRota, HttpRequest, ConstatesDasRequests, JSONModel, MessageBox){
    "use strict";

    let _filtroFormaPagamento = "todos"
    let _filtroValorMin
    let _filtroValorMax
    let _filtroData

    const CHAVE_ITEM_CARTAO_COMBOX = "1"
    const MSG_DE_ERRO_DE_VALIDACAO = "validationErrorMenssage"
    const CAMINHO_PARA_API_ENUM_PAGAMENTO = "/api/EnumFormaPagamento"
    const ID_DO_CLIENTE_NA_ROTA = 1
    const ID_DA_COMBOX_FORMA_PAGAMENTO = "comboBoxFormaDePagamento"
    const LOCAL_DA_MOEDA = 'pt-BR'
    const MOEDA_CORRENTE = 'BRL'
    const VALOR_PADRAO_VAZIO = "00"
    const CHAVE_PADRAO_DA_COMBOBOX = "4"
    const FILTRO_DE_PEDIDOS_DO_CLIENTE = 'clienteId'
    const CAMPO_PADRAO_COMBOX = "Todos pagamentos"
    const NOME_DO_MODELO_DA_COMBO_BOX = "formasDePagamento"
    const NOME_DO_MODELO_DA_MOEDA = "modeloMoeda"
    const NOME_DO_MODELO_DA_LISTA_DE_PEDIDOS = "listaDePedidos"
    const PARAMETRO_FILTRO_PAGAMENTO = "formaPagamento"
    const PARAMETRO_FILTRO_VALOR_MIN = "valorMin"
    const PARAMETRO_FILTRO_VALOR_MAX = "valorMax"
    const PARAMETRO_FILTRO_DATA = "dataPedido"
    const ID_INPUT_VALOR_MIN = "inputValorMin"
    const ID_INPUT_VALOR_MAX = "inputValorMax"
    const ID_DATAPICKER = "filtroDataPicker"
    const DIALOGO_ADICIONAR_E_EDITAR = "ui5.codersgrowth.app.cliente.pedido.AdicionarEditarPedido"
    const ID_DATAPICKER_ADCIONAR = "datapickerAdicionarPedido"
    const ID_COMBOBOX_PAGAMENTO_ADICIONAR = "comboxAdicionarPagamento"
    const ID_INPUT_NUMERO_CARTAO = "inputNumeroDoCartao"
    const ID_INPUT_VALOR = "inputValorDoPedido"
    const MSG_DE_SUCESSO_AO_ADICIONAR_I18N = "successMessageAddingOrder"
    const VALOR_PADRAO = "None"
    const VALOR_DE_ERRO = "Error"
	const PROPRIEDADE_DATA = "/data"
	const PROPRIEDADE_VALOR = "/valor"
	const PROPRIEDADE_VALOR_MAX = "/valorMax"
	const PROPRIEDADE_VALOR_MIN = "/valorMin"
	const PROPRIEDADE_FORMA_DE_PAGAMENTO = "/formaPagamento"
	const PROPRIEDADE_CARTAO = "/numeroCartao"
	const NOME_DO_MODELO_DO_PEDIDO = "pedido"
	const NOME_DO_MODELO_DOS_FILTROS = "filtro"
    
    return ControllerBase.extend("ui5.codersgrowth.app.cliente.DetalhesCliente", {
        formatter: formatter,
        onInit: function () {
            this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_DETALHE).attachPatternMatched(this._aoCoincidirRota, this);
        },
        
        _modeloControleDeTela: function(modelo){
            const nomeDoModelo = "controleDeTela"
            return this._modelo(nomeDoModelo, modelo)
        },

        _aoCoincidirRota: async function () {
            this._exibirEspera(async () => {
				this._registarModeloDosFiltros();
                this._popularComboBox();
                this._filtrarPelaRota();
                this._modeloControleDeTela(new JSONModel({
                    controleVisibilidadeCartao: true
                }))
            })
        },

		_registarModeloDosFiltros: function(){
			let filtro = {
				data: "",
				valorMin: "",
				valorMaX: "",
				formaPagamento: 1
			}

			this._modelo(NOME_DO_MODELO_DOS_FILTROS, new JSONModel(filtro))
		},

        aoDigitarValorMin: function(oEvent){
			this._exibirEspera(() => {
				this._mascararMoeda(oEvent);
				_filtroValorMin = this._removerMascaraDeMoeda(this._modelo(NOME_DO_MODELO_DOS_FILTROS).getProperty(PROPRIEDADE_VALOR_MIN));
				this._filtrar()
			});
		},

        aoDigitarValorMax: function(oEvent){
            this._exibirEspera(() => {
                this._mascararMoeda(oEvent);
                _filtroValorMax = this._removerMascaraDeMoeda(this._modelo(NOME_DO_MODELO_DOS_FILTROS).getProperty(PROPRIEDADE_VALOR_MAX));
                this._filtrar()
            });
        },

        aoFiltrarPelaData: function(){
            this._exibirEspera(() => {
                _filtroData = this._modelo(NOME_DO_MODELO_DOS_FILTROS).getProperty(PROPRIEDADE_DATA)
                this._filtrar()
            });
        },
        
        aoSelecionarFormaDePagamento: function(){
            this._exibirEspera(() => {
				_filtroFormaPagamento = this._modelo(NOME_DO_MODELO_DOS_FILTROS).getProperty(PROPRIEDADE_FORMA_DE_PAGAMENTO)
                this._filtrar()
            });
        },

        _mascararMoeda: function(oEvent) {
            const input = oEvent.getSource();
            let valorDoInput = oEvent.getSource().getValue();
            const valorSemMascara = valorDoInput.replace(/[^\d]/g, "");
            
            
            let valorFormatado;
            if (valorSemMascara.length > 2) {
                valorFormatado = valorSemMascara.slice(0, -2) + "." + valorSemMascara.slice(-2);
            } else {
                valorFormatado = "0." + valorSemMascara.padStart(2, '0');
            }

            const numberValue = parseFloat(valorFormatado);
            if (!isNaN(numberValue)) {
                input.setValue(this._moedaDaMascara(numberValue));
            } else {
                input.setValue(""); 
            }
            this._limparMascaraDeMoedaAoLimparCampo(valorSemMascara, input);
        },

        _moedaDaMascara: function(valor, locale = LOCAL_DA_MOEDA, currency = MOEDA_CORRENTE) {
            return new Intl.NumberFormat(locale, {
                style: 'currency',
                currency
            }).format(valor);
        },

        _removerMascaraDeMoeda: function(valorFormatado) {
			if(valorFormatado !== null){
				const valorSemMascara = valorFormatado.replace(/[^\d]/g, "");
            
            let valorDecimal;
            if (valorSemMascara.length > 2) {
                valorDecimal = valorSemMascara.slice(0, -2) + "." + valorSemMascara.slice(-2);
            } else {
                valorDecimal = "0." + valorSemMascara.padStart(2, '0');
            }
            return parseFloat(valorDecimal);
			}
        },
        
        _limparMascaraDeMoedaAoLimparCampo: function(valor, input){
            if (valor === VALOR_PADRAO_VAZIO || valor === "") {
                input.setValue("");
            }
        },

        _preencherQueryFiltros: function () {
			let query = { clienteId: this.obterParametros()[ID_DO_CLIENTE_NA_ROTA] };
			if (_filtroFormaPagamento != CHAVE_PADRAO_DA_COMBOBOX) query.formaPagamento = _filtroFormaPagamento;
			if (_filtroValorMin) query.valorMin =  _filtroValorMin;
			if (_filtroValorMax) query.valorMax = _filtroValorMax;
			if (_filtroData) query.dataPedido = _filtroData;
			return query;
		},

        _filtrar: function(){
            this._exibirEspera(async () => {
                const urlParams = new URLSearchParams(this._preencherQueryFiltros());
                urlParams.delete(FILTRO_DE_PEDIDOS_DO_CLIENTE);
                
                let urlBase = window.location.origin + window.location.pathname
                let url = `${urlBase}${window.location.hash.split('/?')[0]}/?${urlParams.toString()}`;
                window.history.pushState({}, '', url);
                
                this._popularTabelaDePedidos();
            });   
        },

        _popularComboBox: async function() {
            let retorno = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, CAMINHO_PARA_API_ENUM_PAGAMENTO)
            let todos = {
                key: CHAVE_PADRAO_DA_COMBOBOX,
                descricao: CAMPO_PADRAO_COMBOX
            }
            retorno.push(todos);
            this._modelo(NOME_DO_MODELO_DA_COMBO_BOX, new JSONModel(retorno), )
        },
        
        _popularTabelaDePedidos: async function(){
            this._exibirEspera(async () => {
                const urlParams = new URLSearchParams(this._preencherQueryFiltros());
                let urlFinal = ConstantesDoBanco.CAMINHO_PARA_API_PEDIDO + "?" + urlParams;

                let retornoPedidos = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, urlFinal);
                let modeloMoeda = new JSONModel({
                    currency: MOEDA_CORRENTE
                });
                this.getView().setModel(modeloMoeda, NOME_DO_MODELO_DA_MOEDA);
                this._modelo(NOME_DO_MODELO_DA_LISTA_DE_PEDIDOS, new JSONModel(retornoPedidos));
            });   
        },

        _filtrarPelaRota: function(){
            const urlParams = new URLSearchParams(window.location.hash.split('?')[1]);
            _filtroFormaPagamento = urlParams.has(PARAMETRO_FILTRO_PAGAMENTO) ? (urlParams.get(PARAMETRO_FILTRO_PAGAMENTO)) : CHAVE_PADRAO_DA_COMBOBOX;
            _filtroValorMin = urlParams.get(PARAMETRO_FILTRO_VALOR_MIN);
            _filtroValorMax = urlParams.get(PARAMETRO_FILTRO_VALOR_MAX);
            _filtroData = urlParams.get(PARAMETRO_FILTRO_DATA);

            this._preencherFiltros();
            this._popularTabelaDePedidos();
        },

        _preencherFiltros: function(){
            this.getView().byId(ID_DA_COMBOX_FORMA_PAGAMENTO).setSelectedKey(_filtroFormaPagamento);
            this.getView().byId(ID_INPUT_VALOR_MIN).setValue(_filtroValorMin).fireLiveChange();
            this.getView().byId(ID_INPUT_VALOR_MAX).setValue(_filtroValorMax).fireLiveChange();;
            this.getView().byId(ID_DATAPICKER).setValue(_filtroData);
        },

        aoClicarEmAdicionar: function(){
            this._exibirEspera(async () => { 
                this.oDialog ??= await this.loadFragment({
                    name: DIALOGO_ADICIONAR_E_EDITAR,
                    controller: this
                });
                this._definirValoresPadroes();
                this.oDialog.open();
            });
        },

        aoAdicionarPedido: function(){
            this._exibirEspera(async () => {
				let pedido = this._modelo(NOME_DO_MODELO_DO_PEDIDO).getData();

                let bErroDeVaidacao = false;
                if(pedido.formaPagamento === CHAVE_ITEM_CARTAO_COMBOX){
                    bErroDeVaidacao = this._validarInputCartao(pedido.numeroCartao)
                };
                bErroDeVaidacao = this._validarInputData(pedido.data)
                bErroDeVaidacao = this._validarInputValor(pedido.valor)
                
                if (bErroDeVaidacao) {
                    MessageBox.alert(this.obterTextoI18n(MSG_DE_ERRO_DE_VALIDACAO));
                    return;
                } 

                pedido.formaPagamento = parseInt(pedido.formaPagamento),
                pedido.numeroCartao = pedido.numeroCartao.replace(/\D/g, ''),
                pedido.valor = this._removerMascaraDeMoeda(pedido.valor),
                pedido.clienteId = this.obterParametros()[ID_DO_CLIENTE_NA_ROTA]

                await this._adicionarPedido(pedido);
            });    
        },

        _removerItemTodosDaComboBox: function(){
            let controleComboBox = this.byId(ID_COMBOBOX_PAGAMENTO_ADICIONAR);
            let oItemToRemove = controleComboBox.getItems().find(item => item.getKey() === CHAVE_PADRAO_DA_COMBOBOX);
            if (oItemToRemove) {
                controleComboBox.removeItem(oItemToRemove);
            }
        },
        _adicionarPedido: async function(pedido){
            await HttpRequest._request(ConstatesDasRequests.REQUISICAO_POST, ConstantesDoBanco.CAMINHO_PARA_API_PEDIDO, pedido);
            MessageBox.success(this.obterTextoI18n(MSG_DE_SUCESSO_AO_ADICIONAR_I18N))
            this.oDialog.close();
        },

        aoDigitarNoInputValor: function(oEvent){
            this._mascararMoeda(oEvent);
            let valor = this._modelo(NOME_DO_MODELO_DO_PEDIDO).getProperty(PROPRIEDADE_VALOR)
            this._validarInputValor(valor)
        },

        aoDigitarNoInputCartao: function(oEvent){
			let numeroCartao = this._modelo(NOME_DO_MODELO_DO_PEDIDO).getProperty(PROPRIEDADE_CARTAO)
            this._validarInputCartao(numeroCartao);
        },

        _validarInputCartao: function(valor){
            let sEstadoDoValor = VALOR_PADRAO;
            let bErroDeVaidacao = false;
            let valorDoInput = valor.replace(/\D/g, '');

            if (valorDoInput.length !== 16) {
                sEstadoDoValor = VALOR_DE_ERRO;
                bErroDeVaidacao = true;
            }

            this.getView().byId(ID_INPUT_NUMERO_CARTAO).setValueState(sEstadoDoValor);
            return bErroDeVaidacao;
        },

        _validarInputValor: function(valor){
            let sEstadoDoValor = VALOR_PADRAO;
            let bErroDeVaidacao = false;

            if (valor === "") {
                sEstadoDoValor = VALOR_DE_ERRO;
                bErroDeVaidacao = true;
            }

            this.getView().byId(ID_INPUT_VALOR).setValueState(sEstadoDoValor);
            return bErroDeVaidacao;
        },

		_validarInputData: function(data){
            let sEstadoDoValor = VALOR_PADRAO;
            let bErroDeVaidacao = false;

            if (data === "") {
                sEstadoDoValor = VALOR_DE_ERRO;
                bErroDeVaidacao = true;
            }

            this.getView().byId(ID_DATAPICKER_ADCIONAR).setValueState(sEstadoDoValor);
            return bErroDeVaidacao;
        },

        aoSelecionarData: function(){
			let data = this._modelo(NOME_DO_MODELO_DO_PEDIDO).getProperty(PROPRIEDADE_DATA)
            this._validarInputData(data)
        },

        _registarModeloParaVailidacao: function(){
            let pedido = {
                data: "",
                numeroCartao: "",
                valor: "",
				formaPagamento: "1"
            };

            this._modelo(NOME_DO_MODELO_DO_PEDIDO, new JSONModel(pedido))

            let oView = this.getView(),
            oMM = Messaging;

            oMM.registerObject(oView.byId(ID_DATAPICKER_ADCIONAR), true);
            oMM.registerObject(oView.byId(ID_INPUT_VALOR), true);
            oMM.registerObject(oView.byId(ID_INPUT_NUMERO_CARTAO), true);
        },

        aoClicarEmCancelar: function(){
            this._limparCampos();
            this.oDialog.close();
        },

        _limparCampos: function() {
            let pedido = {
                data: "",
                formaPagamento: CHAVE_ITEM_CARTAO_COMBOX,
                numeroCartao: "",
                valor: ""
            }
			this._modelo(NOME_DO_MODELO_DO_PEDIDO, new JSONModel(pedido))

            this.getView().byId(ID_DATAPICKER_ADCIONAR).setValueState(undefined);
            this.getView().byId(ID_INPUT_VALOR).setValueState(undefined);
            this.getView().byId(ID_INPUT_NUMERO_CARTAO).setValueState(undefined);
        },

        aoSelecionarPagamentoDoPedido: function(){
            this._exibirEspera(() => {
                let controleDeTela = this._modeloControleDeTela();
                let chaveSelecionada = this._modelo(NOME_DO_MODELO_DO_PEDIDO).getProperty(PROPRIEDADE_FORMA_DE_PAGAMENTO)
                
                this._modelo(NOME_DO_MODELO_DO_PEDIDO).setProperty(PROPRIEDADE_CARTAO, "")
                controleDeTela.getData().controleVisibilidadeCartao = chaveSelecionada === CHAVE_ITEM_CARTAO_COMBOX
            
                controleDeTela.updateBindings();
            })
        },

        _definirValoresPadroes: function(){
            this._removerItemTodosDaComboBox();
            this._registarModeloParaVailidacao();
        },
    });
});