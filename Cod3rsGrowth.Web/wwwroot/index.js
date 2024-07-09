sap.ui.define([
	"sap/ui/core/ComponentContainer"
], (ComponentContainer) => {
	"use strict";

	new ComponentContainer({
		name: "ui5.codersgrowth",
		settings : {
			id : "codersgrowth"
		},
		async: true
	}).placeAt("content");
});