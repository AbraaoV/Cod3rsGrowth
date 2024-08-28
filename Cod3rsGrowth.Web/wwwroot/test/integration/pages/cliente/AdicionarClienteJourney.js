sap.ui.define([
	"sap/ui/test/opaQunit",
	"./AdicionarEditarCliente",
	"./Lista"
], function (opaTest) {
	"use strict";

	QUnit.module("Tela de cadastro");

	opaTest("Ao tentar adicionar um cliente com campos vazios deve retornar erros de validacao", function(Given, When, Then) {
		// Arrangements
		Given.iStartMyApp({
			hash: "adicionar"
		})
		//Actions
        When.naTelaDeAdicionarEditar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditar.deveAperecerUmaMessageBoxDe("Alerta");
		// Assertions
		Then.naTelaDeAdicionarEditar.deveFecharMessageBoxAoApertarEm("OK");
	});
	opaTest("Ao tentar adicionar um cliente invalido deve aparecer uma message box de erro", function(Given, When, Then){
		//Actions
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
	opaTest("Ao adicionar um cliente valido deve ser adicionado com sucesso, e continuar na tela de adicionar ao clicar em novo cadastro", function(Given, When, Then){
		//Actions
        When.naTelaDeAdicionarEditar.aoPreencherNome("Cliente C");
		//Actions
		When.naTelaDeAdicionarEditar.aoPreencherCpf("56427563033");
		//Actions
		When.naTelaDeAdicionarEditar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditar.deveAperecerUmaMessageBoxDe("Êxito");
		// Assertions
		Then.naTelaDeAdicionarEditar.deveFecharMessageBoxAoApertarEm("Novo Cadastro");
	});
	opaTest("Após cadastrar um cliente deve conseguir voltar para tela inicial", function(Given, When, Then){
		//Actions
        When.naTelaDeAdicionarEditar.aoPreencherNome("Empresa C");
		//Actions
		When.naTelaDeAdicionarEditar.aoClicarNaComboxPessoa();
		//Actions
		When.naTelaDeAdicionarEditar.aoSelecionarNaComboBox("Pessoa Jurídica");
		//Actions
		When.naTelaDeAdicionarEditar.aoPreencherCnpj("27859908000101");
		//Actions
		When.naTelaDeAdicionarEditar.aoApertarEmSalvar();
		// Assertions
		Then.naTelaDeAdicionarEditar.deveAperecerUmaMessageBoxDe("Êxito");
		// Assertions
		Then.naTelaDeAdicionarEditar.deveFecharMessageBoxAoApertarEm("Voltar à Página Inicial");
		// Cleanuo
		Then.iTeardownMyApp();
	});
	

});