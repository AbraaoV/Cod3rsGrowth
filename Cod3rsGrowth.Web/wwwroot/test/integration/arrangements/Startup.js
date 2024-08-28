sap.ui.define([
	"sap/ui/test/Opa5",
], function(Opa5) {
	"use strict";

	return Opa5.extend("ui5.codersgrowth.test.integration.arrangements.Startup", {
		iStartMyApp: function (oOptionsParameter) {
            var oOptions = oOptionsParameter || {};

            oOptions.delay = oOptions.delay || 50;

            this.iStartMyUIComponent({
                componentConfig: {
                    name: "ui5.codersgrowth",
                    async: true,
                    manifest: true
                },
                hash: oOptions.hash,
                autoWait: oOptions.autoWait
            });
        }
	});
});
