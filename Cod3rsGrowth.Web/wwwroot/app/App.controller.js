sap.ui.define([
   "ui5/codersgrowth/common/ControllerBase",
   "sap/ui/model/json/JSONModel"
], (ControllerBase, JSONModel) => {
   "use strict";

   return ControllerBase.extend("ui5.codersgrowth.app.App", {
      
      onInit : function () {
			var oViewModel,
				fnSetAppNotBusy,
				iOriginalBusyDelay = this.getView().getBusyIndicatorDelay();

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
			this.getView().setModel(oViewModel, "appView");

			fnSetAppNotBusy = function() {
				oViewModel.setProperty("/busy", false);
				oViewModel.setProperty("/delay", iOriginalBusyDelay);
			};

		}
   });
});