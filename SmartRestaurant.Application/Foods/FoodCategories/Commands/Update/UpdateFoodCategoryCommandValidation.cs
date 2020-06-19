﻿using FluentValidation;
using SmartRestaurant.Resources.Commun.BaseEntity;
using SmartRestaurant.Resources.SharedValidation;

namespace SmartRestaurant.Application.FoodCategories.Commands.Update
{
    public class UpdateFoodCategoryCommandValidation : AbstractValidator<IUpdateFoodCategoryModel>
    {
        public UpdateFoodCategoryCommandValidation()
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
                .WithMessage(string.Format(SharedValidationResource.MaxlengthNotValideErrorMessage, "256"));
        }
    }
}
