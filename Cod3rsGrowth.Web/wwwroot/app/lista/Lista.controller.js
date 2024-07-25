sap.ui.define([
   "ui5/codersgrowth/common/ControllerBase",
   "../model/formatter",
], function (ControllerBase, formatter) {
   "use strict";
   let _filtroTipo = null;
   let _filtroNome = "";
   let oParams = {};
   let urlFinal

   const NOME_DO_MODELO_DA_LISTA = "listaDeClientes";
   const CAMINHO_PARA_API = "/api/Cliente?";
   const CAMPO_PESSOA_FISICA = "fisica";
   const CAMPO_PESSOA_JURIDICA = "juridica";
   const VALOR_FILTRO_PESSOA_FISICA = 1;
   const VALOR_FILTRO_PESSOA_JURIDICA = 2;
   const ID_FILTRO_PESSOA_FISICA = "pessoaFisica";
   const ID_FILTRO_PESSOA_JURIDICA = "pessoaJuridica";
   const FRAGMENTO_FILTRO = "ui5.codersgrowth.app.lista.Filtro";
   const PARAMETRO_DA_PAGINA_DE_ITENS_DO_FILTRO = "filterItems";
   const PARAMETRO_FILTRO_NOME = "nome";
   const PARAMETRO_FILTRO_TIPO = "tipo";
   const NOME_DA_ROTA = "lista";
   const ID_FILTRO_DE_PESQUISA = "filtroPesquisa";
   const ROTA_ADICIONAR_CLIENTE = "adicionarCliente";
   
   return ControllerBase.extend("ui5.codersgrowth.app.lista.Lista", {
      formatter: formatter,
      onInit: async function() {
         this.getRota().getRoute(NOME_DA_ROTA).attachPatternMatched(this._prencherLista, this);
      },

      _filtrarPelaRota: function(){
         const oHash = this.getRota().getHashChanger().getHash();
         oParams = new URLSearchParams(oHash);

         _filtroNome = oParams.get(PARAMETRO_FILTRO_NOME);
         _filtroTipo = oParams.has(PARAMETRO_FILTRO_TIPO) ? parseInt(oParams.get(PARAMETRO_FILTRO_TIPO)) : null;
         urlFinal = CAMINHO_PARA_API + oParams;

         const prencherCampoPequisa = this.byId(ID_FILTRO_DE_PESQUISA).setValue(_filtroNome);
      },

      _prencherLista: async function(){
         this._filtrarPelaRota();
         this.getModelo("appView").setProperty("/layout", "OneColumn");
         this._get(urlFinal, NOME_DO_MODELO_DA_LISTA);
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
            this.getModelo("appView").setProperty("/layout", "TwoColumnsMidExpanded");
            this.getRota().navTo("detalhesCliente", {
               clienteId: oElement.getSource().getBindingContext(NOME_DO_MODELO_DA_LISTA).getProperty("id")
            });
         });
		},

      aoClicarEmAdicionar: function(){
         this._exibirEspera(() => {
            this.getModelo("appView").setProperty("/layout", "OneColumn");
            this.getRota().navTo(ROTA_ADICIONAR_CLIENTE);
         });   
      },

      _adicionarParametros: function(){
         let querry = {};
         if (_filtroNome) {
            querry.nome = _filtroNome;
         }
         if (_filtroTipo !== null) {
            querry.tipo = _filtroTipo;
         }
         this.getRota().navTo(NOME_DA_ROTA, {"?queryFiltro": querry});
      },

   });
});