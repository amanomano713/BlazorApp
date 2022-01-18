using AutoMapper;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Services
{
    public interface IAccountService
    {
        SignInModel User { get; }
        Task Login(SignInModel model);
        Task<String> GetItem();

    }

    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "key";


        public SignInModel User { get; private set; }


        public AccountService(
            IMapper mapper,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        ) {
            _mapper = mapper;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;

        }


        public async Task Login(SignInModel model)
        {
            string? User = model.Email;
            var email = await _localStorageService.GetItem<String>(_userKey);
            if (User == email) return;
            await _localStorageService.SetItem(_userKey, User);
        }

        public async Task<String> GetItem()
        {
           var result =  await _localStorageService.GetItem<String>(_userKey);

            return result;
        }
        
    }
}