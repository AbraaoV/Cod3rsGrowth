sap.ui.define([
	"sap/ui/test/opaQunit",
	"./Lista"
], function (opaTest) {
	"use strict";

	QUnit.module("Lista de Clientes");

	opaTest("Deve ser capaz de carregar mais itens", function (Given, When, Then) {
		// Arrangements
		Given.iStartMyApp();
        //Actions
        When.naListaCliente.aoApertarEmMais();
		// Assertions
		Then.naListaCliente.listaDeveMostrarTodosOsClientes();
	});
	opaTest("Deve ser capaz de filtrar por nome", function(Given, When, Then) {
		//Actions
		When.naListaCliente.aoPesquisarNome("João");
		//Assertions
		Then.naListaCliente.listaDeveEstarFiltradaPorNome("João");
	});
	opaTest("Deve ser capaz de limpar a campo de pesquisa", function(Given, When, Then) {
		//Actions
		When.naListaCliente.aoPesquisarNome("");
		//Assertions
		Then.naListaCliente.listaDeveConterDezClientesNaPagina();
	});
	opaTest("Lista cliente filtrado por pessoa física", function(Given, When, Then) {
		// Action
		When.naListaCliente.aoApertarBotaoFiltro();
		When.naListaCliente.aoSelecionarFiltroTipoDePessoa();
		When.naListaCliente.aoSelecionarOTipoDePessoaAFiltrar("Pessoa Jurídica");
		When.naListaCliente.aoApertarBotaoOkNoFiltro();
		// Assertion
		Then.naListaCliente.listaDeveEstarFiltradaPorTipoDePessoa("Pessoa Jurídica");
	});
	opaTest("Deve ser capaz de resetar os filtro do tipo de pessoa", function(Given, When, Then) {
		// Action
		When.naListaCliente.aoApertarBotaoFiltro();
		When.naListaCliente.aoResetarFiltroTipo();
		When.naListaCliente.aoApertarBotaoOkNoFiltro();
		// Assertion
		Then.naListaCliente.listaDeveConterDezClientesNaPagina();
		
	});
	opaTest("Ao clicar em adicionar deve navegar para tela de adicionar", function (Given, When, Then) {
        //Actions
        When.naListaCliente.aoApertarEmAdicionar();
		// Assertions
		Then.naTelaDeAdicionarEditar.deveNavegarParaTelaDeAdicionar();
	});
	opaTest("Botão de voltar negação deve voltar para pagina principal", function(Given, When, Then){
		//Actions
		When.naTelaDeAdicionarEditar.aoApertaEmVoltar();
		// Assertions
		Then.naListaCliente.deveNavegarParaTelaDeLista();
	})
	opaTest("Deve ser capaz de navegar para tela de detalhes", function (Given, When, Then) {
        //Actions
        When.naListaCliente.aoClicarNoClienteDaPosicao(1);
		// Assertions
		Then.naTelaDeDetalhes.deveEstarNaTelaDeDetalhes();
	});
	opaTest("Deve ser capaz de navegar para e tela de edicao de cliente, ao clicar em editar", function (Given, When, Then) {
		//Actions
        When.naTelaDeDetalhes.aoClicarNoBotaoDe("botaoEditar");
		//Assertions
		Then.naTelaDeAdicionarEditar.deveEstarNaTelaDeEditar();
	});
	opaTest("Deve ser capaz de voltar para a tela de detalhes ao clicar em voltar", function (Given, When, Then) {
		//Assertions
		When.naTelaDeAdicionarEditar.aoApertaEmVoltar();
		When.naTelaDeAdicionarEditar.aoApertaEmVoltarSegundoClique();
		Then.naTelaDeDetalhes.deveEstarNaTelaDeDetalhes()
		// Cleanuo
		Then.iTeardownMyApp();
	});
});