sap.ui.define([
	"sap/ui/test/opaQunit",
	"./pages/AdicionarEditarCliente",
	"./pages/Lista"
], function (opaTest) {
	"use strict";

	QUnit.module("Tela de Edição");

	opaTest("Ao tentar editar um cliente com campos vazios deve retornar erros de validacao", function(Given, When, Then) {
		// Arrangements
		Given.iStartMyApp({
			hash: "cliente/1/editar"
		})
		//Actions
        When.naTelaDeAdicionarEditar.aoPreencherNome("");
        //Actions
        When.naTelaDeAdicionarEditar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditar.deveAperecerUmaMessageBoxDe("Alerta");
		// Assertions
		Then.naTelaDeAdicionarEditar.deveFecharMessageBoxAoApertarEm("OK");
	});
	opaTest("Ao tentar atualizar um cliente com campos invalidos deve aparecer uma message box de erro", function(Given, When, Then){
		When.naTelaDeAdicionarEditar.aoPreencherNome("Nome");
		//Actions
		When.naTelaDeAdicionarEditar.aoPreencherCpf("11111111111");
		//Actions
        When.naTelaDeAdicionarEditar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditar.deveAperecerUmaMessageBoxDe("Erro");
		// Assertions
		Then.naTelaDeAdicionarEditar.deveFecharMessageBoxAoApertarEm("Fechar");
	});
	
	opaTest("Após cadastrar um cliente deve conseguir voltar para tela inicial", function(Given, When, Then){
		//Actions
        When.naTelaDeAdicionarEditar.aoPreencherNome("Cliente Editado");
		//Actions
		When.naTelaDeAdicionarEditar.aoPreencherCpf("07378567000");
		//Actions
		When.naTelaDeAdicionarEditar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditar.deveAperecerUmaMessageBoxDe("Êxito");
		// Assertions
		Then.naTelaDeAdicionarEditar.deveFecharMessageBoxAoApertarEm("Voltar à Página Inicial");
		// Cleanup
		Then.iTeardownMyApp();
	});
});