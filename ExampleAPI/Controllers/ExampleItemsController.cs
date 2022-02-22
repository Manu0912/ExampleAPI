#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExampleAPI.Models;

namespace ExampleAPI.Controllers
{
    [Route("api/ExampleItems")]
    [ApiController]
    [Produces("application/json")]
    public class ExampleItemsController : ControllerBase
    {
        private readonly TestContext _context;

        public ExampleItemsController(TestContext context)
        {
            _context = context;
        }

        // GET: api/ExampleItems
        /// <summary>
        /// Get all ExampleItems
        /// </summary>
        /// <response code="200">Returns all ExampleItems</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExampleItem>>> GetExampleItems()
        {
            return await _context.ExampleApis.ToListAsync();
        }

        // GET: api/ExampleItems/5
        /// <summary>
        /// Get a specific ExampleItem.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Returns a specific ExampleItem</response>
        /// /// <response code="404">If specific item is not founded</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// <summary>
        /// Put a specific Example Item.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Success</response>
        /// <response code="404">If specific item is not founded</response>
        /// <response code="400">If the ExampleItem dont have all properties</response>

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=
        /// <summary>
        /// Post a Example Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST 
        ///     {
        ///        "name": "Example",
        ///        "isCompleted": true
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created ExampleItem</response>
        /// <response code="400">If the item is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExampleItem>> PostTodoItem(ExampleItem exampleItem)
        {
            _context.ExampleApis.Add(exampleItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExampleItem), new { id = exampleItem.Id }, exampleItem);
        }

        // DELETE: api/ExampleItems/5
        /// <summary>
        /// Delete a specific Example Item.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Success</response>
        /// /// <response code="404">If specific item is not founded</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
