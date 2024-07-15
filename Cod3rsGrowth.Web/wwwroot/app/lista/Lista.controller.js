sap.ui.define([
   "sap/ui/core/mvc/Controller",
   'sap/ui/model/json/JSONModel',
   "../model/formatter",
   "sap/ui/model/Filter",
	"sap/ui/model/FilterOperator"
], function (Controller, JSONModel, formatter, Filter, FilterOperator) {
   "use strict";
   var _filtroTipo = null;
   var _filtroNome = "";
   return Controller.extend("ui5.codersgrowth.app.lista.Lista", {
      formatter: formatter,
      onInit: async function() {
         console.log("Controller inicializado.");
               try{
               const response = await fetch("https://localhost:7205/api/ControllerCliente", {
                  method: "GET",
                  headers: {
                     "Content-Type": "application/json",
                  },
                  });
                  if (response.ok) {
                  const data = await response.json();

               const oModel = new JSONModel(data);
               this.getView().setModel(oModel);
                  console.log("data", data);
                  }
               }catch(error){
                  console.log(error);        
               }                  
      },
      aoApertarFiltro: async function(){
         this.oDialog ??= await this.loadFragment({
            name: "ui5.codersgrowth.app.lista.Filtro",
            controller: this
         });
         this.oDialog.open();
      },

      aoConfirmar: async function(oEvent){
         var aFiltros = oEvent.getParameter("filterItems");
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
         this.aoBuscar(oEvent);
      },
      aoLimparFiltro: async function(oEvent){
         _filtroTipo = null;
         this.aoBuscar(oEvent);
      },
      aoFiltrarNome: async function(oEvent){
         var sNome = oEvent.getSource().getValue();
         _filtroNome = sNome;
         this.aoBuscar(oEvent);
      },

      aoBuscar: async function (oEvent) {
         var oParams = {
            nome: _filtroNome, 
            tipo: _filtroTipo,
         };
         if(_filtroTipo === null){
            var oParams = {
               nome: _filtroNome, 
            }
         };
         try {
            const response = await fetch("https://localhost:7205/api/ControllerCliente?" + new URLSearchParams(oParams), {
               method: "GET",
               headers: {
                  "Content-Type": "application/json",
               },
            });
      
            if (response.ok) {
               const data = await response.json();
      
               const oModel = new JSONModel(data);
               this.getView().setModel(oModel);
      
               console.log("Data depois do filtro", data);
            } else {
               console.error("Falha ao filtrar:", response.status);
            }
         } catch (error) {
            console.error("Erro ao filtrar:", error);
         }
      },
      
   });
});