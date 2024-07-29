sap.ui.define([
	"ui5/codersgrowth/common/ControllerBase",
	"sap/ui/model/json/JSONModel",
	"ui5/codersgrowth/common/ConstantesDoBanco",
	"ui5/codersgrowth/common/ConstantesLayoutDoApp"
], (ControllerBase, JSONModel, ConstantesDoBanco, ConstantesLayoutDoApp) => {
	"use strict";
	const NOME_MODELO_DO_APP = "appView"
	const PARAMETRO_FILTRO_NOME = "nome";
	const PARAMETRO_FILTRO_TIPO = "tipo";
	const NOME_DO_MODELO_DA_LISTA = "listaDeClientes";
	let _filtroTipo = null;
	let _filtroNome = "";

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

			// const urlParams = new URLSearchParams(window.location.search);

			// _filtroNome = urlParams.get(PARAMETRO_FILTRO_NOME);
			// _filtroTipo = urlParams.has(PARAMETRO_FILTRO_TIPO) ? parseInt(urlParams.get(PARAMETRO_FILTRO_TIPO)) : null;
			// var urlFinal = ConstantesDoBanco.CAMINHO_PARA_API + "?" + urlParams;
			// this._get(urlFinal, NOME_DO_MODELO_DA_LISTA);
		}
	});
});