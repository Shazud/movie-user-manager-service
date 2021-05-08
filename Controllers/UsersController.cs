using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MovieUserManagerService.Data;
using MovieUserManagerService.Models;
using MovieUserManagerService.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Claims;

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
                return BadRequest(new {error = "User already exists!"});
            }
            _repo.CreateUser(userModel);
            _repo.SaveChanges();

            //var userReadDto = _mapper.Map<UserReadDto>(userModel);
            //return CreatedAtRoute(nameof(GetUserByUsername), new {username = userReadDto.username}, userReadDto);


            var key = Encoding.ASCII.GetBytes("TODO:weNeedToChangeThisKeyLater.");
            var authenticationResult = new AuthenticationResultSuccessDto();

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []{
                    new Claim(JwtRegisteredClaimNames.Sub, userModel.username),
                    new Claim("id", userModel.username),
                    new Claim("isAdmin", userModel.isAdmin.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            
            //authenticationResult.token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(securityTokenDescriptor);
            authenticationResult.token = tokenHandler.WriteToken(token);
            authenticationResult.success = true;
            return Ok(authenticationResult);
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