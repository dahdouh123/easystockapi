﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Application.Restaurants.Commands.CreateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(v => v.NameEnglish)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
