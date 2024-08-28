sap.ui.define([
	"sap/ui/test/opaQunit",
	"./AdicionarEditar",
], function (opaTest) {
	"use strict";

	QUnit.module("Dialgo Editar");

    opaTest("Deve abrir o formulario de editar pedido", function(Given, When, Then) {
        // Arrangements
        Given.iStartMyApp({
			hash: "cliente/2/"
		})
		//Actions
        When.naTabelaDePedido.aoClicarNoPedidoDaPosicao(2);
        //Actions
        When.naTabelaDePedido.aoClicarNaOpcao("Editar");
		// Assertions
		Then.naTelaDeAdicionarEditarPedido.deveAbrirFormularioDeAdicionarEditarPedido();
	});
	opaTest("Ao tentar editar um pedido com campos vazios deve retornar erros de validacao", function(Given, When, Then) {
		//Actions
        When.naTelaDeAdicionarEditarPedido.aoPreencherInputDeValor("");
        //Actions
        When.naTelaDeAdicionarEditarPedido.aoClicarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditarPedido.deveAperecerUmaMessageBoxDe("Alerta");
		// Assertions
		Then.naTelaDeAdicionarEditarPedido.deveFecharMessageBoxAoApertarEm("OK");
	});
	opaTest("Deve ser capaz de editar um pedido com valores validados", function(Given, When, Then){
		//Actions
		When.naTelaDeAdicionarEditarPedido
		.aoPreencherInputDeValor("330.50");
		//Actions
		When.naTelaDeAdicionarEditarPedido.aoClicarNaComboBoxFormaDePagamento();
		//Actions
        When.naTelaDeAdicionarEditarPedido.aoSelecionarNaComboBoxOValor("Pagamento efetuado no pix");
		//Actions'
		When.naTelaDeAdicionarEditarPedido.aoClicarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditarPedido
		.deveAperecerUmaMessageBoxDe("Êxito");
		// Assertions
		Then.naTelaDeAdicionarEditarPedido
		.deveFecharMessageBoxAoApertarEm("OK");
	});
	opaTest("Deve ser capaz de fechar o dialog de editar pedido ao clicar em cancelar", function(Given, When, Then){
        //Actions
        When.naTabelaDePedido.aoClicarNoPedidoDaPosicao(2);
        //Actions
        When.naTabelaDePedido.aoClicarNaOpcao("Editar");
		//Actions
        When.naTelaDeAdicionarEditarPedido.aoClicarEmCancelar();
		// Assertions
		Then.naTelaDeAdicionarEditarPedido.dialogDeAdicionarDeveEstarFechado();
		// Cleanup
        Then.iTeardownMyApp();
	});
});