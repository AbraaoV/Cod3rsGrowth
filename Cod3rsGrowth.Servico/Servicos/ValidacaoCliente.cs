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
                .Must(cpf =>
                {
                    return ValidarCpf(cpf);
                })
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Fisica)
                .WithMessage("CPF inválido");
                

            RuleFor(cliente => cliente.Cpf)
                .Empty()
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
                .WithMessage("Para pessoa júridica, não informe Cpf.");

            RuleFor(cliente => cliente.Cnpj)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
                .WithMessage("Para pessoa júridica, o Cnpj é obrigatório.")
                .Must(cnpj =>
                {
                    return ValidarCnpj(cnpj);
                })
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
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

            RuleSet(ConstantesDoValidador.ADICIONAR, () =>
            {
                RuleFor(cliente => cliente.Cpf)
                 .Must(cpf =>
                  {
                      return CpfInedito(cpf);
                  })
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Fisica)
                .WithMessage("Esse CPF já está cadastrado");
                RuleFor(cliente => cliente.Cnpj)
                .Must(cnpj =>
                 {
                     return CnpjInedito(cnpj);
                 })
                .When(cliente => cliente.Tipo == Cliente.TipoDeCliente.Juridica)
                .WithMessage("Esse CNPJ já está cadastrado");
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

        public bool CpfInedito(string cpf)
        {
            if(_clienteRepositorio.ObterTodos(null).Any(c => c.Cpf == cpf))
                return false;
            else
                return true;
        }

        public bool CnpjInedito(string cnpj)
        {
            if (_clienteRepositorio.ObterTodos(null).Any(c => c.Cnpj == cnpj))
                return false;
            else
                return true;
        }
        public bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        private static bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}

