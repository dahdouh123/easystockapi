﻿using System;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;
using SmartRestaurant.Application.Common.Enums;
using SmartRestaurant.Domain.Entities;
using SmartRestaurant.Domain.Enums;

namespace SmartRestaurant.Application.AdminArea.Commands
{
    public class CreateClientAppCommand : CreateCommand
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string MacAdresse { get; set; }

    }

    public class CreateClientAppCommandValidator : AbstractValidator<CreateClientAppCommand>
    {
        public CreateClientAppCommandValidator()
        {
            RuleFor(m => m.Id).NotEmpty().Must(id => id != Guid.Empty);

        }
    }
}