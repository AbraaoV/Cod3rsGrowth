﻿using Cod3rsGrowth.Dominio;
using FluentValidation;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ValidacaoPedido : AbstractValidator<Pedido>
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        public ValidacaoPedido(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;


            RuleFor(pedido => pedido.Data).NotEmpty().WithMessage("O campo Data é obrigatório.").NotNull().WithMessage("O campo Data é obrigatório.");

            RuleFor(pedido => pedido.NumeroCartao)
                .Length(ConstantesDoValidador.QUANTIDADE_DE_NUMEROS_PARA_CARTAO).When(pedido => pedido.FormaPagamento == Pedido.Pagamentos.Cartao)
                .WithMessage("Cartão invalido.");
            RuleFor(pedido => pedido.NumeroCartao)
                .Empty().When(pedido => pedido.FormaPagamento != Pedido.Pagamentos.Cartao)
                .WithMessage("O campo cartão deve estar vazio, casa o forma de pagamento não seja cartão");

            RuleFor(pedido => pedido.Valor).GreaterThan(ConstantesDoValidador.VALOR_MINIMO_PARA_PEDIDO).WithMessage("O valor do pedido deve ser maior que zero.");

            RuleFor(pedido => pedido.FormaPagamento).NotEmpty().WithMessage("O campo FormaPagamento é obrigatório.");

            RuleSet(ConstantesDoValidador.ATUALIZAR, () =>
            {
                RuleFor(pedido => pedido.Id)
                .Must(id =>
                {
                    return ValidarId(id) == true;
                })
                .WithMessage("Esse Id não existe.");
            });

            RuleSet(ConstantesDoValidador.REMOVER, () =>
            {
                RuleFor(pedido => pedido.Id)
                .Must(id =>
                {
                    return ValidarId(id) == true;
                })
                .WithMessage("Esse Id não existe.");
            });
        }
        public bool ValidarId(int id)
        {
            var obter = _pedidoRepositorio.ObterPorId(id);
            if (obter != null)
            {
                return true;
            }
            return false;
        }
    }
}
