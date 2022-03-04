using ExampleAPI.Models;
using MediatR;

namespace ExampleAPI.MediatorExample
{
    public class PostExampleItemCommand : IRequest<ExampleItem>
    {
        public ExampleItem Model { get; }
        public PostExampleItemCommand(ExampleItem model)
        {
            this.Model = model;
        }
    }

    public class PostExampleItemHandler
        : IRequestHandler<PostExampleItemCommand, ExampleItem>
    {
        private readonly TestContext Context;

        public PostExampleItemHandler(TestContext _context)
        {
            Context = _context;
        }

        public async Task<ExampleItem> Handle(
            PostExampleItemCommand request, CancellationToken cancellationToken)
        {
            ExampleItem model = request.Model;
            
            var entity = new ExampleItem()
            {
                Id = model.Id,
                Name = model.Name,
                IsCompleted = model.IsCompleted
            };

            Context.ExampleApis.Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }
    }
}
