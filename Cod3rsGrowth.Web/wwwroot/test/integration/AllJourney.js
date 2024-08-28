sap.ui.define([
	"sap/ui/test/Opa5",
	"./arrangements/Startup",
	"./pages/cliente/ListaJourney",
	"./pages/cliente/AdicionarClienteJourney",
	"./pages/cliente/DetalhesClienteJourney",
	"./pages/cliente/EditarClienteJourney",
	"./pages/cliente/DeletarClienteJourney",
	"./pages/pedido/TabelaJourney",
	"./pages/pedido/AdicionarPedidoJourney",
	"./pages/pedido/EditarPedidoJourney",
	"./pages/pedido/DeletarPedidoJourney"
], function (Opa5, Startup) {
	"use strict";

	Opa5.extendConfig({
		arrangements: new Startup(),
		viewNamespace: "ui5.codersgrowth.app",
		autoWait: true
	});
});
