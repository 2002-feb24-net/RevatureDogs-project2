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
    public class TricksProgressController : ControllerBase
    {
        private readonly Project2Context _context;

        public TricksProgressController(Project2Context context)
        {
            _context = context;
        }

        // GET: api/TricksProgress
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TricksProgress>>> GetTricksProgress()
        {
            return await _context.TricksProgress.ToListAsync();
        }

        // GET: api/TricksProgress/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TricksProgress>> GetTricksProgress(int id)
        {
            var tricksProgress = await _context.TricksProgress.FindAsync(id);

            if (tricksProgress == null)
            {
                return NotFound();
            }

            return tricksProgress;
        }

        // PUT: api/TricksProgress/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTricksProgress(int id, TricksProgress tricksProgress)
        {
            if (id != tricksProgress.Id)
            {
                return BadRequest();
            }

            _context.Entry(tricksProgress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TricksProgressExists(id))
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

        // POST: api/TricksProgress
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TricksProgress>> PostTricksProgress(TricksProgress tricksProgress)
        {
            _context.TricksProgress.Add(tricksProgress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTricksProgress", new { id = tricksProgress.Id }, tricksProgress);
        }

        // DELETE: api/TricksProgress/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TricksProgress>> DeleteTricksProgress(int id)
        {
            var tricksProgress = await _context.TricksProgress.FindAsync(id);
            if (tricksProgress == null)
            {
                return NotFound();
            }

            _context.TricksProgress.Remove(tricksProgress);
            await _context.SaveChangesAsync();

            return tricksProgress;
        }

        private bool TricksProgressExists(int id)
        {
            return _context.TricksProgress.Any(e => e.Id == id);
        }
    }
}
