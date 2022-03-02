using ExampleAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExampleAPI.MediatorExample
{

    public class GetAllExampleItemsQuery : IRequest<List<ExampleItem>> { }

    public class GetAllExampleItemsQueryHandler : IRequestHandler<GetAllExampleItemsQuery, List<ExampleItem>>
    {
        private readonly TestContext Context;

        public GetAllExampleItemsQueryHandler(TestContext _context)
        {
            Context = _context;
        }

        public async Task<List<ExampleItem>> Handle(GetAllExampleItemsQuery request, CancellationToken cancellationToken)
        {
            var exampleItems = await Context.ExampleApis.ToListAsync();
            return exampleItems;
        }

    }
}
