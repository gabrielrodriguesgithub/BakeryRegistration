using AutoMapper;
using BakeryRegistration.Data.DTOs;
using BakeryRegistration.Models;

namespace BakeryRegistration.Profiles
{
    public class BakeryProfile : Profile
    {
        public BakeryProfile()
        {
            CreateMap<CreateBakeryDto, BakeryModel>();
        }
    }
}
