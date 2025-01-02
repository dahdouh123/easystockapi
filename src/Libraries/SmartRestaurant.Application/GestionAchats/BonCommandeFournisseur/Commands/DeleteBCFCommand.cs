﻿using System;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;

namespace SmartRestaurant.Application.GestionAchats.BonCommandeFournisseur.Commands
{
    public class DeleteBCFCommand : DeleteCommand
    {
    }

    public class DeleteBCFCommandValidator : AbstractValidator<DeleteBCFCommand>
    {
        public DeleteBCFCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty().NotNull().NotEqual(Guid.Empty);
        }
    }
}