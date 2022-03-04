#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExampleAPI.Models;
using AutoMapper;

namespace ExampleAPI.Controllers
{
    [Route("api/ExampleItems")]
    [ApiController]
    public class ExampleItemsController : ControllerBase
    {
        private readonly TestContext _context;
        private readonly IMapper _mapper;

        public ExampleItemsController(TestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ExampleItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExampleItem>>> GetExampleItems()
        {
            return await _context.ExampleApis.ToListAsync();
        }

        // GET: api/ExampleItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExampleItem>> GetExampleItem(long id)
        {
            var exampleItem = await _context.ExampleApis.FindAsync(id);

            if (exampleItem == null)
            {
                return NotFound();
            }

            return exampleItem;
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


        [HttpPost]
        public async Task<IActionResult> PostTodoItem([FromBody] ExampleItemDTO exampleItem)
        {
            var _mappedExampleItem = _mapper.Map<ExampleItem>(exampleItem);

            _context.ExampleApis.Add(_mappedExampleItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExampleItem), new { id = _mappedExampleItem.Id }, exampleItem); // return a 201
        }

      
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
