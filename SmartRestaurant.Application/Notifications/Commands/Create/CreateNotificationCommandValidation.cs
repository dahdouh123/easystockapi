﻿using FluentValidation;
using SmartRestaurant.Resources.Commun.BaseEntity;
using SmartRestaurant.Resources.Mailing;
using SmartRestaurant.Resources.Notification;
using SmartRestaurant.Resources.SharedValidation;
using System;

namespace SmartRestaurant.Application.Notifications.Commands.Create
{
    public class CreateNotificationCommandValidation : AbstractValidator<ICreateNotificationModel>
    {
        public CreateNotificationCommandValidation()
        {

            RuleFor(x => x.TemplateId)
                .NotEmpty()
                .WithMessage(String.Format(SharedValidationResource.RequiredErrorMessage,
                NotificationResource.Template));

            RuleFor(x => x.Name).MaximumLength(50)
            .NotEmpty()
            .WithMessage(String.Format(SharedValidationResource.RequiredErrorMessage,
            BaseResource.Name));

            RuleFor(x => x.Alias)
                .MaximumLength(5)
                .WithMessage(String.Format(SharedValidationResource.MaxlengthNotValideErrorMessage,
                "5"));

        }
    }
}