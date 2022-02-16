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

        public DbSet<ExampleItem> ExampleItems { get; set; } = null!;
    }
}
