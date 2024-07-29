sap.ui.define([
   "ui5/codersgrowth/common/ControllerBase",
   "ui5/codersgrowth/common/ConstantesLayoutDoApp"
], function (ControllerBase, ConstantesLayoutDoApp) {
   "use strict";
   const NOME_TARGET_NOT_FOUND = "notFound"

   return ControllerBase.extend("ui5.codersgrowth.app.notFound.NotFound", {
      onInit: function () {
         this.obterRota().getTarget(NOME_TARGET_NOT_FOUND).attachDisplay(this._onNotFoundDisplayed, this);
      },

      _onNotFoundDisplayed : function () {
         this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA)
		}

   });
});