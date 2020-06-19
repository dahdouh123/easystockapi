﻿using FluentValidation;
using SmartRestaurant.Resources.Commun.BaseEntity;
using SmartRestaurant.Resources.Restaurants.Staffs;
using SmartRestaurant.Resources.SharedValidation;

namespace SmartRestaurant.Application.Restaurants.Chains.Commands.Update
{
    public class UpdateChainCommandValidation : AbstractValidator<IUpdateChainModel>
    {
        public UpdateChainCommandValidation()
        {
            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage(string.Format(SharedValidationResource.RequiredErrorMessage, BaseResource.Id));
            RuleFor(x => x.Alias)
               .MaximumLength(5)
               .WithMessage(string.Format(SharedValidationResource.MaxlengthNotValideErrorMessage, "5"));
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage(string.Format(SharedValidationResource.RemoteErrorMessage, BaseResource.Name));

            RuleFor(x => x.Name)
                .MaximumLength(256)
                .WithMessage(string.Format(SharedValidationResource.MaxlengthNotValideErrorMessage, "256")); RuleFor(x => x.OwnerId)
             .NotEmpty()
             .WithMessage(string.Format(SharedValidationResource.RequiredErrorMessage, StaffResource.Owner));
            RuleFor(x => x.OwnerId)
             .NotEmpty()
             .WithMessage(string.Format(SharedValidationResource.RequiredErrorMessage, StaffResource.Owner));

        }
    }
}