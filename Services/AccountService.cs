using AutoMapper;
using BlazorApp.Encryptor;
using BlazorApp.Models;
using DevExpress.Xpo;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Services
{
    public interface IAccountService
    {
        SignInModel User { get; }
        Task Login(SignInModel model);
        Task<string> GetItem();

        //Task<IQueryable<MovPackage>> Get();
    }
    
    public class AccountService : IAccountService
    {
        private readonly UnitOfWork _readUnitOfWork;
        private readonly IMapper _mapper;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "key";
        private string _access = "access_token";
        private readonly IEncryptor _IEncryptor;

        public SignInModel User { get; private set; }


        public AccountService(
            IMapper mapper,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            IEncryptor IEncryptor,
            UnitOfWork readUnitOfWork
        ) {
            _mapper = mapper;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _IEncryptor = IEncryptor;
            _readUnitOfWork = readUnitOfWork;

        }

        //public Task<IQueryable<MovPackage>> Get()
        //{

        //    Session MainSession = new Session();

        //    XPQuery<MovPackage> customersQuery = new XPQuery<MovPackage>(MainSession);

        //    var customers = from c in customersQuery
        //                    select c;


        //    foreach (MovPackage customer in customers)
        //        Console.WriteLine(customer.Id);

        //    var resul = _readUnitOfWork.Query<MovPackage>().Select(m => m.IdAfiliado);

        //    var query = (IQueryable<MovPackage>)_readUnitOfWork.Query<MovPackage>();

        //    return Task.FromResult(query);
        //}

        public async Task Login(SignInModel model)
        {

            string? UserEmail = model.Email;

            var email = await _localStorageService.GetItem<string>(_userKey);

            if (!string.IsNullOrEmpty(email)) {

                if (email.Contains("@")) 
                {
                    await _localStorageService.SetItemToken(_access, model.Token);
                    await _localStorageService.SetItem(_userKey, UserEmail);
                    return;
                }
                else
                {
                    var result = _IEncryptor.Decryption(email);

                    if (UserEmail != result)
                    {
                        await _localStorageService.SetItemToken(_access, model.Token);
                        await _localStorageService.SetItem(_userKey, UserEmail);
                    };
                }                
            }
            else
            {
                await _localStorageService.SetItem(_userKey, UserEmail);
                await _localStorageService.SetItemToken(_access, model.Token);
            } 

        }

        public async Task<string> GetItem()
        {
            var result =  await _localStorageService.GetItem<string>(_userKey);

            return result;
        }
        
    }
}