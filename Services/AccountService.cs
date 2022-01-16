using BlazorApp.Models;
using BlazorApp.Models.Account;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Services
{
    public interface IAccountService
    {
        SignInModel User { get; }
        Task Initialize();
        Task Login(SignInModel model);
        Task Logout();
        Task Register(AddUser model);
        Task<IList<SignInModel>> GetAll();
        Task<String> GetItem();
        Task Update(string id, EditUser model);
        Task Delete(string id);
    }

    public class AccountService : IAccountService
    {
        //private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "key";

        public SignInModel User { get; private set; }

        //SignInModel IAccountService.User => throw new NotImplementedException();

        public AccountService(
            //IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService

        ) {
            //_httpService = httpService;
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

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task Register(AddUser model)
        {
            throw new NotImplementedException();
        }

        public Task<IList<SignInModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<String> GetItem()
        {
           var result =  await _localStorageService.GetItem<String>(_userKey);

            return result;
        }

        public Task Update(string id, EditUser model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task Initialize()
        {
            throw new NotImplementedException();
        }

        //public async Task Logout()
        //{
        //    User = null;
        //    await _localStorageService.RemoveItem(_userKey);
        //    _navigationManager.NavigateTo("account/login");
        //}

        //public async Task Register(AddUser model)
        //{
        //    await _httpService.Post("/users/register", model);
        //}

        //public async Task<IList<User>> GetAll()
        //{
        //    return await _httpService.Get<IList<User>>("/users");
        //}

        //public async Task<User> GetById(string id)
        //{
        //    return await _httpService.Get<User>($"/users/{id}");
        //}

        //public async Task Update(string id, EditUser model)
        //{
        //    await _httpService.Put($"/users/{id}", model);

        //    // update stored user if the logged in user updated their own record
        //    if (id == User.Id) 
        //    {
        //        // update local storage
        //        User.FirstName = model.FirstName;
        //        User.LastName = model.LastName;
        //        User.Username = model.Username;
        //        await _localStorageService.SetItem(_userKey, User);
        //    }
        //}

        //public async Task Delete(string id)
        //{
        //    await _httpService.Delete($"/users/{id}");

        //    // auto logout if the logged in user deleted their own record
        //    if (id == User.Id)
        //        await Logout();
        //}
    }
}