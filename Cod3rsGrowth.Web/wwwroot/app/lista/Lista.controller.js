sap.ui.define([
   "sap/ui/core/mvc/Controller",
   'sap/ui/model/json/JSONModel',
   "../model/formatter",
   "sap/ui/model/Filter",
	"sap/ui/model/FilterOperator"
], function (Controller, JSONModel, formatter, Filter, FilterOperator) {
   "use strict";

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
      aoBuscar: function (oEvent) {
			var aFilters = [];
			var sQuery = oEvent.getSource().getValue();
			if (sQuery && sQuery.length > 0) {
				var filter = new Filter("nome", FilterOperator.Contains, sQuery);
				aFilters.push(filter);
			}

			var oList = this.byId("listaCliente");
			var oBinding = oList.getBinding("items");
			oBinding.filter(aFilters, "Application");
		},
   });
});