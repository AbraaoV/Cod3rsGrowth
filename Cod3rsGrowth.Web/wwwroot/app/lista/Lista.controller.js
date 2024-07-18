sap.ui.define([
   "sap/ui/core/mvc/Controller",
   'sap/ui/model/json/JSONModel',
   "../model/formatter",
   "sap/ui/model/Filter",
	"sap/ui/model/FilterOperator",
   "sap/m/MessageToast"
], function (Controller, JSONModel, formatter, MessageToast) {
   "use strict";
   let _filtroTipo = null;
   let _filtroNome = "";
   const NOME_DO_MODELO_DA_LISTA = "listaDeClientes";
   const CAMINHO_PARA_API = "/api/Cliente?";
   const CAMPO_PESSOA_FISICA = "fisica";
   const CAMPO_PESSOA_JURIDICA = "juridica";
   const VALOR_FILTRO_PESSOA_FISICA = 1;
   const VALOR_FILTRO_PESSOA_JURIDICA = 2;
   const FRAGMENTO_FILTRO = "ui5.codersgrowth.app.lista.Filtro"
   const PARAMETRO_DA_PAGINA_DE_ITENS_DO_FILTRO = "filterItems"
   var oParams = {}
   
   return Controller.extend("ui5.codersgrowth.app.lista.Lista", {
      formatter: formatter,
      onInit: async function() {
         const oRota = this.getOwnerComponent().getRouter();
         oRota.getRoute("lista").attachPatternMatched(this.prencherLista, this);
      },
      filtrarPelaRota: function(){
         const oRota = this.getOwnerComponent().getRouter();
         const oMudarHash = oRota.getHashChanger().getHash();
         oParams = new URLSearchParams(oMudarHash);

         _filtroNome = oParams.get("nome");
         _filtroTipo = oParams.has("tipo") ? parseInt(oParams.get("tipo")) : null;

         const prencherCampoPequisa = this.byId("filtroPesquisa").setValue(_filtroNome);
      },
      prencherLista: async function(){

         this.filtrarPelaRota();

         try{
            let url = CAMINHO_PARA_API + oParams;

            const response = await fetch(url, {
               method: "GET",
               headers: {
                  "Content-Type": "application/json",
               },
            });

            if (response.ok) {
               const data = await response.json();

               const oModel = new JSONModel(data);
               this.getView().setModel(oModel, NOME_DO_MODELO_DA_LISTA);
            }
         } catch(error) {
            MessageToast.show(error);        
         }
      },

      aoClicarEmFiltro: async function(){
         this.oDialog ??= await this.loadFragment({
            name: FRAGMENTO_FILTRO,
            controller: this
         });

         if (_filtroTipo === VALOR_FILTRO_PESSOA_FISICA) {
            this.byId("pessoaFisica").setSelected(true);
            this.byId("pessoaJuridica").setSelected(false);
         } else if (_filtroTipo === VALOR_FILTRO_PESSOA_JURIDICA) {
            this.byId("pessoaFisica").setSelected(false);
            this.byId("pessoaJuridica").setSelected(true);
         }

         this.oDialog.open();
      },

      aoClicarEmConfirmarNoFiltro: async function(oEvent){
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
         })
         this.adicionarParametros();
      },

      aoClicarEmLimparFiltro: function(){
         _filtroTipo = null;
         this.filtrar();
      },

      aoFiltrarNome: async function(oEvent){
         let sNome = oEvent.getSource().getValue();
         _filtroNome = sNome;

         this.adicionarParametros();
      },

      adicionarParametros: function(){
         const oRota = this.getOwnerComponent().getRouter();
         let querry = {};
         if (_filtroNome) {
            querry.nome = _filtroNome;
         }
         if (_filtroTipo !== null) {
            querry.tipo = _filtroTipo;
         }
         oRota.navTo("lista", {"?queryFiltro": querry});

         this.prencherLista();
      },
   });
});