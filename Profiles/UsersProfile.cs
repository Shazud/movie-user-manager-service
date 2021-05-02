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
        }
    }
}