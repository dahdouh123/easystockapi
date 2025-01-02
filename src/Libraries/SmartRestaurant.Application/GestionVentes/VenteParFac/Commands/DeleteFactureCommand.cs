﻿using System;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;

namespace SmartRestaurant.Application.GestionVentes.VenteParFac.Commands
{
    public class DeleteFactureCommand : DeleteCommand
    {
    }

    public class DeleteFactureCommandValidator : AbstractValidator<DeleteFactureCommand>
    {
        public DeleteFactureCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}