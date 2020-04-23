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
    public class TricksController : ControllerBase
    {
        private readonly IProject2Repository _userRepo;

        public TricksController(IProject2Repository userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: api/Tricks
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Tricks>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTricksAsync()
        {
            var tricks = await _userRepo.GetTricksAsync();
            return Ok(tricks);
        }
    }
}
