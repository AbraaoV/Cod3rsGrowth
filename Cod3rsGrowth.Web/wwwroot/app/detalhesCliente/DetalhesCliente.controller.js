sap.ui.define([
	"sap/ui/model/json/JSONModel",
	"ui5/codersgrowth/common/ControllerBase",
], function (JSONModel, ControllerBase) {
	"use strict";

	return ControllerBase.extend("ui5.codersgrowth.app.detalhesCliente.DetalhesCliente", {
		onInit: function () {
			this.getRota().getRoute("detalhesCliente").attachPatternMatched(this._aoCarregar, this);
		},

        aoFecharDetalhes: function () {
            debugger
			this.getModelo("appView").setProperty("/actionButtonsInfo/midColumn/fullScreen", false);
			this.getRota().navTo("lista");
		},

        aoClicarEmTelaCheia: function () {
            this.getModelo("appView").setProperty("/layout", "MidColumnFullScreen");
			
		},
		aoClicarEmFecharTelaCheia: function () {
			this.getModelo("appView").setProperty("/layout", "TwoColumnsMidExpanded");
		},

		_aoCarregar: function () {
			this.getModelo("appView").setProperty("/layout", "TwoColumnsMidExpanded");
		}
	});
});
