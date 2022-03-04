#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExampleAPI.Models;
using MediatR;
using ExampleAPI.MediatorExample;
using System.Net;

namespace ExampleAPI.Controllers
{
    [Route("api/ExampleItems")]
    [ApiController]
    public class ExampleItemsController : ControllerBase
    {
        private readonly TestContext _context;
        private readonly IMediator _mediator;

        public ExampleItemsController(TestContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;

        }

        // GET: api/ExampleItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExampleItem>>> GetExampleItems()
        {
            var exampleItems = await _mediator.Send(new GetAllExampleItemsQuery());
           
            return Ok(exampleItems);
        }

        // GET: api/ExampleItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExampleItem>> GetExampleItem(int id)
        {
            var exampleItem = await _mediator.Send(new GetExampleItemByIdQuery(id));
            return exampleItem != null ? Ok(exampleItem) : NotFound();
        }

        // PUT: api/ExampleItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExampleItem(long id, ExampleItem exampleItem)
        {
            if (id != exampleItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(exampleItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExampleItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ExampleItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExampleItem>> PostTodoItem(ExampleItem exampleItem)
        { 
            var command = new PostExampleItemCommand(exampleItem);
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetExampleItem), new { id = response.Id }, response);
        }

        // DELETE: api/ExampleItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExampleItem(long id)
        {
            var exampleItem = await _context.ExampleApis.FindAsync(id);
            if (exampleItem == null)
            {
                return NotFound();
            }

            _context.ExampleApis.Remove(exampleItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExampleItemExists(long id)
        {
            return _context.ExampleApis.Any(e => e.Id == id);
        }
    }
}
