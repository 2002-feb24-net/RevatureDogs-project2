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
    public class DogsController : ControllerBase
    {
        private readonly IProject2Repository _userRepo;

        public DogsController(IProject2Repository userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: api/Dogs
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Dogs>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDogsAsync()
        {
            var dogs = await _userRepo.GetDogsAsync();
            return Ok(dogs);
        }

        // GET: api/Dogs/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Dogs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDogsAsync(int id)
        {
            var dogs = await _userRepo.GetDogsAsync(id);

            if (dogs == null)
            {
                return BadRequest("No dogs with ID: " + id + " in our system.");
            }

            return Ok(dogs);
        }

#region GetUserDogs
        //// GET: api/Dogs/1
        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(Dogs), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetUserDogsAsync(int id)
        //{
        //    var dogs = await _userRepo.GetDogsAsync(id);

        //    if (dogs == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(dogs);
        //}
#endregion

        // PUT: api/Dogs/1
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Dogs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutDogs(int id, [FromBody] Dogs dogs)
        {
            if (id != dogs.Id)
            {
                return BadRequest();
            }

            var updatedDog = await _userRepo.PutDogsAsync(id, dogs);
            if (updatedDog is null)
            {
                return BadRequest("The Dog with ID " + id + " exist, cannot update.");
            }
            else
            {
                return Ok(updatedDog);
            }
        }

        // DELETE: api/Dogs/1
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Dogs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDogsAsync(int id)
        {
            var result = await _userRepo.RemoveDogsAsync(id);
            if (result)
            {
                return Ok("Dog with ID " + id + " successfully removed");
            }
            else
            {
                return BadRequest("Dog with ID " + id + " does not exist.");
            }
        }

        // POST: api/Dogs
        [HttpPost]
        [ProducesResponseType(typeof(Dogs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateDogsAsync([FromBody] Dogs dogs)
        {
            var newDogs = new Dogs
            {
                DogTypeId = dogs.DogTypeId,
                UserId = dogs.UserId,
                PetName = dogs.PetName
            };
            var addedDog = await _userRepo.PostDogsAsync(newDogs);
            return Ok(addedDog);
        }
    }
}
