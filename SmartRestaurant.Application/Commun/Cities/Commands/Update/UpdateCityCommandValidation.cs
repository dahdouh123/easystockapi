﻿using FluentValidation;
using SmartRestaurant.Resources.Commun.BaseEntity;
using SmartRestaurant.Resources.SharedValidation;
using System;

namespace SmartRestaurant.Application.Commun.Cities.Commands.Update
{
    public class UpdateCityCommandValidation : AbstractValidator<IUpdateCityModel>
    {

        public UpdateCityCommandValidation()
        {

            RuleFor(x => x.Name)
            .MaximumLength(50)
            .NotEmpty()
            .WithMessage(String.Format(SharedValidationResource.RequiredErrorMessage,
            BaseResource.Name));
            RuleFor(x => x.IsoCode)
                .MaximumLength(5)
                .WithMessage(String.Format(SharedValidationResource.MaxlengthNotValideErrorMessage,
                "5"));
            RuleFor(x => x.Alias)
                .MaximumLength(5)
                .WithMessage(String.Format(SharedValidationResource.MaxlengthNotValideErrorMessage,
                "5"));

            RuleFor(x => x.StateId)
             .NotEmpty()
             .WithMessage(string.Format(SharedValidationResource.RequiredErrorMessage,
             BaseResource.Id));

        }
    }
}
