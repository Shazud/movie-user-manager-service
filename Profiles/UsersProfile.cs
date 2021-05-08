using System.Collections.Generic;
using AutoMapper;
using MovieUserManagerService.Models;
using MovieUserManagerService.Dtos;

namespace MovieUserManagerService.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserUpdateDto>();
        }
    }
}