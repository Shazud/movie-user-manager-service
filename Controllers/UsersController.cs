using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MovieUserManagerService.Data;
using MovieUserManagerService.Models;

namespace MovieUserManagerService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagementServiceRepo _repo;

        public UsersController(IUserManagementServiceRepo repo)
        {
            _repo = repo;
        }

        //GET api/users
        [HttpGet]
        public ActionResult <IEnumerable<User>> GetAllUsers(){
            var users = _repo.GetAllUsers();
            return Ok(users);
        }

        //GET api/users/{id}
        [HttpGet("{id}")]
        public ActionResult <User> GetUserByUsername(string username){
            var user = _repo.GetUserByUsername(username);
            return Ok(user);
        }
    }
}