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
    public class TricksController : ControllerBase
    {
        private readonly Project2Context _context;

        public TricksController(Project2Context context)
        {
            _context = context;
        }

        // GET: api/Tricks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tricks>>> GetTricks()
        {
            return await _context.Tricks.ToListAsync();
        }

        // GET: api/Tricks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tricks>> GetTricks(int id)
        {
            var tricks = await _context.Tricks.FindAsync(id);

            if (tricks == null)
            {
                return NotFound();
            }

            return tricks;
        }

        // // PUT: api/Tricks/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutTricks(int id, Tricks tricks)
        // {
        //     if (id != tricks.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(tricks).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TricksExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // // POST: api/Tricks
        // // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        // [HttpPost]
        // public async Task<ActionResult<Tricks>> PostTricks(Tricks tricks)
        // {
        //     _context.Tricks.Add(tricks);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetTricks", new { id = tricks.Id }, tricks);
        // }

        // // DELETE: api/Tricks/5
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Tricks>> DeleteTricks(int id)
        // {
        //     var tricks = await _context.Tricks.FindAsync(id);
        //     if (tricks == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.Tricks.Remove(tricks);
        //     await _context.SaveChangesAsync();

        //     return tricks;
        // }

        private bool TricksExists(int id)
        {
            return _context.Tricks.Any(e => e.Id == id);
        }
    }
}
