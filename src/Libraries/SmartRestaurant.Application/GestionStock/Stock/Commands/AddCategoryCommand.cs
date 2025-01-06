﻿using System.Collections.Generic;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;
using SmartRestaurant.Domain.Entities;

namespace SmartRestaurant.Application.Stock.Commands
{
    public class AddCategoryCommand : CreateCommand
    {
        public string Nom { get; set; }
        public List<CategoryAttribute> CategorieAttributs { get; set; }
    }

    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(command => command.Nom)
                .NotNull().WithMessage("La catégorie ne peut pas être nulle.");


            RuleFor(command => command.CategorieAttributs)
                .NotEmpty().WithMessage("La catégorie doit avoir au moins un attribut.");

            // Vous pouvez ajouter d'autres règles de validation pour les attributs et leurs valeurs ici
        }
    }
}