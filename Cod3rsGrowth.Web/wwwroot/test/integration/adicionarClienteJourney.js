sap.ui.define([
	"sap/ui/test/opaQunit",
	"./pages/AdicionarCliente",
	"./pages/Lista"
], function (opaTest) {
	"use strict";

	QUnit.module("Tela de cadastro");

	opaTest("Ao clicar em adicionar deve navegar para tela de adicionar", function (Given, When, Then) {
		// Arrangements
		Given.iStartMyApp();
        //Actions
        When.naListaCliente.aoApertarEmAdicionar();
		// Assertions
		Then.naTelaDeAdicionar.deveNavegarParaTelaDeAdicionar();
	});
	opaTest("Ao tentar adicionar um cliente com campos vazios deve retornar erros de validacao", function(Given, When, Then) {
		//Actions
        When.naTelaDeAdicionar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionar.deveAperecerUmaMessageBoxDe("Alerta");
		// Assertions
		Then.naTelaDeAdicionar.deveFecharMessageBoxAoApertarEm("OK");
	});
	opaTest("Ao tentar adicionar um cliente invalido deve aparecer uma message box de erro", function(Given, When, Then){
		//Actions
        When.naTelaDeAdicionar.aoPreencherNome("Nome");
		//Actions
		When.naTelaDeAdicionar.aoPreencherCpf("11111111111");
		//Actions
        When.naTelaDeAdicionar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionar.deveAperecerUmaMessageBoxDe("Erro");
		// Assertions
		Then.naTelaDeAdicionar.deveFecharMessageBoxAoApertarEm("Fechar");
	});
	opaTest("Ao adicionar um cliente valido deve ser adicionado com sucesso, e continuar na tela de adicionar ao clicar em novo cadastro", function(Given, When, Then){
		//Actions
        When.naTelaDeAdicionar.aoPreencherNome("Cliente C");
		//Actions
		When.naTelaDeAdicionar.aoPreencherCpf("56427563033");
		//Actions
		When.naTelaDeAdicionar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionar.deveAperecerUmaMessageBoxDe("Êxito");
		// Assertions
		Then.naTelaDeAdicionar.deveFecharMessageBoxAoApertarEm("Novo Cadastro");
	});
	opaTest("Após cadastrar um cliente deve conseguir voltar para tela inicial", function(Given, When, Then){
		//Actions
        When.naTelaDeAdicionar.aoPreencherNome("Empresa C");
		//Actions
		When.naTelaDeAdicionar.aoClicarNaComboxPessoa();
		//Actions
		When.naTelaDeAdicionar.aoSelecionarNaComboBox("Pessoa Jurídica");
		//Actions
		When.naTelaDeAdicionar.aoPreencherCnpj("27859908000101");
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