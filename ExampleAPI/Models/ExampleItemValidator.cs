using FluentValidation;
using FluentValidation.Validators;

namespace ExampleAPI.Models
{
    public class ExampleItemValidator : AbstractValidator<ExampleItem>
    {
        public ExampleItemValidator()
        {
            RuleFor(exampleItem => exampleItem.Id)
                .GreaterThan(0).WithMessage("Id has to be greater than zero")
                .NotEmpty().WithMessage("Id cannot be empty")
                .NotNull().WithMessage("Id cannot be null");

            RuleFor(exampleItem => exampleItem.IsCompleted)
                .NotNull().WithMessage("IsCompleted cannot be null");

            RuleFor(exampleItem => exampleItem.Name)
                .MaximumLength(50).WithMessage("Name cannot be longer than 50 letters")
                .MinimumLength(2).WithMessage("Name has to be longer than 2 letters")
                .NotEmpty().WithMessage("Name cannot be empty")
                .NotNull().WithMessage("Name cannot be null");
        }
    }
}
