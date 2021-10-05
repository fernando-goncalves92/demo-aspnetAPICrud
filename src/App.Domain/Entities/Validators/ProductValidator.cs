using FluentValidation;

namespace App.Domain.Entities.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty()
               .WithMessage("O campo Nome precisa ser fornecido")
               .Length(2, 200)
               .WithMessage("O campo Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("O campo Descrição precisa ser fornecido")
                .Length(2, 1000)
                .WithMessage("O campo Descrição precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(c => c.Value)
                .GreaterThan(0)
                .WithMessage("O campo sValor precisa ser maior que {ComparisonValue}");
        }
    }
}
