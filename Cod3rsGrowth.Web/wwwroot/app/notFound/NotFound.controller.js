sap.ui.define([
   "ui5/codersgrowth/common/ControllerBase",
], function (ControllerBase) {
   "use strict";
   return ControllerBase.extend("ui5.codersgrowth.app.notFound.NotFound", {
      onInit: function () {
         this.getRota().getTarget("notFound").attachDisplay(this._onNotFoundDisplayed, this);
      },

      _onNotFoundDisplayed : function () {
         this.getModelo("appView").setProperty("/layout", "OneColumn");
		}

   });
});