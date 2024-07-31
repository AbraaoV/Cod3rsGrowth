sap.ui.define([
	"ui5/codersgrowth/common/ControllerBase",
	"sap/ui/model/json/JSONModel",
	"ui5/codersgrowth/common/ConstantesLayoutDoApp"
], (ControllerBase, JSONModel, ConstantesLayoutDoApp) => {
	"use strict";
	const NOME_MODELO_DO_APP = "appView"
	const DELAY_DO_BUSY_INDICATOR = 0

return ControllerBase.extend("ui5.codersgrowth.app.App", {
	
		onInit: function () {
			this._definirModeloDoApp();
		},

		_definirModeloDoApp: function(){
			let oViewModel;

			oViewModel = new JSONModel({
				busy : false,
				delay : DELAY_DO_BUSY_INDICATOR,
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