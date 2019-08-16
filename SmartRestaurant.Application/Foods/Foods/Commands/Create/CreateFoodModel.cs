﻿using System.Collections.Generic;
using SmartRestaurant.Application.Foods.Foods.Models;
using SmartRestaurant.Application.Foods.Models;

namespace SmartRestaurant.Application.Foods.Commands.Create
{
    public class CreateFoodModel : IFoodModelCommand
    {
        public CreateFoodModel()
        {
            Compositions = new List<FoodCompositionModel>();
        }
        public IFoodModel FoodModel { get; set; }
        public INutritionModel Nutrition { get; set; }
        public List<FoodCompositionModel> Compositions { get; set; }
    }
}