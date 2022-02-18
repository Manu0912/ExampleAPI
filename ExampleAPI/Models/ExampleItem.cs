using FluentValidation;

namespace ExampleAPI.Models
{
    public class ExampleItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class ExampleItemValidator : AbstractValidator<ExampleItem>
    {
        public ExampleItemValidator()
        {
            RuleFor(exampleItem => exampleItem.Name).MaximumLength(50);
            RuleFor(exampleItem => exampleItem.Id).GreaterThan(0);
        }
    }
}

