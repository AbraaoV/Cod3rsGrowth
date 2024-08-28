sap.ui.define([
	"sap/ui/test/opaQunit",
	"./AdicionarEditar",
], function (opaTest) {
	"use strict";

	QUnit.module("DialgoAdicionar");

    opaTest("Deve abrir o formulario de adicionar pedido", function(Given, When, Then) {
        // Arrangements
        Given.iStartMyApp({
			hash: "cliente/2/"
		})
		//Actions
        When.naTabelaDePedido.aoClicarEmAdicionar();
		// Assertions
		Then.naTelaDeAdicionarEditarPedido
		.deveAbrirFormularioDeAdicionarEditarPedido();
	});
	opaTest("Ao tentar adicionar um pedido com campos vazios deve retornar erros de validacao", function(Given, When, Then) {
		//Actions
        When.naTelaDeAdicionarEditarPedido
		.aoClicarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditarPedido
		.deveAperecerUmaMessageBoxDe("Alerta");
		// Assertions
		Then.naTelaDeAdicionarEditarPedido
		.deveFecharMessageBoxAoApertarEm("OK");
	});
	opaTest("Ao adicionar um pedido valido deve ser adicionado com sucesso", function(Given, When, Then){
		//Actions
        When.naTelaDeAdicionarEditarPedido
		.aoPreencherInputDeData("2024-06-12T12:34:09.923");
		//Actions
		When.naTelaDeAdicionarEditarPedido
		.aoPreencherInputDeValor("35.50");
		//Actions
		When.naTelaDeAdicionarEditarPedido
		.aoClicarNaComboBoxFormaDePagamento();
		//Actions
        When.naTelaDeAdicionarEditarPedido
		.aoSelecionarNaComboBoxOValor("Pagamento efetuado no pix");
		//Actions
		When.naTelaDeAdicionarEditarPedido
		.aoClicarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditarPedido
		.deveAperecerUmaMessageBoxDe("Êxito");
		// Assertions
		Then.naTelaDeAdicionarEditarPedido
		.deveFecharMessageBoxAoApertarEm("OK");
	});
	opaTest("Deve ser capaz de fechar o dialog de adicionar pedido ao clicar em cancelar", function(Given, When, Then){
		//Actions
        When.naTabelaDePedido.aoClicarEmAdicionar();
		//Actions
        When.naTelaDeAdicionarEditarPedido
		.aoClicarEmCancelar();
		// Assertions
		Then.naTelaDeAdicionarEditarPedido
		.dialogDeAdicionarDeveEstarFechado();
		// Cleanup
        Then.iTeardownMyApp();
	});
});