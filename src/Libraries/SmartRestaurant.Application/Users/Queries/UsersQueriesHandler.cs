﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Exceptions;
using SmartRestaurant.Application.Common.Extensions;
using SmartRestaurant.Application.Common.Interfaces;
using SmartRestaurant.Application.Common.Tools;
using SmartRestaurant.Domain.Identity.Entities;
using SmartRestaurant.Domain.Identity.Enums;

namespace SmartRestaurant.Application.Users.Queries
{
    public class UsersQueriesHandler :
        IRequestHandler<GetFoodBusinessEmployeesQuery, PagedListDto<FoodBusinessEmployeesDtos>>,
        IRequestHandler<GetFoodBusinessManagersWithinOrganizationQuery, PagedListDto<FoodBusinessManagersDto>>,
        IRequestHandler<GetHotelsManagersWithinOrganizationQuery, PagedListDto<HotelsManagersDto>>,

        IRequestHandler<GetUsersByRoleQuery, PagedListDto<ApplicationUserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityContext _identityContext;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;

        public UsersQueriesHandler(IApplicationDbContext context, IMapper mapper, IIdentityContext identityContext,
            IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _identityContext = identityContext;
            _userService = userService;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
        }

        #region Get FoodBusiness employees
        public async Task<PagedListDto<FoodBusinessEmployeesDtos>> Handle(GetFoodBusinessEmployeesQuery request,
            CancellationToken cancellationToken)
        {
            var validator = new GetFoodBusinessEmployeesValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid)
                throw new ValidationException(result);

            var roles = _userService.GetRoles();
            if (roles == null)
                throw new InvalidOperationException("User role shouldn't be null or  empty");

            var foodBusinesses = await _context.FoodBusinesses.FindAsync(Guid.Parse(request.FoodBusinessId))
                .ConfigureAwait(false);
            if (foodBusinesses == null)
                throw new NotFoundException(nameof(FoodBusiness), request.FoodBusinessId);

            var usersIdsList = _context.FoodBusinessUsers
                .Where(foodBusinessUsers => foodBusinessUsers.FoodBusinessId == Guid.Parse(request.FoodBusinessId))
                .Select(foodBusinessUsers => foodBusinessUsers.ApplicationUserId).ToList();

            PagedResultBase<ApplicationUser> pagedUsersList = null;
            if (roles.Contains(Roles.FoodBusinessManager.ToString()) || roles.Contains(Roles.FoodBusinessAdministrator.ToString()))
            {
                var employeesRoles = new List<string>
                    {Roles.Cashier.ToString(), Roles.Waiter.ToString(), Roles.Chef.ToString()};
                pagedUsersList = _identityContext.UserRoles.Include(u => u.Role)
                    .Where(u => employeesRoles.Contains(u.Role.Name) && usersIdsList.Contains(u.User.Id))
                    .Select(u => u.User)
                    .GetPaged(request.Page, request.PageSize);
            }else if( roles.Contains(Roles.SuperAdmin.ToString()) || roles.Contains(Roles.SupportAgent.ToString()))
            {
                var employeesRoles = new List<string>
                    {Roles.Cashier.ToString(), Roles.Waiter.ToString(), Roles.Chef.ToString(), Roles.FoodBusinessManager.ToString()};
                pagedUsersList = _identityContext.UserRoles.Include(u => u.Role)
                    .Where(u => employeesRoles.Contains(u.Role.Name) && usersIdsList.Contains(u.User.Id))
                    .Select(u => u.User)
                    .GetPaged(request.Page, request.PageSize);
            }

