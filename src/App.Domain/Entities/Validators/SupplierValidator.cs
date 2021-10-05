using App.Domain.Validators;
using FluentValidation;

namespace App.Domain.Entities.Validators
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(f => f.Name)
                .NotEmpty()
                .WithMessage("O campo Nome precisa ser fornecido")
                .Length(2, 100)
                .WithMessage("O campo Nome precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.SupplierType == SupplierType.Person, () =>
            {
                RuleFor(f => f.Document.Length)
                .Equal(CpfValidator.CpfLength)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
                
                RuleFor(f => CpfValidator.Validate(f.Document))
                .Equal(true)
                .WithMessage("O documento fornecido é inválido.");
            });

            When(f => f.SupplierType == SupplierType.Company, () =>
            {
                RuleFor(f => f.Document.Length)
                .Equal(CnpjValidator.CnpjLength)
                .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                RuleFor(f => CnpjValidator.Validate(f.Document))
                .Equal(true)
                .WithMessage("O documento fornecido é inválido.");
            });
        }
    }
}
