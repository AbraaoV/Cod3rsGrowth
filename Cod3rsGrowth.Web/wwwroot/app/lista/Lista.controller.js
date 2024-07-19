sap.ui.define([
   "sap/ui/core/mvc/Controller",
   'sap/ui/model/json/JSONModel',
   "../model/formatter",
   "sap/m/MessageBox"
], function (Controller, JSONModel, formatter, MessageBox) {
   "use strict";
   let _filtroTipo = null;
   let _filtroNome = "";
   let oParams = {};

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
   const ID_LISTA_DE_CLIENTES = "listaClientes";
   const ID_FILTRO_DE_PESQUISA = "filtroPesquisa"
   const MSG_DE_ERRO = "Ocorreu um erro: "
   
   return Controller.extend("ui5.codersgrowth.app.lista.Lista", {
      formatter: formatter,
      onInit: async function() {
         const oRota = this.getOwnerComponent().getRouter();
         oRota.getRoute(NOME_DA_ROTA).attachPatternMatched(this._prencherLista, this);
      },

      _filtrarPelaRota: function(){
         const oRota = this.getOwnerComponent().getRouter();
         const oHash = oRota.getHashChanger().getHash();
         oParams = new URLSearchParams(oHash);

         _filtroNome = oParams.get(PARAMETRO_FILTRO_NOME);
         _filtroTipo = oParams.has(PARAMETRO_FILTRO_TIPO) ? parseInt(oParams.get(PARAMETRO_FILTRO_TIPO)) : null;

         const prencherCampoPequisa = this.byId(ID_FILTRO_DE_PESQUISA).setValue(_filtroNome);
      },
      _modeloLista: function(oModel){
         this.getView().setModel(oModel, NOME_DO_MODELO_DA_LISTA);
      },
      _get: async function(url){
         this._exibirEspera( async () => {
            let urlFinal = url + oParams;

            const response = await fetch(urlFinal, {
               method: "GET",
               headers: {
                  "Content-Type": "application/json",
               },
            });
            if (response.ok) {
            const data = await response.json();
            const oModel = new JSONModel(data);
   
            return this._modeloLista(oModel);
            }
         });
      },

      _prencherLista: async function(){

         this._filtrarPelaRota();

         this._get(CAMINHO_PARA_API);
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

      _adicionarParametros: function(){
         const oRota = this.getOwnerComponent().getRouter();
         let querry = {};
         if (_filtroNome) {
            querry.nome = _filtroNome;
         }
         if (_filtroTipo !== null) {
            querry.tipo = _filtroTipo;
         }
         oRota.navTo(NOME_DA_ROTA, {"?queryFiltro": querry});
      },

      _exibirEspera: function(funcao) {
         var oLista = this.byId(ID_LISTA_DE_CLIENTES);
         oLista.setBusy(true);
         
         try {
            funcao();
         } catch(error) {
            MessageBox.error(MSG_DE_ERRO + error.message);
         } finally {
            oLista.setBusy(false)
         }
      },
      
   });
});