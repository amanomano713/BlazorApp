using AutoMapper;
using BlazorApp.DataAcess.Infraestructure.Abstractions;
using BlazorApp.Entities.User;
using BlazorApp.Handlers.Commands;
using BlazorApp.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace BlazorApp.Features.Accounts.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IDataProtector _dataProtector;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AccountController(IDataProtectionProvider dataProtectionProvider, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, IUserDataRepository userDataRepository,
            IMapper mapper, IMediator mediator)
        {
            _dataProtector = dataProtectionProvider.CreateProtector("SignIn");
            _userManager = userManager;
            _signInManager = signInManager;
            _userDataRepository = userDataRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("account/createpackages")]
        public async Task<IActionResult> Createpackages(string parameter1, string parameter2)
        {
        
                var packageMontos = new List<PackageMontoDTO>()
                {
                    new PackageMontoDTO() { CodPackage = "Pack10", Monto = 10},
                    new PackageMontoDTO() { CodPackage = "Pack20", Monto = 20},
                    new PackageMontoDTO() { CodPackage = "Pack50", Monto = 50}

                };

#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var monto = packageMontos.FirstOrDefault(x => x.CodPackage == parameter1).Monto;
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.

            var packageDTO = new PackageDTO
            {
                Id = parameter2,
                CodPackage = parameter1,
                Monto = monto
            };

            var requestModel = _mapper.Map<CreatePackagesCommand>(packageDTO);

            var response = await _mediator.Send(requestModel);

            var Ok = 0;

            if (response!=null)
            {
                Ok = 1;
            }

            return this.Json(new { result = Ok });
        }

        [HttpGet("account/signinactual")]
        public async Task<IActionResult> SignInActual(String cadena)
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
    }
}
