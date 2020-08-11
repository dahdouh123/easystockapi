﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SmartRestaurant.API.Helpers;
using SmartRestaurant.API.Models.UserModels;
using SmartRestaurant.Application.Common.Enums;
using SmartRestaurant.Domain.Entities;
using SmartRestaurant.Infrastructure.Identity.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRestaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return Unauthorized();
            }
            var user = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
            var token = await TokenGenerator.Generate(user, _userManager, _configuration);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new { token, user.UserName, roles });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName
            };
            var result = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(false);
            return await GrantDinerRole(user, result);
        }

        [HttpPost("authenticateViaSocialMedia")]
        public async Task<IActionResult> AuthenticateViaSocialMedia(AuthenticateViaSocialMediaModel model)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName
                };
                var result = await _userManager.CreateAsync(user).ConfigureAwait(false);
                await GrantDinerRole(user, result);
            }
            var token = await TokenGenerator.Generate(user, _userManager, _configuration);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new { token, user.UserName, roles });
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (model.CurrentPassword.Equals(model.ConfirmNewPassword))
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.UserId);
                if (user == null)
                {
                    return Ok(HttpResponseHelper.Respond(ResponseType.NotFound));
                }
                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(HttpResponseHelper.Respond(ResponseType.OK));
                }
                return Ok(HttpResponseHelper.Respond(ResponseType.InternalServerError));
            }
            return Ok(HttpResponseHelper.Respond(ResponseType.BadRequest));
        }

        [HttpPost("forgetPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            //Send email
            return Ok(HttpResponseHelper.Respond(ResponseType.OK));
        }

        private async Task<IActionResult> GrantDinerRole(ApplicationUser user, IdentityResult result)
        {
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Diner.ToString());

                return Ok(HttpResponseHelper.Respond(ResponseType.OK));
            }
            return Ok(HttpResponseHelper.Respond(ResponseType.BadRequest));
        }
    }
}
