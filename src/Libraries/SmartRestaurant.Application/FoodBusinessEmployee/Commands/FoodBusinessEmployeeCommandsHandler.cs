using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SmartRestaurant.Application.Common.Configuration;
using SmartRestaurant.Application.Common.Exceptions;
using SmartRestaurant.Application.Common.Interfaces;
using SmartRestaurant.Application.Common.Tools;
using SmartRestaurant.Application.Common.WebResults;
using SmartRestaurant.Domain.Entities;
using SmartRestaurant.Domain.Identity.Entities;

using SmartRestaurant.Application.Common.Enums;

namespace SmartRestaurant.Application.FoodBusinessEmployee.Commands
{
    public class FoodBusinessEmployeeCommandsHandler :
        IRequestHandler<AddEmployeeInOrganizationCommand, Ok>,
        IRequestHandler<UpdateEmployeeRoleInOrganizationCommand, Ok>,
        IRequestHandler<RemoveEmployeeFromOrganizationCommand, Ok>,
        IRequestHandler<InviteUserToJoinOrganizationCommand, Created>,
        IRequestHandler<UserAcceptsInvitationToJoinOrganizationCommand, NoContent>
    {
        private readonly IApplicationDbContext _context;
        private readonly IOptions<EmailTemplates> _emailTemplates;
        private readonly IMapper _mapper;
        private readonly IOptions<SmtpConfig> _smtpConfig;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<WebPortal> _webPortal;
        private readonly IUserService _userService;

        public FoodBusinessEmployeeCommandsHandler(IApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<SmtpConfig> smtpConfig,
            IOptions<WebPortal> webPortal, IOptions<EmailTemplates> emailTemplates, IUserService userService)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _smtpConfig = smtpConfig;
            _webPortal = webPortal;
            _emailTemplates = emailTemplates;
            _userService = userService;
        }

        public async Task<Ok> Handle(AddEmployeeInOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new AddEmployeeInOrganizationCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) throw new ValidationException(result);

            var foodBusinessUser = _context.FoodBusinessUsers.First(b =>
                b.FoodBusinessId == request.FoodBusinessId && b.ApplicationUserId == request.UserId.ToString());

            if (foodBusinessUser == null) throw new NotFoundException(nameof(foodBusinessUser), request.FoodBusinessId);

            var user = _userManager.Users.First(u => u.Id == foodBusinessUser.ApplicationUserId);
            if (user == null) throw new NotFoundException(nameof(user), request.UserId);

            var foodBusiness = _context.FoodBusinesses.First(b => b.FoodBusinessId == request.FoodBusinessId);
            if (foodBusiness == null) throw new NotFoundException(nameof(FoodBusiness), request.FoodBusinessId);

