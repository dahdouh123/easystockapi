﻿using System;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;
using SmartRestaurant.Application.Common.Dtos.ValueObjects;

namespace SmartRestaurant.Application.TypeReclamation.Commands
{
    public class UpdateServiceTechniqueCommand : UpdateCommand
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public NamesDto Names { get; set; }
    }

    public class UpdateServiceTechniqueCommandValidator : AbstractValidator<UpdateServiceTechniqueCommand>
    {
        public UpdateServiceTechniqueCommandValidator()
        {

            RuleFor(v => v.Names)
              .Cascade(CascadeMode.StopOnFirstFailure)
              .NotNull()
              .DependentRules(() => {
                  RuleFor(v => v.Names.AR)
                     .Cascade(CascadeMode.StopOnFirstFailure)
                     .NotEmpty()
                     .MaximumLength(200);

                  RuleFor(v => v.Names.EN)
                     .Cascade(CascadeMode.StopOnFirstFailure)
                     .NotEmpty()
                     .MaximumLength(200);

                  RuleFor(v => v.Names.FR)
                     .Cascade(CascadeMode.StopOnFirstFailure)
                     .NotEmpty()
                     .MaximumLength(200);

                  RuleFor(v => v.Names.TR)
                     .Cascade(CascadeMode.StopOnFirstFailure)
                     .NotEmpty()
                     .MaximumLength(200);

                  RuleFor(v => v.Names.RU)
                   .Cascade(CascadeMode.StopOnFirstFailure)
                   .NotEmpty()
                   .MaximumLength(200);
              });
            RuleFor(l => l.HotelId).NotEmpty().NotEqual(Guid.Empty).WithMessage("'{PropertyName}' must be a valid GUID");

        }

    }
}