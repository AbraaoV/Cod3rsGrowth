﻿using Cod3rsGrowth.Dominio;
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

            RuleFor(cliente => cliente.Tipo).NotEmpty().WithMessage("O Tipo é um campo obrigatório");

            RuleFor(cliente => cliente.Cnpj)
                .Empty()
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Fisica)
                .WithMessage("Para pessoa física, não informe Cnpj.");

            RuleFor(cliente => cliente.Cpf)
                .NotEmpty()
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Fisica)
                .WithMessage("Para pessoa física, o Cpf é obrigatório.")
                .Length(11)
                .WithMessage("CPF inválido");

            RuleFor(cliente => cliente.Cpf)
                .Empty()
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
                .WithMessage("Para pessoa júridica, não informe Cpf.");

            RuleFor(cliente => cliente.Cnpj)
                .NotEmpty()
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
                .WithMessage("Para pessoa júridica, o Cpnj é obrigatório.")
                .Length(14)
                .WithMessage("CNPJ inválido");





        }
    }
}
