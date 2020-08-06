﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartRestaurant.API.Helpers;
using SmartRestaurant.API.Models;
using SmartRestaurant.Application.Common.Enums;
using SmartRestaurant.Domain.Entities;
using System.Threading.Tasks;

namespace SmartRestaurant.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userManager.Users;
            return Ok(users);
        }

        [HttpGet("GetById/{username}")]
        public async Task<IActionResult> GetById(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Ok(HttpResponseHelper.Respond(ResponseType.NotFound));
            }
            return Ok(user);
        }

        [Authorize(Roles = "SupportAgent,SuperAdmin")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ApplicationUserModel model)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            return CheckResultStatus(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(ApplicationUserModel model)
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(model);
            var result = await _userManager.UpdateAsync(user);
            return CheckResultStatus(result);
        }

        [Authorize(Roles = "SupportAgent,SuperAdmin")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var result = await _userManager.DeleteAsync(user);
            return CheckResultStatus(result);
        }

        [Authorize(Roles = "SupportAgent,SuperAdmin")]
        [HttpPost("Deactivate")]
        public async Task<IActionResult> Deactivate(string Id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            user.IsActive = false;
            var result = await _userManager.UpdateAsync(user);
            return CheckResultStatus(result);
        }

        [Authorize(Roles = "SupportAgent,SuperAdmin")]
        [HttpPost("Activate")]
        public async Task<IActionResult> Activate(string Id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(Id);
            user.IsActive = true;
            var result = await _userManager.UpdateAsync(user);
            return CheckResultStatus(result);
        }

        private IActionResult CheckResultStatus(IdentityResult result)
        {
            if (result.Succeeded)
            {
                return Ok(HttpResponseHelper.Respond(ResponseType.OK));
            }
            return Ok(HttpResponseHelper.Respond(ResponseType.InternalServerError));
        }
    }
}