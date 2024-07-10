sap.ui.define([
    "sap/ui/core/UIComponent",
     "sap/ui/model/json/JSONModel"
 ], (UIComponent, JSONModel) => {
    "use strict";
 
    return UIComponent.extend("ui5.codersgrowth.Component", {
        metadata : {
            interfaces: ["sap.ui.core.IAsyncContentCreation"],
            manifest: "json"
         },
 
         init: function () {
          // call the init function of the parent
          UIComponent.prototype.init.apply(this, arguments);

          // create the views based on the url/hash
          this.getRouter().initialize();
      }
    });
 });
 