using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Db;
using _02_apis.Model;
using _02_apis.Service.Implementation;
using _02_apis.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace _02_apis.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGetAllUsers _getAllUsers;
        private readonly ILogger<UserController> _logger;
        public UserController(IGetAllUsers getAllUsers, ILogger<UserController> logger)
        {
            _getAllUsers = getAllUsers;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _getAllUsers.getAll();
            if (users == null || users.Count == 0)
            {
                _logger.LogWarning("No users found in the database.");
                return NotFound("No users found");
            }
            _logger.LogInformation($"Retrieved {users.Count} users from the database.");
            return Ok(users);
        }

        [HttpGet("/id")]
        public async Task<IActionResult> GetUserById([FromQuery] int id)
        {
            var user = await _getAllUsers.getOneById(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID = {id} not found.");
                return NotFound($"User with ID = {id} not found");
            }
            _logger.LogInformation($"Retrieved user with ID = {id} from the database.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            _logger.LogInformation("Creating a new user.");
            var createdUser = await _getAllUsers.CreateUser(newUser);
            _logger.LogInformation($"User with ID = {createdUser.Id} created successfully.");
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
    }
}