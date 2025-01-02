﻿using System;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;
using SmartRestaurant.Domain.Common.Enums;
using SmartRestaurant.Domain.Entities;
using SmartRestaurant.Domain.Enums;

namespace SmartRestaurant.Application.GestionEmployees.Employees.Fournisseurs.Commands
{
    public class UpdateFournisseurCommand : UpdateCommand
    {

        public string FullName { get; set; }
        public string RaisonSociale { get; set; }
        public string Commerce { get; set; }

        public string Email { get; set; }
        public string Addresse { get; set; }
        public string Tel { get; set; }
        public decimal Nrc { get; set; }
        public decimal Nif { get; set; }
        public decimal Nic { get; set; }

        public decimal Numarticle { get; set; }


    }

    public class UpdateFournisseurCommandValidator : AbstractValidator<UpdateFournisseurCommand>
    {
        public UpdateFournisseurCommandValidator()
        {
            RuleFor(m => m.Id).NotEmpty().Must(id => id != Guid.Empty);
          
        }
    }
}