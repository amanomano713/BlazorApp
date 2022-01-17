using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Features.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IDataProtector _dataProtector;
        private readonly IUserDataRepository _userDataRepository;

        public AccountController(IDataProtectionProvider dataProtectionProvider, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IUserDataRepository userDataRepository)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("SignIn");
            _userManager = userManager;
            _signInManager = signInManager;
            _userDataRepository = userDataRepository;
        }

        [HttpPost("account/CreateUserData")]
        public async Task<IActionResult> CreateUserData([FromBody] UserData user)
        {
            var result = _userDataRepository.Add(user);

            await _userDataRepository.UnitOfWork.SaveChangesAsync();

            return Redirect("/");
        }

        [HttpGet("account/signinactual")]
        public async Task<IActionResult> SignInActual(string cadena)
        {

            //string parsedGuid = System.Guid.Parse("39c3d33a-94df-4623-9318-b425be178606").ToString();

            //var result = _userDataRepository.GetAsync(parsedGuid).GetAwaiter().GetResult();

            //UserData userData = new UserData();

            //userData.Id = parsedGuid;

            //var result = _userDataRepository.Add(userData);

            //await _userDataRepository.UnitOfWork.SaveChangesAsync();

            var data = _dataProtector.Unprotect(cadena);

            var parts = data.Split('|');

            var identityUser = await _userManager.FindByIdAsync(parts[0]);

            var isTokenValid = await _userManager.VerifyUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "SignIn", parts[1]);

            if (isTokenValid)
            {
                await _signInManager.SignInAsync(identityUser, true);
                if (parts.Length == 3 && Url.IsLocalUrl(parts[2]))
                {
                    return Redirect(parts[2]);
                }

                return Redirect("/");
            }
            else
            {
                return Unauthorized("STOP!");
            }
        }

        [Authorize]
        [HttpGet("account/signout")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();

            return Redirect("/");
        }
    }
}
