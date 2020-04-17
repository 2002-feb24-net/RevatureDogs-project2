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
    public class DogTypesController : ControllerBase
    {
        private readonly Project2Context _context;

        public DogTypesController(Project2Context context)
        {
            _context = context;
        }

        // GET: api/DogTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DogTypes>>> GetDogTypes()
        {
            return await _context.DogTypes.ToListAsync();
        }

        // GET: api/DogTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DogTypes>> GetDogTypes(int id)
        {
            var dogTypes = await _context.DogTypes.FindAsync(id);

            if (dogTypes == null)
            {
                return NotFound();
            }

            return dogTypes;
        }

        // PUT: api/DogTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDogTypes(int id, DogTypes dogTypes)
        {
            if (id != dogTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(dogTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DogTypesExists(id))
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

        // POST: api/DogTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DogTypes>> PostDogTypes(DogTypes dogTypes)
        {
            _context.DogTypes.Add(dogTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDogTypes", new { id = dogTypes.Id }, dogTypes);
        }

        // DELETE: api/DogTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DogTypes>> DeleteDogTypes(int id)
        {
            var dogTypes = await _context.DogTypes.FindAsync(id);
            if (dogTypes == null)
            {
                return NotFound();
            }

            _context.DogTypes.Remove(dogTypes);
            await _context.SaveChangesAsync();

            return dogTypes;
        }

        private bool DogTypesExists(int id)
        {
            return _context.DogTypes.Any(e => e.Id == id);
        }
    }
}