            foodBusinessUser = new FoodBusinessUser
            {
                ApplicationUserId = request.UserId.ToString(),
                FoodBusinessId = request.FoodBusinessId,
                FoodBusiness = foodBusiness
            };
            await _userManager.AddToRoleAsync(user, request.Role);
            _context.FoodBusinessUsers.Add(foodBusinessUser);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return default;
        }

        public async Task<Ok> Handle(RemoveEmployeeFromOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            var validator = new RemoveEmployeeFromInOrganizationCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) throw new ValidationException(result);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (var foodBusinessId in request.businessesIds)
                {
                    var foodBusinessUser = _context.FoodBusinessUsers.First(b =>
                        b.FoodBusinessId == Guid.Parse(foodBusinessId) && b.ApplicationUserId == request.UserId);
                    if (foodBusinessUser == null)
                        throw new NotFoundException(nameof(foodBusinessUser), foodBusinessId);

                    _context.FoodBusinessUsers.Remove(foodBusinessUser);
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }

                transaction.Complete();
            }

            return default;
        }

        public async Task<Ok> Handle(UpdateEmployeeRoleInOrganizationCommand request,
            CancellationToken cancellationToken)
        {

            await ChecksHelper.CheckValidation_ThrowExceptionIfQueryIsInvalid
               <UpdateEmployeeRoleInOrganizationCommandValidator, UpdateEmployeeRoleInOrganizationCommand>
               (request, cancellationToken).ConfigureAwait(false);

           var foodBusinessUser = _context.FoodBusinessUsers.First(b =>
                b.FoodBusinessId == Guid.Parse(request.FoodBusinessId) && b.ApplicationUserId == request.UserId);
            if (foodBusinessUser == null) 
                throw new NotFoundException(nameof(foodBusinessUser), request.FoodBusinessId);

            var user = _userManager.Users.First(u => u.Id == foodBusinessUser.ApplicationUserId);
            if (user == null) 
                throw new NotFoundException(nameof(user), request.UserId);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
               var removeRoleResult = await _userManager.RemoveFromRoleAsync(user, request.OldRole);
                if (!removeRoleResult.Succeeded)
                    throw new UserManagerException(removeRoleResult.Errors);

                var addRoleResult = await _userManager.AddToRoleAsync(user, request.NewRole);
                if (!addRoleResult.Succeeded)
                    throw new UserManagerException(addRoleResult.Errors);

                transaction.Complete();
            }
            return default;
        }

        #region UserAcceptsInvitationToJoinOrganizationHandler

        public async Task<NoContent> Handle(UserAcceptsInvitationToJoinOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            await ChecksHelper.CheckValidation_ThrowExceptionIfQueryIsInvalid
                <UserAcceptsInvitationToJoinOrganizationValidator, UserAcceptsInvitationToJoinOrganizationCommand>
                (request, cancellationToken).ConfigureAwait(false);

            var user = await _userManager.FindByIdAsync(request.Id).ConfigureAwait(false);
            if (user == null)
                throw new NotFoundException(nameof(ApplicationUser), request.Id);

            if (!user.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase))
                throw new NotFoundException(nameof(ApplicationUser), request.Email);

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                _mapper.Map(request, user);

                var userUpdateResult = await _userManager.UpdateAsync(user);
                if (!userUpdateResult.Succeeded)
                    throw new UserManagerException(userUpdateResult.Errors);

                var passwordResetResult = await _userManager.ResetPasswordAsync(user, HexaDecimalHelper.FromHexString(request.Token), request.Password);
                if (!passwordResetResult.Succeeded)
                    throw new UserManagerException(passwordResetResult.Errors);

                transaction.Complete();
            }

            return default;
        }

        #endregion

        #region InviteUserToJoinOrganizationHandler

        public async Task<Created> Handle(InviteUserToJoinOrganizationCommand request,
            CancellationToken cancellationToken)
        {
            await ChecksHelper.CheckValidation_ThrowExceptionIfQueryIsInvalid
                <InviteUserToJoinOrganizationCommandValidator, InviteUserToJoinOrganizationCommand>
                (request, cancellationToken).ConfigureAwait(false);

            var newUser = _mapper.Map<ApplicationUser>(request);
            await CreateUserAndAssignRolesToHim(request, newUser).ConfigureAwait(false);
            await AssignUserToFoodBusinessesOrHotel(request.businessesIds,request.Typeinvitation, newUser.Id, cancellationToken)
                .ConfigureAwait(false);
            await SendConfirmationEmail(newUser);
            return default;
        }

        private async Task CreateUserAndAssignRolesToHim(InviteUserToJoinOrganizationCommand request,
            ApplicationUser newUser)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var randomPassword = Guid.NewGuid().ToString();
                newUser.EmailConfirmed = true;
                var userCreationResult = await _userManager.CreateAsync(newUser, randomPassword);
                if (!userCreationResult.Succeeded)
                    throw new AccountCreationException(userCreationResult.Errors);
                await GrantRoles(request.Roles, newUser).ConfigureAwait(false);
                transaction.Complete();
            }
        }

        private async Task GrantRoles(List<string> roles, ApplicationUser user)
        {
            foreach (var role in roles) await _userManager.AddToRoleAsync(user, role).ConfigureAwait(false);
        }

        private async Task AssignUserToFoodBusinessesOrHotel(List<string> listOfbusinessesIds, string type, string userId,
            CancellationToken cancellationToken)
        {
            if (type==TypeInvitation.foodbusinness)

            {
                foreach (var foodBusinessId in listOfbusinessesIds)
                {
                    var foodBusinessUser = new FoodBusinessUser
                    {
                        ApplicationUserId = userId,
                        FoodBusinessId = Guid.Parse(foodBusinessId)
                    };
                    await _context.FoodBusinessUsers.AddAsync(foodBusinessUser).ConfigureAwait(false);
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
            }

            else
            {
                foreach (var foodBusinessId in listOfbusinessesIds)
                {
                    var HotelUser = new HotelUser
                    {
                        ApplicationUserId = userId,
                        HotelId = Guid.Parse(foodBusinessId)
                    };
                    await _context.hotelUsers.AddAsync(HotelUser).ConfigureAwait(false);
                    await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
            }
        }

        private async Task SendConfirmationEmail(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var webPortalHost = _webPortal.Value.host;
            var webPortalPathToEmployeeAcceptInvitation = _webPortal.Value.pathToEmployeeAcceptInvitation
                .Replace("{id}", user.Id)
                .Replace("{token}", HexaDecimalHelper.ToHexString(token));
            var linkToAcceptInvitationWebPage = $"{webPortalHost}{webPortalPathToEmployeeAcceptInvitation}";

            var userLanguage = _userService.GetUserLanguage();
            var emailSubject = _emailTemplates.Value.InvitationToJoinOrganization.SelectLanguage(userLanguage).Subject;
            var emailTemplate = _emailTemplates.Value.InvitationToJoinOrganization.SelectLanguage(userLanguage).Template
                .Replace("{linkToAcceptInvitationWebPage}", linkToAcceptInvitationWebPage);
            new EmailHelper(_smtpConfig.Value).SendEmail(user.Email, emailSubject, emailTemplate);
        }

        #endregion
    }
}