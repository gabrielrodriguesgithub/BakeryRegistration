using AutoMapper;
using BakeryRegistration.Data.DTOs;
using BakeryRegistration.Models;

namespace BakeryRegistration.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, UserModel>();
        }
    }
}
