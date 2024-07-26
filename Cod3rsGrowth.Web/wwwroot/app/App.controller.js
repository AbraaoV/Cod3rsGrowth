sap.ui.define([
	"ui5/codersgrowth/common/ControllerBase",
	"sap/ui/model/json/JSONModel",
	"ui5/codersgrowth/common/ConstantesDoBanco",
	"ui5/codersgrowth/common/ConstantesLayoutDoApp"
], (ControllerBase, JSONModel, ConstantesDoBanco, ConstantesLayoutDoApp) => {
	"use strict";
const NOME_MODELO_DA_LISTA = "listaDeClientes"
const NOME_MODELO_DO_APP = "appView"

return ControllerBase.extend("ui5.codersgrowth.app.App", {
	
	onInit : function () {
			var oViewModel,

			oViewModel = new JSONModel({
				busy : false,
				delay : 0,
				layout : ConstantesLayoutDoApp.LAYOUT_UMA_COLUNA,
				previousLayout : "",
				actionButtonsInfo : {
					midColumn : {
						fullScreen : false
					}
				}
			});
			this._modelo(oViewModel, NOME_MODELO_DO_APP);

			this._get(ConstantesDoBanco.CAMINHO_PARA_API, NOME_MODELO_DA_LISTA)
		}
	});
});