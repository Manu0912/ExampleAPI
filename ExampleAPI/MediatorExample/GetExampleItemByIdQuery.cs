using ExampleAPI.Models;
using MediatR;

namespace ExampleAPI.MediatorExample
{
    public class GetExampleItemByIdQuery : IRequest<ExampleItem>
    {
        public long Id { get; }

        public GetExampleItemByIdQuery(int id)
        {
            Id = id;
        }
    }

    public class GetExampleItemByIdHandler : IRequestHandler<GetExampleItemByIdQuery, ExampleItem>
    {
        private readonly TestContext Context;

        public GetExampleItemByIdHandler(TestContext _context)
        {
            Context = _context;
        }

        public async Task<ExampleItem> Handle(GetExampleItemByIdQuery request, CancellationToken cancellationToken)
        {
            var exampleItem = await Context.ExampleApis.FindAsync(request.Id);
            return exampleItem;
        }
    }
}
