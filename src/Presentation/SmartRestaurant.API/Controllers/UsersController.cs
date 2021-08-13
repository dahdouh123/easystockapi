﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartRestaurant.API.Helpers;
using SmartRestaurant.API.Models.UserModels;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Enums;
using SmartRestaurant.Application.Common.Exceptions;
using SmartRestaurant.Application.Common.Extensions;
using SmartRestaurant.Application.Common.Interfaces;
using SmartRestaurant.Application.Users.Queries;
using SmartRestaurant.Domain.Identity.Entities;

namespace SmartRestaurant.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ApiController
    {
        private readonly IIdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager, IEmailSender emailSender,
            IIdentityContext identityContext) :
            base(emailSender)
        {
            _userManager = userManager;
            _identityContext = identityContext;
        }

        [Authorize(Roles = "SuperAdmin,SupportAgent")]
        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            var result = _userManager.Users.GetPaged(page, pageSize);
            var pagedResult = await GetPagedListOfUsers(result).ConfigureAwait(false);

            return Ok(pagedResult);
        }

        [Route("role/{role}")]
        [Authorize(Roles = "SuperAdmin,SupportAgent")]
        [HttpGet]
        public IActionResult GetAllByRole([FromRoute] string role, int page, int pageSize)
        {
            var result = _identityContext.UserRoles.Include(u => u.Role)
                .Where(u => u.Role.Name == role).Select(u => u.User).GetPaged(page, pageSize);

            return Ok(result);
        }

        [Route("{userId}")]
        [Authorize(Roles = "SuperAdmin,SupportAgent")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId).ConfigureAwait(false);
            if (user == null)
                throw new NotFoundException(nameof(user), userId);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            return Ok(new UserWithRolesModel(user, (roles == null) ? new string[0] : roles.ToArray()));
        }

        private async Task<PagedListDto<UserWithRolesModel>> GetPagedListOfUsers(
            PagedResultBase<ApplicationUser> result)
        {
            var userModels = new List<UserWithRolesModel>();
            foreach (var user in result.Data)
            {
                var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                userModels.Add(new UserWithRolesModel(user, roles.ToArray()));
            }

            var pagedResult = new PagedListDto<UserWithRolesModel>(result.CurrentPage, result.PageCount,
                result.PageSize,
                result.RowCount, userModels);
            return pagedResult;
        }

        [Route("foodBusiness/staff")]
        [Authorize(Roles = "FoodBusinessAdministrator, FoodBusinessManager")]
        [HttpGet]
        public async Task<IActionResult> GetStaff(string foodBusinessId, int page, int pageSize)
        {
            var query = new GetFoodBusinessEmployeesQuery {
                FoodBusinessId = foodBusinessId,
                Page = page,
                PageSize = pageSize
            };
            return await SendWithErrorsHandlingAsync(query);
        }

        [Route("organization/foodBusinessManagers")]
        [Authorize(Roles = "FoodBusinessAdministrator")]
        [HttpGet]
        public async Task<IActionResult> GetFoodBusinessManagersWithinOrganization(int page, int pageSize)
        {
            var query = new GetFoodBusinessManagersWithinOrganizationQuery
            {
                Page = page,
                PageSize = pageSize
            };
            return await SendWithErrorsHandlingAsync(query);
        }

        [Authorize(Roles = "SupportAgent,SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUserModel model)
        {
            if (SuperAdminCheck(model.Roles)) return BadRequest();
            var user = new ApplicationUser(model.FullName, model.Email, model.UserName);
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return CheckResultStatus(result);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await SendEmailConfirmation(user, token);
            foreach (var role in model.Roles) await _userManager.AddToRoleAsync(user, role).ConfigureAwait(false);

            return CheckResultStatus(result);
        }

        [Route("{id}")]
        [Authorize(Roles = "SuperAdmin,SupportAgent")]
        [HttpPut]
        public async Task<IActionResult> Update([FromRoute] string id, ApplicationUserModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new NotFoundException(nameof(user), id);
            user.FullName = model.FullName;
            user.Email = model.Email;
            user.UserName = model.UserName;
            var result = await _userManager.UpdateAsync(user);
            return CheckResultStatus(result);
        }

        [Authorize(Roles = "SupportAgent,SuperAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            if (SuperAdminCheck(roles)) return BadRequest();
            var result = await _userManager.DeleteAsync(user);
            return CheckResultStatus(result);
        }

        [Authorize(Roles = "SupportAgent,SuperAdmin")]
        [HttpPatch("disable")]
        public async Task<IActionResult> Disable(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            return CheckResultStatus(result);
        }

        [Authorize(Roles = "SupportAgent,SuperAdmin")]
        [HttpPatch("enable")]
        public async Task<IActionResult> Enable(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = true;
            var result = await _userManager.UpdateAsync(user);
            return CheckResultStatus(result);
        }

        private static bool SuperAdminCheck(IEnumerable<string> roles)
        {
            return roles.Contains("SuperAdmin", StringComparer.OrdinalIgnoreCase);
        }

        private IActionResult CheckResultStatus(IdentityResult result)
        {
            return Ok(result.Succeeded
                ? HttpResponseHelper.Respond(ResponseType.OK)
                : HttpResponseHelper.Respond(ResponseType.InternalServerError));
        }
    }
}