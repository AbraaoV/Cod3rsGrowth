sap.ui.define([
   "ui5/codersgrowth/common/ControllerBase",
   "ui5/codersgrowth/common/ConstantesDoBanco",
   "ui5/codersgrowth/common/ConstantesLayoutDoApp",
   "ui5/codersgrowth/common/ConstantesDaRota",
   "../model/formatter",
   "ui5/codersgrowth/common/HttpRequest",
   "ui5/codersgrowth/common/ConstatesDasRequests",
   "sap/ui/model/json/JSONModel"

], function (ControllerBase, ConstantesDoBanco, ConstantesLayoutDoApp, ConstantesDaRota, formatter, HttpRequest, ConstatesDasRequests, JSONModel) {
   "use strict";
   let _filtroTipo = null;
   let _filtroNome = "";
   let urlFinal

   const NOME_DO_MODELO_DA_LISTA = "listaDeClientes";
   const CAMPO_PESSOA_FISICA = "fisica";
   const CAMPO_PESSOA_JURIDICA = "juridica";
   const VALOR_FILTRO_PESSOA_FISICA = 1;
   const VALOR_FILTRO_PESSOA_JURIDICA = 2;
   const ID_FILTRO_PESSOA_FISICA = "pessoaFisica";
   const ID_FILTRO_PESSOA_JURIDICA = "pessoaJuridica";
   const FRAGMENTO_FILTRO = "ui5.codersgrowth.app.cliente.Filtro";
   const PARAMETRO_DA_PAGINA_DE_ITENS_DO_FILTRO = "filterItems";
   const PARAMETRO_FILTRO_NOME = "nome";
   const PARAMETRO_FILTRO_TIPO = "tipo";
   const ID_FILTRO_DE_PESQUISA = "filtroPesquisa";
   const PROPRIEDADE_ID_DO_CLIENTE_DA_LISTA = "id";
   
   return ControllerBase.extend("ui5.codersgrowth.app.cliente.Lista", {
      formatter: formatter,
      onInit: async function() {
         this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DA_LISTA_CLIENTE).attachPatternMatched(this._aoCoincidirRota, this);
         this.obterRota().getRoute(ConstantesDaRota.NOME_DA_ROTA_DE_DETALHE).attachPatternMatched(this._aoCoincidirRotaComFltro, this);
      },

      _aoCoincidirRotaComFltro: function(){
         this._exibirEspera(()=> this._filtrarPelaRota())
      },

      _filtrarPelaRota: function(){
         const urlParams = new URLSearchParams(window.location.search);

         _filtroNome = urlParams.get(PARAMETRO_FILTRO_NOME);
         _filtroTipo = urlParams.has(PARAMETRO_FILTRO_TIPO) ? parseInt(urlParams.get(PARAMETRO_FILTRO_TIPO)) : null;
         urlFinal = ConstantesDoBanco.CAMINHO_PARA_API + "?" + urlParams;
         this.byId(ID_FILTRO_DE_PESQUISA).setValue(_filtroNome);

         this._adicionarParametros();
      },

      _aoCoincidirRota: async function(){
         this._exibirEspera(async () =>{
            this._filtrarPelaRota();
            this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
            let retorno = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, urlFinal)
            this._modelo( NOME_DO_MODELO_DA_LISTA, new JSONModel(retorno));
         })
      },

      aoClicarEmFiltro: async function(){
         this._exibirEspera( async () => {
            this.oDialog ??= await this.loadFragment({
               name: FRAGMENTO_FILTRO,
               controller: this
            });

            this.byId(ID_FILTRO_PESSOA_FISICA).setSelected(_filtroTipo === VALOR_FILTRO_PESSOA_FISICA);
            this.byId(ID_FILTRO_PESSOA_JURIDICA).setSelected(_filtroTipo === VALOR_FILTRO_PESSOA_JURIDICA);

            this.oDialog.open();
         });
      },

      aoClicarEmConfirmarNoFiltro: async function(oEvent){
         this._exibirEspera(() => {
            let aFiltros = oEvent.getParameter(PARAMETRO_DA_PAGINA_DE_ITENS_DO_FILTRO);
            aFiltros.forEach(function (oItem) {
               switch (oItem.getKey()) {
                  case CAMPO_PESSOA_FISICA:
                     _filtroTipo = VALOR_FILTRO_PESSOA_FISICA;
                     break;
                  case CAMPO_PESSOA_JURIDICA:
                     _filtroTipo = VALOR_FILTRO_PESSOA_JURIDICA;
                     break;
                  default:
                  break;
               }
            });
         })
         this._adicionarParametros();
      },

      aoClicarEmLimparFiltro: function(){
         this._exibirEspera(() => {
            _filtroTipo = null;
         });   
      },

      aoFiltrarNome: async function(oEvent){
         this._exibirEspera(() => {
            let sNome = oEvent.getSource().getValue();
            _filtroNome = sNome;
         
            this._adicionarParametros();
         });
      },

      aoClicarEmDetalhe : function (oElement) {
         this._exibirEspera(() => {
            this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_DUAS_COLUNAS_DIVIDAS)
            this.obterRota().navTo(ConstantesDaRota.NOME_DA_ROTA_DE_DETALHE, {
               clienteId: oElement.getSource().getBindingContext(NOME_DO_MODELO_DA_LISTA).getProperty(PROPRIEDADE_ID_DO_CLIENTE_DA_LISTA)
            });
         });
		},

      aoClicarEmAdicionar: function(){
         this._exibirEspera(() => {
            this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
            this.obterRota().navTo(ConstantesDaRota.NOME_DA_ROTA_DE_ADICIONAR_CLIENTE);
         });   
      },

      _adicionarParametros: async function(){
         this._exibirEspera(async () => {
            let querry = {};
            if (_filtroNome) {
               querry.nome = _filtroNome;
            }
            if (_filtroTipo !== null) {
               querry.tipo = _filtroTipo;
            }
            const urlParams = new URLSearchParams(querry);
            const urlBase =  window.location.origin + window.location.pathname;
            
            let url = `${urlBase}?${urlParams.toString()}`;
            if(window.location.hash){
               url += window.location.hash;
            }
            window.history.pushState({}, '', url);
            urlFinal = ConstantesDoBanco.CAMINHO_PARA_API + "?" + urlParams;
            let retorno = await HttpRequest._request(ConstatesDasRequests.REQUISICAO_GET, urlFinal)
            this._modelo(NOME_DO_MODELO_DA_LISTA, new JSONModel(retorno));
         });   
      },

   });
});