using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ValidacaoPedido : AbstractValidator<Pedido>
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        public ValidacaoPedido(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;


            RuleFor(pedido => pedido.Data).NotEmpty().WithMessage("O campo Data é obrigatório.").NotNull().WithMessage("O campo Data é obrigatório.");

            RuleFor(pedido => pedido.NumeroCartao).Length(ConstantesDoValidador.QUANTIDADE_DE_NUMEROS_PARA_CARTAO).WithMessage("Cartão invalido.");

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
