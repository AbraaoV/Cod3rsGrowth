using Cod3rsGrowth.Dominio;
using Cod3rsGrowth.Infra;
using FluentValidation;

namespace Cod3rsGrowth.Servico.Servicos
{
    public class ValidacaoCliente : AbstractValidator<Cliente>
    {
        public ValidacaoCliente()
        {
            RuleFor(cliente => cliente.Nome).NotEmpty().WithMessage("O nome é um campo obrigatório.")
                .MaximumLength(50).WithMessage("O nome não pode ter mais de 50 caracteres");

            //RuleFor(cliente => cliente.Cpf).Length(11).WithMessage("CPF inválido");

            //RuleFor(cliente => cliente.Cnpj).NotEmpty().Length(14).WithMessage("CNPJ inválido");

            //RuleFor(cliente => cliente.Tipo).NotEmpty().WithMessage("O Tipo é um campo obrigatório");

            RuleFor(cliente => cliente.Tipo)
                .NotEqual(Cliente.TipoDeCliente.Juridica).WithMessage("aqui")
                .When(cliente => !string.IsNullOrEmpty(cliente.Cnpj))
                .WithMessage("Informe apenas CPF para pessoa física.")
                .When(cliente => string.IsNullOrEmpty(cliente.Cpf))
                .WithMessage("O campo CPF, é obrigatório para pessoa física.");

            RuleFor(cliente => cliente.Tipo)
                .NotEqual(Cliente.TipoDeCliente.Fisica)
                .When(cliente => !string.IsNullOrEmpty(cliente.Cnpj))
                .WithMessage("Informe apenas CNPJ para pessoa jurídica.")
                .When(cliente => string.IsNullOrEmpty(cliente.Cpf))
                .WithMessage("O campo CNPJ, é obrigatório para pessoa jurídica.");

        }
    }
}

