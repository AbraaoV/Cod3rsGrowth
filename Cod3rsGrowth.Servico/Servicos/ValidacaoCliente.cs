using Cod3rsGrowth.Dominio;
using FluentValidation;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ValidacaoCliente : AbstractValidator<Cliente>
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        public ValidacaoCliente(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;


            RuleFor(cliente => cliente.Nome).NotEmpty().WithMessage("O nome é um campo obrigatório.")
                .MaximumLength(ConstantesDoValidador.TAMANHO_MAXIMO_DO_NOME).WithMessage("O nome não pode ter mais de 50 caracteres");

            RuleFor(cliente => cliente.Tipo).NotEmpty().WithMessage("O Tipo é um campo obrigatório");

            RuleFor(cliente => cliente.Cnpj)
                .Empty()
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Fisica)
                .WithMessage("Para pessoa física, não informe Cnpj.");

            RuleFor(cliente => cliente.Cpf)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Fisica)
                .WithMessage("Para pessoa física, o Cpf é obrigatório.")
                .Length(ConstantesDoValidador.QUANTIDADE_DE_NUMEROS_PARA_CPF).When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Fisica)
                .WithMessage("CPF inválido");

            RuleFor(cliente => cliente.Cpf)
                .Empty()
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
                .WithMessage("Para pessoa júridica, não informe Cpf.");

            RuleFor(cliente => cliente.Cnpj)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
                .WithMessage("Para pessoa júridica, o Cnpj é obrigatório.")
                .Length(ConstantesDoValidador.QUANTIDADE_DE_NUMEROS_PARA_CNPJ).When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
                .WithMessage("CNPJ inválido");


            RuleSet(ConstantesDoValidador.ATUALIZAR, () =>
            {
                RuleFor(cliente => cliente.Id)
                .Must(id =>
                {
                    return ValidarId(id) == true;
                })
                .WithMessage("Esse Id não existe.");
            });

            RuleSet(ConstantesDoValidador.REMOVER, () =>
            {
                RuleFor(cliente => cliente.Id)
                .Must(id =>
                {
                    return ValidarId(id) == true;
                })
                .WithMessage("Esse Id não existe.");
            });


        }
        public bool ValidarId(int id)
        {
           var obter = _clienteRepositorio.ObterPorId(id);
            if (obter != null)
            {
              return true;
            }
              return false;
        }
    }
}

