sap.ui.define([
   "sap/ui/core/mvc/Controller",
   'sap/ui/model/json/JSONModel'
], function (Controller, JSONModel) {
   "use strict";

   return Controller.extend("ui5.codersgrowth.app.lista.Lista", {
      onInit: async function() {

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

      async onBemVindo() {
         this.oDialogo ??= await this.loadFragment({
               name: "ui5.anime.app.BemVindoDialogo"
         });
         this.oDialogo.open();
      },
      onFecharDialogo(){
         this.byId("bemVindoDialogo").close();
      }
   });

});