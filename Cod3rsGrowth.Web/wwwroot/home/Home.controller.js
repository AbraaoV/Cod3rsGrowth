sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/m/MessageToast"
 ], function (Controller, MessageToast) {
    "use strict";
 
    return Controller.extend("ui5.codersgrowth.home.Home", {
        noMostrarAqui() {
            const oBundle = this.getView().getModel("i18n").getResourceBundle();
            const sMsg = oBundle.getText("aquiMsg")

            MessageToast.show(sMsg);
       }
    });
 
 });