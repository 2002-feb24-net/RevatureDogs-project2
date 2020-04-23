using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevDogs.Core.Interfaces;
using RevDogs.Core.Model;
using RevDogs.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevDogsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TricksProgressController : ControllerBase
    {
        private readonly IProject2Repository _userRepo;

        public TricksProgressController(IProject2Repository userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: api/TricksProgress/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TricksProgress), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTricksProgress(int id)
        {
            var tricksProgresses = await _userRepo.GetTricksProgressAsync(id);

            if (tricksProgresses == null)
            {
                return NotFound();
            }

            return Ok(tricksProgresses);
        }

        // PUT: api/TricksProgress/1
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TricksProgress), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutTricksProgress(int id, TricksProgress tricksProgress)
        {
            if (id != tricksProgress.Id)
            {
                return BadRequest();
            }

            await _userRepo.PutTricksProgressAsync(id, tricksProgress);
            var updatedTrick = await _userRepo.GetTricksProgressByIdAsync(id);
            return Ok(updatedTrick);
        }

        // DELETE: api/TricksProgress/1
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TricksProgress), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTricksProgressAsync(int id)
        {
            var tricksProgresses = await _userRepo.GetTricksProgressAsync(id);
            if (tricksProgresses == null)
            {
                return NotFound();
            }

            await _userRepo.RemoveTricksProgressAsync(id);

            return Ok(tricksProgresses);
        }

        // POST: api/TricksProgress
        [HttpPost]
        [ProducesResponseType(typeof(TricksProgress), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTricksProgressAsync(TricksProgress tricksProgresses)
        {
            var newTricksProgress = new TricksProgress
            {
                PetId = tricksProgresses.PetId,
                TrickId = tricksProgresses.TrickId
            };
            var addedtrick = await _userRepo.PostTricksProgressAsync(newTricksProgress);

            return Ok(addedtrick);
        }
    }
}
