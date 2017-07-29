using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;
using TechTime.ViewModels.Api;

namespace TechTime.Controllers.Api
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private IRecordRepository _repo;

        public UsersController(IRecordRepository repo)
        {
            _repo = repo;
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] AddUserViewModel user)
        {
            await _repo.AddUser(user.Username, user.Password, user.Email);

            if (await _repo.SaveChangesAsync())
                return Created("User added sucessfully", user.Username);

            return BadRequest("Failed to save User to database.");
        }
    }
}
