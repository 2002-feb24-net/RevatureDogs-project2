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

            if (tricksProgresses is null)
            {
                return NotFound("TricksProgress with ID: " + id + " does not exist.");
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
            var exists = await _userRepo.PutTricksProgressAsync(id, tricksProgress);
            if (exists is null)
            {
                return BadRequest("TricksProgress with ID " + id + " does not exist.");
            }
            else
            {
                if (id != tricksProgress.Id)
                {
                    return BadRequest();
                }

                return Ok("Your pups Trick has been updated!");
            }
        }

        // DELETE: api/TricksProgress/1
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TricksProgress), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTricksProgressAsync(int id)
        {
            var results = await _userRepo.RemoveTricksProgressAsync(id);
            if (!results)
            {
                return BadRequest("TricksProgress with this ID " + id + " does not exist");
            }


            return Ok("TricksProgress successfully removed.");
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

            if (addedtrick is null)
            {
                return BadRequest("Your pup already knows this trick!");
            }
            else
            {
                return Ok("Your pup learned a new trick!");
            }
        }
    }
}
