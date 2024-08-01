sap.ui.define([
	"sap/ui/test/opaQunit",
	"./pages/Lista",
    "./pages/DetalhesCliente",
    "./pages/App"
], function (opaTest) {
	"use strict";

	QUnit.module("Detalhes do cliente");

	
    opaTest("Deve ser capaz de filtrar por nome com a pagina de detalhes aberta ao lado", function(Given, When, Then) {
		Given.iStartMyApp({
			hash: "cliente/38/"
		})
		//Actions
		When.naListaCliente.aoPesquisarNome("Empresa");
		//Assertions
		Then.naListaCliente.listaDeveEstarFiltradaPorNome("Empresa");
        Then.naPaginaDoApp.oFlexibleColumnLayoutDoAppDeveSer("TwoColumnsMidExpanded");
	});
	opaTest("Deve ser capaz de limpar a campo de pesquisa com a pagina de detalhes aberta ao lado", function(Given, When, Then) {
		//Actions
		When.naListaCliente.aoPesquisarNome("");
		//Assertions
		Then.naListaCliente.listaDeveConterDezClientesNaPagina();
        Then.naPaginaDoApp.oFlexibleColumnLayoutDoAppDeveSer("TwoColumnsMidExpanded");
	});
	opaTest("Deve ser capaz de filtrar por tipo de pessoa com a pagina de detalhes aberta ao lado", function(Given, When, Then) {
		// Action
		When.naListaCliente.aoApertarBotaoFiltro();
		When.naListaCliente.aoSelecionarFiltroTipoDePessoa();
		When.naListaCliente.aoSelecionarOTipoDePessoaAFiltrar("Pessoa Jurídica");
		When.naListaCliente.aoApertarBotaoOkNoFiltro();
		// Assertion
		Then.naListaCliente.listaDeveEstarFiltradaPorTipoDePessoa("Juridica");
        Then.naPaginaDoApp.oFlexibleColumnLayoutDoAppDeveSer("TwoColumnsMidExpanded");
	});
	opaTest("Deve ser capaz de resetar os filtro do tipo de pessoa com a pagina de detalhes aberta ao lado", function(Given, When, Then) {
		// Action
		When.naListaCliente.aoApertarBotaoFiltro();
		When.naListaCliente.aoResetarFiltroTipo();
		When.naListaCliente.aoApertarBotaoOkNoFiltro();
		// Assertion
		Then.naListaCliente.listaDeveConterDezClientesNaPagina();
        Then.naPaginaDoApp.oFlexibleColumnLayoutDoAppDeveSer("TwoColumnsMidExpanded");
	});
    opaTest("Deve ser capaz de entrar no modo em tela cheia", function (Given, When, Then) {
        //Actions
        When.naTelaDeDetalhes.aoClicarNoBotaoDe("botaoTelaCheia",);
		// Assertions
		Then.naPaginaDoApp.oFlexibleColumnLayoutDoAppDeveSer("MidColumnFullScreen");
	});
    opaTest("Deve ser capaz de sair do modo tela cheia", function (Given, When, Then) {
        //Actions
        When.naTelaDeDetalhes.aoClicarNoBotaoDe("botaoSairTelaCheia",);
		// Assertions
		Then.naPaginaDoApp.oFlexibleColumnLayoutDoAppDeveSer("TwoColumnsMidExpanded");
	});
    opaTest("Deve ser capaz de fechar a tela de detalhes", function (Given, When, Then) {
        //Actions
        When.naTelaDeDetalhes.aoClicarNoBotaoDe("botaoFecharDetalhes");
		// Assertions
		Then.naListaCliente.listaDeveConterDezClientesNaPagina();
        // Cleanuo
        Then.iTeardownMyApp();
	});
});