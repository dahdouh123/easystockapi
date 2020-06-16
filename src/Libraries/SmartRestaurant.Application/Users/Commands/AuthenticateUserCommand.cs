﻿using FluentValidation;
using MediatR;
using SmartRestaurant.Application.Common.Dtos;

namespace SmartRestaurant.Application.Users.Commands
{
    public class AuthenticateUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            RuleFor(v => v.Email)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Password)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
