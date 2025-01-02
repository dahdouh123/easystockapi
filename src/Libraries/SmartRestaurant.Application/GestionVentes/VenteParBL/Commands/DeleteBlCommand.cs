﻿using System;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;

namespace SmartRestaurant.Application.GestionVentes.VenteParBl.Commands
{
    public class DeleteBlCommand : DeleteCommand
    {
    }

    public class DeleteBlCommandValidator : AbstractValidator<DeleteBlCommand>
    {
        public DeleteBlCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}