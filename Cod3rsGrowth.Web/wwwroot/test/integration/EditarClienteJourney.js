sap.ui.define([
	"sap/ui/test/opaQunit",
	"./pages/AdicionarCliente",
	"./pages/Lista"
], function (opaTest) {
	"use strict";

	QUnit.module("Tela de Edição");

	opaTest("Deve ser possivel navegar para a tela de editar", function (Given, When, Then) {
		// Arrangements
		Given.iStartMyApp();
        //Actions
        When.naTelaDeDetalhes.aoApertarEmEditar();
		// Assertions
		Then.naTelaDeAdicionar.deveNavegarParaTelaDeEditar();
	});
	opaTest("Ao tentar editar um cliente com campos vazios deve retornar erros de validacao", function(Given, When, Then) {
		//Actions
        When.naTelaDeAdicionar.aoPreencherNome("");
        //Actions
        When.naTelaDeAdicionar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionar.deveAperecerUmaMessageBoxDe("Alerta");
		// Assertions
		Then.naTelaDeAdicionar.deveFecharMessageBoxAoApertarEm("OK");
	});
	opaTest("Ao tentar atualizar um cliente com campos invalidos deve aparecer uma message box de erro", function(Given, When, Then){
		//Actions
		When.naTelaDeAdicionar.aoPreencherCpf("11111111111");
		//Actions
        When.naTelaDeAdicionar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionar.deveAperecerUmaMessageBoxDe("Erro");
		// Assertions
		Then.naTelaDeAdicionar.deveFecharMessageBoxAoApertarEm("Fechar");
	});
	
	opaTest("Após cadastrar um cliente deve conseguir voltar para tela inicial", function(Given, When, Then){
		//Actions
        When.naTelaDeAdicionar.aoPreencherNome("Cliente Editado");
		//Actions
		When.naTelaDeAdicionar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionar.deveAperecerUmaMessageBoxDe("Êxito");
		// Assertions
		Then.naTelaDeAdicionar.deveFecharMessageBoxAoApertarEm("Voltar à Página Inicial");
		// Cleanuo
	});
	opaTest("Botão de voltar negação deve voltar para pagina principal", function(Given, When, Then){
		//Actions
		When.naListaCliente.aoApertarEmAdicionar();
		//Actions
		When.naTelaDeAdicionar.aoApertaEmVoltar();
		// Assertions
		Then.naListaCliente.deveNavegarParaTelaDeLista();
		// Cleanuo
		Then.iTeardownMyApp();
	})

});