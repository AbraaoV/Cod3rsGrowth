sap.ui.define([
	"ui5/codersgrowth/common/ControllerBase",
	"sap/ui/model/json/JSONModel",
	"ui5/codersgrowth/common/ConstantesLayoutDoApp",
	"sap/ui/model/resource/ResourceModel"
], (ControllerBase, JSONModel, ConstantesLayoutDoApp, ResourceModel) => {
	"use strict";
	const NOME_MODELO_DO_APP = "appView"
	const DELAY_DO_BUSY_INDICATOR = 0
	const NOME_DO_MODELO_I18N = "i18n"
	const DIRETORIO_DO_I18N = "ui5.codersgrowth.i18n.i18n"

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
		},

		_definirModeloI18n: function(){
			const i18nModel = new ResourceModel({
				bundleName: DIRETORIO_DO_I18N
			});
			this._modelo(i18nModel, NOME_DO_MODELO_I18N );
		}

	});
});