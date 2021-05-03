using System.Collections.Generic;
using AutoMapper;
using MovieUserManagerService.Models;
using MovieUserManagerService.Read;

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