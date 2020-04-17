using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevDogsApi.Models;

namespace RevDogsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly Project2Context _context;

        public DogsController(Project2Context context)
        {
            _context = context;
        }

        // GET: api/Dogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dogs>>> GetDogs()
        {
            return await _context.Dogs.ToListAsync();
        }

        // GET: api/Dogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dogs>> GetDogs(int id)
        {
            var dogs = await _context.Dogs.FindAsync(id);

            if (dogs == null)
            {
                return NotFound();
            }

            return dogs;
        }

        // PUT: api/Dogs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDogs(int id, Dogs dogs)
        {
            if (id != dogs.Id)
            {
                return BadRequest();
            }

            _context.Entry(dogs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogsExists(id))
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

        // POST: api/Dogs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dogs>> PostDogs(Dogs dogs)
        {
            _context.Dogs.Add(dogs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDogs", new { id = dogs.Id }, dogs);
        }

        // DELETE: api/Dogs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dogs>> DeleteDogs(int id)
        {
            var dogs = await _context.Dogs.FindAsync(id);
            if (dogs == null)
            {
                return NotFound();
            }

            _context.Dogs.Remove(dogs);
            await _context.SaveChangesAsync();

            return dogs;
        }

        private bool DogsExists(int id)
        {
            return _context.Dogs.Any(e => e.Id == id);
        }
    }
}
