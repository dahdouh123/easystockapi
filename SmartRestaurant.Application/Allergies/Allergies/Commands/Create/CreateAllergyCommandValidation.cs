﻿using FluentValidation;
using SmartRestaurant.Resources.Commun.BaseEntity;
using SmartRestaurant.Resources.SharedValidation;

namespace SmartRestaurant.Application.Allergies.Allergies.Commands.Create
{
    public class CreateAllergyCommandValidation : AbstractValidator<ICreateAllergyModel>
    {
        public CreateAllergyCommandValidation()
        {
 RuleFor(x => x.Alias)
               .MaximumLength(5)
               .WithMessage(string.Format(SharedValidationResource.MaxlengthNotValideErrorMessage, "5"));
            RuleFor(x => x.Name)
                 .NotNull()
                 .NotEmpty()
                 .WithMessage(string.Format(SharedValidationResource.RequiredErrorMessage, BaseResource.Name));

            RuleFor(x => x.Name)
                .MaximumLength(256)
                .WithMessage(string.Format(SharedValidationResource.MaxlengthNotValideErrorMessage, "256"));

        }
    }
}