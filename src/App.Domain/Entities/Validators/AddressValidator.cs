using FluentValidation;

namespace App.Domain.Entities.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("O campo Rua precisa ser fornecido")
                .Length(2, 200).WithMessage("O campo Rua precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.District)
                .NotEmpty().WithMessage("O campo Bairro precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo Bairro precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.ZipCode)
                .NotEmpty().WithMessage("O campo CEP precisa ser fornecido")
                .Length(8).WithMessage("O campo CEP precisa ter {MaxLength} caracteres");

            RuleFor(c => c.City)
                .NotEmpty().WithMessage("A campo Cidade precisa ser fornecida")
                .Length(2, 100).WithMessage("O campo Cidade precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.State)
                .NotEmpty().WithMessage("O campo Estado precisa ser fornecido")
                .Length(2, 50).WithMessage("O campo Estado precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Number)
                .NotEmpty().WithMessage("O campo Número precisa ser fornecido")
                .Length(1, 50).WithMessage("O campo Número precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}
