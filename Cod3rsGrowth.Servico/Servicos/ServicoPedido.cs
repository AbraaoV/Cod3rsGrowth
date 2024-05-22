using Cod3rsGrowth.Servico.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cod3rsGrowth.Infra;
using Cod3rsGrowth.Dominio;
using FluentValidation.Results;
using FluentValidation;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ServicoPedido : IServicoPedido
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        private readonly ValidacaoPedido _validarPedido;
        public ServicoPedido(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio = pedidoRepositorio;
            _validarPedido = new ValidacaoPedido();
        }
        public List<Pedido> ObterTodos()
        {
            var pedidos = _pedidoRepositorio.ObterTodos();
            return pedidos;
        }
        public Pedido ObterPorId(int id)
        {
            var pedidos = _pedidoRepositorio.ObterPorId(id);
            return pedidos;
        }
        public void Adicionar(Pedido pedido)
        {
            ValidationResult result = _validarPedido.Validate(pedido);
            if (result.IsValid)
            {
                _pedidoRepositorio.Adicionar(pedido);
            }
            else if (!result.IsValid)
            {
                string mensagemErro = string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException("Falha na validação dos dados:" + Environment.NewLine + mensagemErro);
            }
        }

    }
}
