sap.ui.define([
	"../model/formatter",
	"ui5/codersgrowth/common/ControllerBase",
	"ui5/codersgrowth/common/ConstantesDoBanco",
	"ui5/codersgrowth/common/ConstantesLayoutDoApp",
	"ui5/codersgrowth/common/ConstantesDaRota",
	"ui5/codersgrowth/common/HttpRequest",
    "ui5/codersgrowth/common/ConstatesDasRequests",
	'sap/ui/model/json/JSONModel',
	"sap/m/MessageBox",
], function (formatter, ControllerBase, ConstantesDoBanco, ConstantesLayoutDoApp, ConstantesDaRota, HttpRequest, ConstatesDasRequests, JSONModel, MessageBox){
	"use strict";

	let _filtroFormaPagamento = "todos"
	let _filtroValorMin
	let _filtroValorMax
	let _filtroData

	const CAMINHO_PARA_API_ENUM_PAGAMENTO = "/api/EnumFormaPagamento"
	const ID_BOTAO_TELA_CHEIA = "botaoTelaCheia"
	const ID_BOTAO_SAIR_TELA_CHEIA = "botaoSairTelaCheia"
	const NOME_DO_MODELO_DO_CLIENTE = "clienteSelecionado"
	const NOME_DO_MODELO_DO_APP = "appView"
	const ID_DO_CLIENTE_NA_ROTA = 1
	const PROPRIEDADE_ID_DO_CLIENTE = "/id"
	const MSG_DE_SUCESSO_AO_DELETAR_I18N = "sucessOnCustumerDelete"
	const MSG_DE_AVISO_AO_DELETAR_I18N = "warningMessageWhenDeleteCustomer"
	const OPCAO_SIM = "Sim"
	const OPCAO_NAO = "Não"
	const ID_DA_COMBOX_FORMA_PAGAMENTO = "comboBoxFormaDePagamento"
	const LOCAL_DA_MOEDA = 'pt-BR'
	const MOEDA_CORRENTE = 'BRL'
	const VALOR_PADRAO_VAZIO = "00"
	const CHAVE_PADRAO_DA_COMBOBOX = "todos"
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
	const ID_DATAPICKER_ADCIONAR = "dataDoPedido"
	const ID_COMBOBOX_PAGAMENTO_ADICIONAR = "formaDePagamento"
	const ID_INPUT_NUMERO_CARTAO = "numeroDoCartao"
	const ID_INPUT_VALOR = "valorDoPedido"
	
	return ControllerBase.extend("ui5.codersgrowth.app.cliente.DetalhesCliente", {
		formatter: formatter,
		onInit: function () {
			this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_DETALHE).attachPatternMatched(this._aoCoincidirRota, this);
		},
		
		_aoCoincidirRota: async function () {
			this._exibirEspera(async () => {
				this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_DUAS_COLUNAS_DIVIDAS)
				let retorno = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[ID_DO_CLIENTE_NA_ROTA])
				this._modelo(new JSONModel(retorno), NOME_DO_MODELO_DO_CLIENTE);
				this._popularComboBox();
				this._filtrarPelaRota();
			})
		},

        aoFecharDetalhes: function () {
			this._exibirEspera(() => {
				this.obterModelo(NOME_DO_MODELO_DO_APP).setProperty(ConstantesLayoutDoApp.LAYOUT_SAIR_TELA_CHEIA, false);
				this.obterRota().navTo(ConstantesDaRota.NOME_DA_ROTA_DA_LISTA_CLIENTE);
			});
		},

        aoClicarEmTelaCheia: function () {
			this._exibirEspera(() => {
				this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_MEIO_TELA_CHEIA)
				this.getView().byId(ID_BOTAO_SAIR_TELA_CHEIA).setVisible(true);
				this.getView().byId(ID_BOTAO_TELA_CHEIA ).setVisible(false);
			});	
		},
		aoClicarEmFecharTelaCheia: function () {
			this._exibirEspera(() => {
				this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_DUAS_COLUNAS_DIVIDAS)
				this.getView().byId(ID_BOTAO_SAIR_TELA_CHEIA).setVisible(false);
				this.getView().byId(ID_BOTAO_TELA_CHEIA).setVisible(true);
			});	
		},

		aoClicarEmEditar: function(){
            this._exibirEspera(() => {
                this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
                this.obterRota().navTo(ConstantesDaRota.NOME_DA_ROTA_DE_EDITAR_CLIENTE, {
                    clienteId: this.obterModelo(NOME_DO_MODELO_DO_CLIENTE).getProperty(PROPRIEDADE_ID_DO_CLIENTE)
                });
            });
        },

		aoClicarEmDeletar: function(){ 
			MessageBox.warning(this.obterTextoI18n(MSG_DE_AVISO_AO_DELETAR_I18N), {
				actions: [ OPCAO_SIM,  OPCAO_NAO],
				onClose: async (sAction) => {
					if (sAction ===  OPCAO_SIM) {
						this._exibirEspera(async () => {
							await HttpRequest._request(ConstatesDasRequests.REQUISICAO_DELETE, ConstantesDoBanco.CAMINHO_PARA_API + "/" + this.obterParametros()[ID_DO_CLIENTE_NA_ROTA])
							this._sucessoNaRequicao(this.obterTextoI18n(MSG_DE_SUCESSO_AO_DELETAR_I18N))
						});
					} 
				}
			})
		},

		aoDigitarValorMin: function(oEvent){
			this._exibirEspera(() => {
				this._mascararMoeda(oEvent);
				_filtroValorMin = this._removerMascaraDeMoeda(oEvent.getSource().getValue());
				this._filtrar()
			});
		},

		aoDigitarValorMax: function(oEvent){
			this._exibirEspera(() => {
				this._mascararMoeda(oEvent);
				_filtroValorMax = this._removerMascaraDeMoeda(oEvent.getSource().getValue());
				this._filtrar()
			});
		},

		aoFiltrarPelaData: function(oEvent){
			this._exibirEspera(() => {
				_filtroData = oEvent.getSource().getValue();
				this._filtrar()
			});
		},
		
		aoSelecionarFormaDePagamento: function(oEvent){
			this._exibirEspera(() => {
				let controleComboBox = this.getView().byId(ID_DA_COMBOX_FORMA_PAGAMENTO);
				_filtroFormaPagamento = controleComboBox.getSelectedKey();
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
			const valorSemMascara = valorFormatado.replace(/[^\d]/g, "");
			
			let valorDecimal;
			if (valorSemMascara.length > 2) {
				valorDecimal = valorSemMascara.slice(0, -2) + "." + valorSemMascara.slice(-2);
			} else {
				valorDecimal = "0." + valorSemMascara.padStart(2, '0');
			}
			return parseFloat(valorDecimal);
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
			this._modelo(new JSONModel(retorno), NOME_DO_MODELO_DA_COMBO_BOX)
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
				this._modelo(new JSONModel(retornoPedidos), NOME_DO_MODELO_DA_LISTA_DE_PEDIDOS);
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
			this._exibirEspera( async () => {
				this.oDialog ??= await this.loadFragment({
				   name: DIALOGO_ADICIONAR_E_EDITAR,
				   controller: this
				});
	
				this.oDialog.open();
			 });
		},

		aoClicarEmAdicionarPedido: function(){
			this._exibirEspera(async () => {
				debugger
				const data = this.getView().byId(ID_DATAPICKER_ADCIONAR).getValue()
				const formaPagamento = this.getView().byId(ID_COMBOBOX_PAGAMENTO_ADICIONAR).getSelectedKey()
				const numeroCartao = this.getView().byId(ID_INPUT_NUMERO_CARTAO).getValue()
				const valor = this._removerMascaraDeMoeda(this.getView().byId(ID_INPUT_VALOR).getValue())

                let pedido = {
                    data: data,
                    formaPagamento: formaPagamento,
					numeroCartao: numeroCartao,
                    valor: valor,
                };

				await this._adicionarPedido(pedido);
            });    
        },

        _adicionarPedido: async function(pedido){
            await HttpRequest._request(ConstatesDasRequests.REQUISICAO_POST, ConstantesDoBanco.CAMINHO_PARA_API_PEDIDO, pedido);
            this._sucessoNaRequicao("teste")
        },

		aoDigitarValor: function(oEvent){
			this._mascararMoeda(oEvent);
		},

	});
});
