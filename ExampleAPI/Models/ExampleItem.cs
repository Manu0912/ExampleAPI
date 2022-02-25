using FluentValidation;

namespace ExampleAPI.Models
{
    public class ExampleItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public bool IsCompleted { get; set; }

    }

    
}

