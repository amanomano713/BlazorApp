using AutoMapper;
using BlazorApp.Entities.User;
using BlazorApp.Models;

namespace BlazorApp.DataAcess.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserData,UserDTO>();
            CreateMap<UserDTO, UserData>();
        }
    }
}
