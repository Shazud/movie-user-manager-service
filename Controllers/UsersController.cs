using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieUserManagerService.Data;
using MovieUserManagerService.Models;
using MovieUserManagerService.Read;

namespace MovieUserManagerService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagerServiceRepo _repo;
        private readonly IMapper _mapper;

        public UsersController(IUserManagerServiceRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //GET api/users
        [HttpGet]
        public ActionResult <IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = _repo.GetAllUsers();
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
        }

        //GET api/users/{username}
        [HttpGet("{username}", Name="GetUserByUsername")]
        public ActionResult <UserReadDto> GetUserByUsername(string username)
        {
            var user = _repo.GetUserByUsername(username);
            return user != null ? Ok(_mapper.Map<UserReadDto>(user)) : NotFound();
        }

        //POST api/users
        [HttpPost]
        public ActionResult <UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            _repo.CreateUser(userModel);
            _repo.SaveChanges();
            var userReadDto = _mapper.Map<UserReadDto>(userModel);
            return CreatedAtRoute(nameof(GetUserByUsername), new {username = userReadDto.username}, userReadDto);
        }

        //PUT api/users/{username}
        [HttpPut("{username}")]
        public ActionResult UpdateUser(string username, UserUpdateDto userUpdateDto)
        {
            var targetUser = _repo.GetUserByUsername(username);
            if(targetUser == null)
            {
                return NotFound();
            }

            _mapper.Map(userUpdateDto, targetUser);
            _repo.UpdateUser(targetUser);
            _repo.SaveChanges();

            return NoContent();
        }

        //PATCH api/users/{username}
        [HttpPatch("{username}")]
        public ActionResult PatchUser(string username, JsonPatchDocument<UserUpdateDto> patchDocument)
        {
            var targetUser = _repo.GetUserByUsername(username);
            if(targetUser == null)
            {
                return NotFound();
            }

            var userUpdateModel = _mapper.Map<UserUpdateDto>(targetUser);
            userUpdateModel.passwordConfirmation = userUpdateModel.password;

            patchDocument.ApplyTo(userUpdateModel, ModelState);
            if(!TryValidateModel(userUpdateModel))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(userUpdateModel, targetUser);
            _repo.UpdateUser(targetUser);
            _repo.SaveChanges();

            return NoContent();
        }
    }
}