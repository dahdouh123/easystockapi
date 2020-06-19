﻿using FluentValidation;
using SmartRestaurant.Resources.Commun.BaseEntity;
using SmartRestaurant.Resources.SharedValidation;

namespace SmartRestaurant.Application.Commun.Specialites.Commands.Delete
{
    public class DeleteSpecialityModelValidation : AbstractValidator<DeleteSpecialityModel>
    {
        public DeleteSpecialityModelValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(string.Format(SharedValidationResource.RequiredErrorMessage, BaseResource.Id));
        }
    }
}
