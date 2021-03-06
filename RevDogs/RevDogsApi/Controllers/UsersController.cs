﻿using Microsoft.AspNetCore.Http;
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
    public class UsersController : ControllerBase
    {
        private readonly IProject2Repository _userRepo;

        public UsersController(IProject2Repository userRepo)
        {
            _userRepo = userRepo;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Users>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _userRepo.GetUsersAsync();
            return Ok(users);
        }

        // GET: api/Users/1
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers(int id)
        {
            var users = await _userRepo.GetUsersAsync(id);

            if (users == null)
            {
                return NotFound("User with ID: " + id + "does not exist.");
            }

            return Ok(users);
        }

        // GET: api/Users/Login
        [HttpGet("login/{username}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Users>> GetUsersLogin(string username)
        {
            var users = await _userRepo.GetUserLoginAsync(username);

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // PUT: api/Users/1
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutUsers(int id, [FromBody] Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            var user = await _userRepo.PutUsersAsync(id, users);

            if (user is null)
            {
                return BadRequest("User with ID: " + id + " does not exist.");
            }
            return Ok("User " + user.UserName + " successfully updated");
        }

        // DELETE: api/Users/1
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var users = await _userRepo.RemoveUsersAsync(id);

            if (!users)
            {
                return BadRequest("User with ID: " + id + " doesn't exist.");
            }
            return Ok("User with ID: " + id + "has been removed from our system.");
        }

        // POST: api/Users
        [HttpPost]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateUserAsync(Users users)
        {
            var newUser = new Users
            {
                FirstName = users.FirstName,
                LastName = users.LastName,
                UserName = users.UserName
            };
            var addedUser = await _userRepo.PostUsersAsync(newUser);

            if (addedUser is null)
            {
                return BadRequest("An account with that username: " + users.UserName+ " already exist, please create a different one");
            }
            return Ok(users.UserName + ", Welcome to Revature Dogs!");
        }
    }
}
