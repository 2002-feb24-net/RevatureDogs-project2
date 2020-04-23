using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RevDogs.Core.Interfaces;
using RevDogs.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevDogsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogTypesController : ControllerBase
    {
        private readonly IProject2Repository _userRepo;

        public DogTypesController(IProject2Repository userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: api/DogTypes
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DogTypes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDogTypesAsync()
        {
            var dogTypes = await _userRepo.GetDogTypesAsync();
            return Ok(dogTypes);
        }
    }
}
