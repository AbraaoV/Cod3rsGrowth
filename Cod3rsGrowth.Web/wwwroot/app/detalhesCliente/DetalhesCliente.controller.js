sap.ui.define([
	"../model/formatter",
	"ui5/codersgrowth/common/ControllerBase",
	"ui5/codersgrowth/common/ConstantesDoBanco",
	"ui5/codersgrowth/common/ConstantesLayoutDoApp"
], function (formatter, ControllerBase, ConstantesDoBanco, ConstantesLayoutDoApp) {
	"use strict";
	const NOME_DA_ROTA_DE_DETALHE = "detalhesCliente"
	const NOME_DA_ROTA_TELA_DE_LISTA = "lista"
	const ID_BOTAO_TELA_CHEIA = "botaoTelaCheia"
	const ID_BOTAO_SAIR_TELA_CHEIA = "botaoSairTelaCheia"
	const NOME_DO_MODELO_DO_CLIENTE = "clienteSelecionado"
	const NOME_DO_MODELO_DO_APP = "appView" 

	return ControllerBase.extend("ui5.codersgrowth.app.detalhesCliente.DetalhesCliente", {
		formatter: formatter,
		onInit: function () {
			this.obterRota().getRoute(NOME_DA_ROTA_DE_DETALHE).attachPatternMatched(this._aoCarregar, this);
		},

        aoFecharDetalhes: function () {
			this.obterModelo(NOME_DO_MODELO_DO_APP).setProperty("/actionButtonsInfo/midColumn/fullScreen", false);
			this.obterRota().navTo(NOME_DA_ROTA_TELA_DE_LISTA);
		},

        aoClicarEmTelaCheia: function () {
			this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_MEIO_TELA_CHEIA)
			this.peloId(ID_BOTAO_SAIR_TELA_CHEIA).setVisible(true);
			this.peloId(ID_BOTAO_TELA_CHEIA ).setVisible(false);
		},
		aoClicarEmFecharTelaCheia: function () {
			this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_DUAS_COLUNAS_DIVIDAS)
			this.peloId(ID_BOTAO_SAIR_TELA_CHEIA).setVisible(false);
			this.peloId(ID_BOTAO_TELA_CHEIA).setVisible(true);
		},

		_aoCarregar: function () {
			this.mudarLayout(ConstantesLayoutDoApp.LAYOUT_DUAS_COLUNAS_DIVIDAS)
			const obterParametros = this.obterRota().getHashChanger().getHash().split("/");
			this._get(ConstantesDoBanco.CAMINHO_PARA_API + "/" + obterParametros[1], NOME_DO_MODELO_DO_CLIENTE );
		}
	});
});
