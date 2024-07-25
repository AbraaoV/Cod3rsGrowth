sap.ui.define([
	"ui5/codersgrowth/common/ControllerBase",
	"sap/ui/model/json/JSONModel"
], (ControllerBase, JSONModel) => {
	"use strict";

return ControllerBase.extend("ui5.codersgrowth.app.App", {
	
	onInit : function () {
			var oViewModel,

			oViewModel = new JSONModel({
				busy : false,
				delay : 0,
				layout : "OneColumn",
				previousLayout : "",
				actionButtonsInfo : {
					midColumn : {
						fullScreen : false
					}
				}
			});
			this._modelo(oViewModel, "appView");
		}
	});
});