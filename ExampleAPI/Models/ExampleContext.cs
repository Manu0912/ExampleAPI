using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ExampleAPI.Models
{
    public class ExampleContext : DbContext
    {
        public ExampleContext(DbContextOptions<ExampleContext> options)
            : base(options)
        {
        }

        public DbSet<ExampleContext> TodoItems { get; set; } = null!;
    }
}
