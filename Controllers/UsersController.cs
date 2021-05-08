using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieUserManagerService.Data;
using MovieUserManagerService.Models;
using MovieUserManagerService.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;
using MovieUserManagerService.Services;
using MovieUserManagerService.Utils;

namespace MovieUserManagerService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagerServiceRepo _repo;
        private readonly IMapper _mapper;
        private readonly IUserAuthenticationService _auth;

        public UsersController(IUserManagerServiceRepo repo, IMapper mapper, IUserAuthenticationService auth)
        {
            _repo = repo;
            _mapper = mapper;
            _auth = auth;
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


        //DELETE api/users/{username}
        [HttpDelete("{username}")]
        public ActionResult DeleteUser(string username)
        {
            var targetUser = _repo.GetUserByUsername(username);
            if(targetUser == null)
            {
                return NotFound();
            }

            _repo.DeleteUser(targetUser);
            _repo.SaveChanges();

            return NoContent();
        }


        //POST api/users
        [HttpPost]
        public ActionResult <AuthenticationResult> CreateUser(UserCreateDto userCreateDto)
        {
            var userModel = _mapper.Map<User>(userCreateDto);
            if(_repo.GetUserByUsername(userModel.username) != null)
            {
                return BadRequest(new {error = ErrorMessages.userExists});
            }
            _repo.CreateUser(userModel);
            _repo.SaveChanges();

            //var userReadDto = _mapper.Map<UserReadDto>(userModel);
            //return CreatedAtRoute(nameof(GetUserByUsername), new {username = userReadDto.username}, userReadDto);

            return Ok(new AuthenticationResultSuccessDto{
                success = true,
                token = _auth.CreateToken(userModel)
            });
        }


        //POST api/users/register
        [HttpPost("/register")]
        public ActionResult <AuthenticationResult> UserRegister(UserCreateDto userCreateDto)
        {
            return CreateUser(userCreateDto);
        }


        //POST api/users/login
        // [HttpPost("/login")]
        // public ActionResult UserLogin(UserLoginDto userLoginDto)
        // {
        //     UserManager 
        //     var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret)));
        //     return Ok();
        // }
    }
}