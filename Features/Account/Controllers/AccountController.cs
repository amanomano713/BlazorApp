using AutoMapper;
using BlazorApp.Encryptor;
using BlazorApp.Handlers.Commands;
using BlazorApp.Messages;
using BlazorApp.Models;
using BlazorApp.Services;
using MediatR;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILocalStorageService _localStorageService;
        private readonly IEncryptor _IEncryptor;

        public AccountController(IDataProtectionProvider dataProtectionProvider,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper, IMediator mediator,
            ILocalStorageService localStorageService,
            IEncryptor IEncryptor)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("SignIn");
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _mediator = mediator;
            _localStorageService = localStorageService;
            _IEncryptor = IEncryptor;
        }

        /// <summary>
        /// validate token
        /// </summary>
        /// <returns></returns>
        private async Task<bool> validatetoken()
        {
           
            Request.Headers.TryGetValue("Authorization", out var Bearer);

            var token = Bearer.ToString().Replace("Bearer", string.Empty).Replace("key", string.Empty);

            var parts = token.Split('|');

            var KeyEmail = _IEncryptor.Decryption(parts[1]);

            var identityUser = await _userManager.FindByEmailAsync(KeyEmail);

            var isTokenValid = await _userManager.VerifyUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "SignIn", parts[0]);

            return isTokenValid;
        }



        [HttpPost("account/createpuja")]
        public async Task<IActionResult> CreatePuja(string param1)
        {
            var val = await validatetoken();

            //var val = true;

            var Ok = 0;

            if (val == true)
            {
                var data = _IEncryptor.Decryption(param1);

                var parts = data.Split('|');

                string? MontoPuja= parts[2].Replace("_", string.Empty);

                var monto = System.Convert.ToInt64(MontoPuja);

                var pujaDTO = new PujaDTO
                {
                    IdAfiliado= parts[0],
                    IdPuja = parts[1],
                    Monto = monto
                };

                var requestModel = _mapper.Map<CreatePujaCommand>(pujaDTO);

                var response = await _mediator.Send(requestModel);

                if (response != null)
                {
                    Ok = 1;
                }
            }
            else
            {
                Ok = 2;
            }

            return this.Json(new { result = Ok });
        }



        [HttpPost("account/createretiro")]
        public async Task<IActionResult> CreateRetiro(string param1)
        {

            var val = await validatetoken();

            var Ok = 0;

            if (val == true)
            {
                var data = _IEncryptor.Decryption(param1);

                var parts = data.Split('|');

                var Withdrawal = System.Convert.ToInt64(parts[2]);

                var withdrawalDTO = new WithdrawalDTO
                {
                    Id = parts[0],
                    Wallet = parts[1],
                    Monto = Withdrawal
                };

                var requestModel = _mapper.Map<CreateWithdrawalCommand>(withdrawalDTO);

                var response = await _mediator.Send(requestModel);

                if (response != null)
                {
                    Ok = 1;
                }

            }
            else
            {
                Ok = 2;
            }

            return this.Json(new { result = Ok });
        }


        [HttpPost("account/createtransfer")]
        public async Task<IActionResult> CreateTransfer(string param1)
        {

            var val = await validatetoken();

            var Ok = 0;

            if (val == true)
            {

                var data = _IEncryptor.Decryption(param1);

                var parts = data.Split('|');

                var transferDTO = new TransferDTO
                {
                    Id = parts[0],
                    Afiliado = parts[1],
                    Monto = System.Convert.ToInt32(parts[2])
                };

                var requestModel = _mapper.Map<CreateTransferCommand>(transferDTO);

                var response = await _mediator.Send(requestModel);

                if (response != null)
                {
                    Ok = 1;
                }
            }
            else
            {
                Ok = 2;
            }

            return this.Json(new { result = Ok });
        }

        [HttpPost("account/createpackages")]
        public async Task<IActionResult> Createpackages(string parameter1)
        {
            var val = await validatetoken();

            var Ok = 0;

            if (val == true)
            {

                var data = _IEncryptor.Decryption(parameter1);

                var parts = data.Split('|');

                var packageMontos = new List<PackageMontoDTO>()
                {
                    new PackageMontoDTO() { CodPackage = "Pack10", Monto = 10},
                    new PackageMontoDTO() { CodPackage = "Pack20", Monto = 20},
                    new PackageMontoDTO() { CodPackage = "Pack50", Monto = 50},
                    new PackageMontoDTO() { CodPackage = "Pack100", Monto = 100},
                    new PackageMontoDTO() { CodPackage = "Pack200", Monto = 200},
                    new PackageMontoDTO() { CodPackage = "Pack500", Monto = 500},
                    new PackageMontoDTO() { CodPackage = "Pack1000", Monto = 1000},
                    new PackageMontoDTO() { CodPackage = "Pack1500", Monto = 1500},
                };

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
                var monto = packageMontos.FirstOrDefault(x => x.CodPackage == parts[1]).Monto;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

                var packageDTO = new PackageDTO
                {
                    IdAfiliado = parts[0],
                    CodPackage = parts[1],
                    Monto = monto
                };

                try
                {

                    var requestModel = _mapper.Map<CreatePackagesCommand>(packageDTO);

                    var response = await _mediator.Send(requestModel);

                    if (response != null)
                    {
                        Ok = 1;
                    }
                }
                catch (Exception e)
                {
                    Ok = 3;
                }
            }
            else
            {
                Ok = 2;
            }

            return this.Json(new { result = Ok });
        }

        [HttpGet("account/signinactual")]
        public async Task<IActionResult> SignInActual(string cadena)
        {


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

        [HttpGet("account/index")]
        public async Task<IActionResult> Index()
        {
            return Redirect("/");
        }
    }
}
