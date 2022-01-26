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
            Encryptor.encriptador clave = new BlazorApp.Encryptor.encriptador();

            string? UserEmail = model.Email;

            var email = await _localStorageService.GetItem<string>(_userKey);

            if (!string.IsNullOrEmpty(email)) {

                if (email.Contains("@")) 
                {
                    await _localStorageService.SetItem(_userKey, UserEmail);
                    return;
                }
                else
                {
                    var result = clave.DesEncriptacion(email);

                    if (UserEmail != result)
                    {

                        await _localStorageService.SetItem(_userKey, UserEmail);
                    };
                }                
            }
            else
            {

                await _localStorageService.SetItem(_userKey, UserEmail);
            } 

        }

        public async Task<string> GetItem()
        {
            var result =  await _localStorageService.GetItem<string>(_userKey);

            return result;
        }
        
    }
}