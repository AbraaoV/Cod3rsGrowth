using Cod3rsGrowth.Dominio;
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
        public ValidacaoPedido()
        {
            RuleFor(pedido => pedido.Data).NotEmpty().NotNull().WithMessage("O campo Data é obrigatório.");

            RuleFor(pedido => pedido.NumeroCartao).Length(16).WithMessage("Cartão invalido.");

            RuleFor(pedido => pedido.Valor).GreaterThan(0).WithMessage("O valor do pedido deve ser maior que zero.");

            RuleFor(pedido => pedido.FormaPagamento).NotEmpty().WithMessage("O campo FormaPagamento é obrigatório.");
 
        }
    }
}
