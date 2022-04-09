using AutoMapper;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using BlazorApp.Models;

namespace BlazorApp.DataAcess.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<UserData, UserDTO>();
            CreateMap<UserDTO, UserData>();

            CreateMap<CreatePackagesCommand, PackageDTO>();
            CreateMap<PackageDTO, CreatePackagesCommand>();

            CreateMap<CreatePackagesCommand, Packages>();
            CreateMap<Packages, CreatePackagesCommand>();

            CreateMap<CreatePujaCommand, PujaDTO>();
            CreateMap<PujaDTO, CreatePujaCommand>();

            CreateMap<CreatePujaCommand, Puja>();
            CreateMap<Puja, CreatePujaCommand>();

        }
    }
}
