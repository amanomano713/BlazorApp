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
            CreateMap<UpdateUserDataCommand, UserDTO>();
            CreateMap<UserDTO, UpdateUserDataCommand>();

            CreateMap<UserData, UserDTO>();
            CreateMap<UserDTO, UserData>();

            CreateMap<UserData, CreateUserDataCommand>();
            CreateMap<CreateUserDataCommand, UserData>();

            CreateMap<UserData, UpdateUserDataCommand>();
            CreateMap<UpdateUserDataCommand, UserData>();

            CreateMap<CreateUserDataCommand, UserDTO>();
            CreateMap<UserDTO, CreateUserDataCommand>();

            CreateMap<CreatePackagesCommand, PackageDTO>();
            CreateMap<PackageDTO, CreatePackagesCommand>();

            CreateMap<CreatePackagesCommand, Packages>();
            CreateMap<Packages, CreatePackagesCommand>();

            CreateMap<CreateWithdrawalCommand, Withdrawal>();
            CreateMap<Withdrawal, CreateWithdrawalCommand>();

            CreateMap<CreateTransferCommand, Transfer>();
            CreateMap<Transfer, CreateTransferCommand>();

            CreateMap<CreateTransferCommand, TransferDTO>();
            CreateMap<TransferDTO, CreateTransferCommand>();

        }
    }
}
