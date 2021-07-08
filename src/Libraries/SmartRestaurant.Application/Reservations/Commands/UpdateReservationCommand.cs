﻿using System;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;

namespace SmartRestaurant.Application.Reservations.Commands
{
    public class UpdateReservationCommand : SmartRestaurantCommand
    {
        public UpdateReservationCommand()
        {
            LastModifiedAt = DateTime.Now;
        }
        public string ReservationName { get; set; }
        public int NumberOfDiners { get; set; }
        public DateTime ReservationDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedAt { get; }

    }

    public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
    {
        public UpdateReservationCommandValidator()
        {
            RuleFor(reservation => reservation.ReservationName).NotEmpty().MaximumLength(200).MinimumLength(5);
            RuleFor(reservation => reservation.NumberOfDiners).GreaterThan(0).LessThan(1000);
            RuleFor(reservation => reservation.ReservationDate).GreaterThan(date => DateTime.Now);
            RuleFor(reservation => reservation.CmdId).NotEmpty();
            RuleFor(reservation => reservation.LastModifiedBy).NotEmpty();

        }
    }
}