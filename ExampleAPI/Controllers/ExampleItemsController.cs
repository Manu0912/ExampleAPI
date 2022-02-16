#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExampleAPI.Models;

namespace ExampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleItemsController : ControllerBase
    {
        private readonly ExampleContext _context;

        public ExampleItemsController(ExampleContext context)
        {
            _context = context;
        }

        // GET: api/ExampleItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExampleItem>>> GetExampleItems()
        {
            return await _context.ExampleItems.ToListAsync();
        }

        // GET: api/ExampleItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExampleItem>> GetExampleItem(long id)
        {
            var exampleItem = await _context.ExampleItems.FindAsync(id);

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

        // POST: api/ExampleItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExampleItem>> PostTodoItem(ExampleItem exampleItem)
        {
            _context.ExampleItems.Add(exampleItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(ExampleItem), new { id = exampleItem.Id }, exampleItem);
        }

        // DELETE: api/ExampleItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExampleItem(long id)
        {
            var exampleItem = await _context.ExampleItems.FindAsync(id);
            if (exampleItem == null)
            {
                return NotFound();
            }

            _context.ExampleItems.Remove(exampleItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExampleItemExists(long id)
        {
            return _context.ExampleItems.Any(e => e.Id == id);
        }
    }
}
