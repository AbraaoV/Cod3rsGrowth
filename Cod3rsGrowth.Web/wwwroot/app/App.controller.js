sap.ui.define([
	"ui5/codersgrowth/common/ControllerBase",
	"sap/ui/model/json/JSONModel",
	"ui5/codersgrowth/common/ConstantesLayoutDoApp"
], (ControllerBase, JSONModel, ConstantesLayoutDoApp) => {
	"use strict";
	const NOME_MODELO_DO_APP = "appView"

return ControllerBase.extend("ui5.codersgrowth.app.App", {
	
	onInit : function () {
			var oViewModel,

			oViewModel = new JSONModel({
				busy : false,
				delay : 5000,
				layout : ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA,
				previousLayout : "",
				actionButtonsInfo : {
					midColumn : {
						fullScreen : false
					}
				}
			});
			this._modelo(oViewModel, NOME_MODELO_DO_APP);
		}
	});
});