sap.ui.define([
	"sap/ui/test/Opa5",
	"sap/ui/test/matchers/Properties"
], function (Opa5, Properties) {
	"use strict";

	Opa5.createPageObjects({
		naPaginaDoApp: {
			actions: {
				
			},

			assertions: {
				oFlexibleColumnLayoutDoAppDeveSer: function (sLayout) {
					return this.waitFor({
						id: "layout",
						viewName: "App",
						matchers: new Properties({
							layout: sLayout
						}),
						success: function () {
							Opa5.assert.ok(true, "O layout atual do app é " + sLayout);
						},
						errorMessage: "O app não está no layout " + sLayout
					});
				}
			}
		}
	});
});
