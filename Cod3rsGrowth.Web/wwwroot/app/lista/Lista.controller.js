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
   return Controller.extend("ui5.codersgrowth.app.lista.Lista", {
      formatter: formatter,
      onInit: async function() {
         const oRota = this.getOwnerComponent().getRouter();
         oRota.getRoute("lista").attachPatternMatched(this.prencherLista, this);
      },
      prencherLista: async function(){
         try{
            const response = await fetch("/api/Cliente", {
               method: "GET",
               headers: {
                  "Content-Type": "application/json",
               },
               });
               if (response.ok) {
               const data = await response.json();

            const oModel = new JSONModel(data);
            this.getView().setModel(oModel, "listaDeCliente");
               }
            }catch(error){
               MessageToast.show(error);        
            }        
      },
      aoClicarEmFiltro: async function(){
         this.oDialog ??= await this.loadFragment({
            name: "ui5.codersgrowth.app.lista.Filtro",
            controller: this
         });
         this.oDialog.open();
      },

      aoClicarEmConfirmarNoFiltro: async function(oEvent){
         let aFiltros = oEvent.getParameter("filterItems");
         aFiltros.forEach(function (oItem) {
				switch (oItem.getKey()) {
					case "fisica":
						_filtroTipo = 1;
						break;
					case "juridica":
						_filtroTipo = 2;
						break;
					default:
					break;
				}
         })
         this.filtrar();
      },
      aoClicarEmLimparFiltro: function(){
         _filtroTipo = null;
         this.filtrar();
      },
      aoFiltrarNome: async function(oEvent){
         let sNome = oEvent.getSource().getValue();
         _filtroNome = sNome;
         this.filtrar();
      },

      filtrar: async function () {
         let oParams = {
            nome: _filtroNome, 
            tipo: _filtroTipo,
         };
         if(_filtroTipo === null){
            delete oParams.tipo;
         };
         try {
            const response = await fetch("/api/Cliente?" + new URLSearchParams(oParams), {
               method: "GET",
               headers: {
                  "Content-Type": "application/json",
               },
            });
      
            if (response.ok) {
               const data = await response.json();
      
               const oModel = new JSONModel(data);
               this.getView().setModel(oModel,"listaDeCliente");
      
            } else {
               MessageToast.show("Falha ao filtrar:", response.status);
            }
         } catch (error) {
            MessageToast.show(error); 
         }
      },
      
   });
});