            var data = _mapper.Map<List<FoodBusinessEmployeesDtos>>(await pagedUsersList.Data
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false));

            foreach (var user in pagedUsersList.Data)
            {
                var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                var foodBusinessEmployees =
                    data.FirstOrDefault(FoodBusinessEmployees => FoodBusinessEmployees.Id == user.Id);
                foodBusinessEmployees.Roles = (List<string>) userRoles;
            }

            var pagedFoodBusinessEmployees = new PagedListDto<FoodBusinessEmployeesDtos>(pagedUsersList.CurrentPage,
                pagedUsersList.PageCount, pagedUsersList.PageSize, pagedUsersList.RowCount, data);
            return pagedFoodBusinessEmployees;
        }
        #endregion

        #region Get FoodBusinessManagers within organization
        public async Task<PagedListDto<FoodBusinessManagersDto>> Handle(
            GetFoodBusinessManagersWithinOrganizationQuery request, CancellationToken cancellationToken)
        {
            await ChecksHelper
                .CheckValidation_ThrowExceptionIfQueryIsInvalid<GetFoodBusinessManagersWithinOrganizationValidator,
                    GetFoodBusinessManagersWithinOrganizationQuery>(request, cancellationToken).ConfigureAwait(false);
            var foodBusinessAdministratorId =
                ChecksHelper.GetUserIdFromToken_ThrowExceptionIfUserIdIsNullOrEmpty(_userService);
            await ChecksHelper
                .CheckUserExistence_ThrowExceptionIfUserNotFound(_identityContext, foodBusinessAdministratorId)
                .ConfigureAwait(false);

            var listOfUsersIds = GetUsersIdsByFoodBusinessAdministratorId(foodBusinessAdministratorId);
            var pagedUsersList = GetPagedUsersByRolesAndUsersIds(Roles.FoodBusinessManager.ToString(), listOfUsersIds,
                request.Page, request.PageSize);

            var userList = await pagedUsersList.Data.ToListAsync(cancellationToken).ConfigureAwait(false);
            var foodBusinessManagersDto = _mapper.Map<List<FoodBusinessManagersDto>>(userList);

            await AppendRolesToListOfFoodBusinessManagers(pagedUsersList, foodBusinessManagersDto)
                .ConfigureAwait(false);
            await AppendFoodBusinessesToListOfFoodBusinessManagers(pagedUsersList, foodBusinessManagersDto)
                .ConfigureAwait(false);
            return ConstructPagedFoodBusinessManagers(pagedUsersList, foodBusinessManagersDto);
        }

        private List<string> GetUsersIdsByFoodBusinessAdministratorId(string foodBusinessAdministratorId)
        {
            return _context.FoodBusinessUsers.Include(foodBusinessUsers => foodBusinessUsers.FoodBusiness)
                .Where(foodBusinessUsers => foodBusinessUsers.FoodBusiness.FoodBusinessAdministratorId ==
                                            foodBusinessAdministratorId)
                .Select(foodBusinesses => foodBusinesses.ApplicationUserId)
                .ToList();
        }

        private PagedResultBase<ApplicationUser> GetPagedUsersByRolesAndUsersIds(string role,
            List<string> listOfUsersIds, int page, int pageSize)
        {
            return _identityContext.UserRoles.Include(u => u.Role)
                .Where(u => u.Role.Name == role && listOfUsersIds.Contains(u.User.Id))
                .Select(u => u.User)
                .GetPaged(page, pageSize);
        }

        private static PagedListDto<FoodBusinessManagersDto> ConstructPagedFoodBusinessManagers(
            PagedResultBase<ApplicationUser> pagedUsersList, List<FoodBusinessManagersDto> foodBusinessEmployeesDto)
        {
            return new PagedListDto<FoodBusinessManagersDto>(pagedUsersList.CurrentPage,
                pagedUsersList.PageCount, pagedUsersList.PageSize, pagedUsersList.RowCount, foodBusinessEmployeesDto);
        }

        private async Task AppendRolesToListOfFoodBusinessManagers(PagedResultBase<ApplicationUser> pagedUsersList,
            List<FoodBusinessManagersDto> data)
        {
            foreach (var user in pagedUsersList.Data)
            {
                var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                var foodBusinessManager = data.FirstOrDefault(FoodBusinessManager => FoodBusinessManager.Id == user.Id);
                foodBusinessManager.Roles = (List<string>) userRoles;
            }
        }

        private async Task AppendFoodBusinessesToListOfFoodBusinessManagers(
            PagedResultBase<ApplicationUser> pagedUsersList, List<FoodBusinessManagersDto> data)
        {
            foreach (var user in pagedUsersList.Data)
            {
                var listOfFoodBusiness = await _context.FoodBusinessUsers.Include(x => x.FoodBusiness)
                    .Where(foodBusinessUser => foodBusinessUser.ApplicationUserId == user.Id)
                    .Select(foodBusinessUser => foodBusinessUser.FoodBusiness)
                    .Distinct()
                    .ToListAsync()
                    .ConfigureAwait(false);

                var foodBusinessManager = data.FirstOrDefault(FoodBusinessManager => FoodBusinessManager.Id == user.Id);

                foreach (var foodBusiness in listOfFoodBusiness) foodBusinessManager.FoodBusinesses.Add(foodBusiness);
            }
        }
        #endregion







        #region Get HotelsManagers within organization
        public async Task<PagedListDto<HotelsManagersDto>> Handle(
            GetHotelsManagersWithinOrganizationQuery request, CancellationToken cancellationToken)
        {
            await ChecksHelper
                .CheckValidation_ThrowExceptionIfQueryIsInvalid<GetHotelsManagersWithinOrganizationValidator,
                    GetHotelsManagersWithinOrganizationQuery>(request, cancellationToken).ConfigureAwait(false);
            var foodBusinessAdministratorId =
                ChecksHelper.GetUserIdFromToken_ThrowExceptionIfUserIdIsNullOrEmpty(_userService);
            await ChecksHelper
                .CheckUserExistence_ThrowExceptionIfUserNotFound(_identityContext, foodBusinessAdministratorId)
                .ConfigureAwait(false);

            var listOfUsersIds = GetUsersIdsByFoodBusinessAdministratorIdHotel(foodBusinessAdministratorId);
            var pagedUsersList = GetPagedUsersByRolesAndUsersIdsHotel(Roles.FoodBusinessManager.ToString(), listOfUsersIds,
                request.Page, request.PageSize);
           
            var userList = await pagedUsersList.Data.ToListAsync(cancellationToken).ConfigureAwait(false);
            var hotelManagersDto = _mapper.Map<List<HotelsManagersDto>>(userList);

            await AppendRolesToListOfHotelManagers(pagedUsersList, hotelManagersDto)
                .ConfigureAwait(false);
            await AppendHotelsToListOfHotelManagers(pagedUsersList, hotelManagersDto)
                .ConfigureAwait(false);
            return ConstructPagedHotelManagers(pagedUsersList, hotelManagersDto);
        }


        private List<string> GetUsersIdsByFoodBusinessAdministratorIdHotel(string foodBusinessAdministratorId)
        {
            return _context.hotelUsers.Include(foodBusinessUsers => foodBusinessUsers.Hotel)
                .Where(foodBusinessUsers => foodBusinessUsers.Hotel.FoodBusinessAdministratorId ==
                                            foodBusinessAdministratorId)
                .Select(foodBusinesses => foodBusinesses.ApplicationUserId)
                .ToList();
        }
      
        private PagedResultBase<ApplicationUser> GetPagedUsersByRolesAndUsersIdsHotel(string role,
            List<string> listOfUsersIds, int page, int pageSize)
        {
            return _identityContext.UserRoles.Include(u => u.Role)
                .Where(u => u.Role.Name == role && listOfUsersIds.Contains(u.User.Id))
                .Select(u => u.User)
                .GetPaged(page, pageSize);
        }
      


        private static PagedListDto<HotelsManagersDto> ConstructPagedHotelManagers(
           PagedResultBase<ApplicationUser> pagedUsersList, List<HotelsManagersDto> hotelEmployeesDto)
        {
            return new PagedListDto<HotelsManagersDto>(pagedUsersList.CurrentPage,
                pagedUsersList.PageCount, pagedUsersList.PageSize, pagedUsersList.RowCount, hotelEmployeesDto);
        }
      


        private async Task AppendRolesToListOfHotelManagers(PagedResultBase<ApplicationUser> pagedUsersList,
           List<HotelsManagersDto> data)
        {
            foreach (var user in pagedUsersList.Data)
            {
                var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                var hotelManager = data.FirstOrDefault(HotelsManager => HotelsManager.Id == user.Id);
                hotelManager.Roles = (List<string>)userRoles;
            }
        }


      
        private async Task AppendHotelsToListOfHotelManagers(
           PagedResultBase<ApplicationUser> pagedUsersList, List<HotelsManagersDto> data)
        {
            foreach (var user in pagedUsersList.Data)
            {
                var listOfHotels = await _context.hotelUsers.Include(x => x.Hotel)
                    .Where(hotelUser => hotelUser.ApplicationUserId == user.Id)
                    .Select(hotelUser => hotelUser.Hotel)
                    .Distinct()
                    .ToListAsync()
                    .ConfigureAwait(false);

                var hotelManager = data.FirstOrDefault(HotelManager => HotelManager.Id == user.Id);

                foreach (var hotel in listOfHotels) hotelManager.Hotels.Add(hotel);
            }
        }
        #endregion


        #region Get users by role
        public async Task<PagedListDto<ApplicationUserDto>> Handle(GetUsersByRoleQuery request, CancellationToken cancellationToken)
        {
            await ChecksHelper.CheckValidation_ThrowExceptionIfQueryIsInvalid<GetUsersByRoleQueryValidator, GetUsersByRoleQuery>
                    (request, cancellationToken).ConfigureAwait(false);

            var pagedUsersList = _identityContext.UserRoles.Include(u => u.Role)
                .Where(u => u.Role.Name == request.Role).Select(u => u.User)
                .GetPaged(request.Page, request.PageSize);


            var data = _mapper.Map<List<ApplicationUserDto>>(await pagedUsersList.Data
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false));

            foreach (var user in pagedUsersList.Data)
            {
                var userRoles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                var applicationUserDto = data.FirstOrDefault(applicationUserDto => applicationUserDto.Id == user.Id);
                applicationUserDto.Roles = (List<string>) userRoles;
            }

            return new PagedListDto<ApplicationUserDto>(pagedUsersList.CurrentPage,
                pagedUsersList.PageCount, pagedUsersList.PageSize, pagedUsersList.RowCount, data);

        }
        #endregion


    }
}