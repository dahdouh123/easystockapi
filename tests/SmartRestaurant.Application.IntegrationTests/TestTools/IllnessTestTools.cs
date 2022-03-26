﻿using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Illness.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartRestaurant.Application.IntegrationTests.TestTools
{
    using static Testing;
    public class IllnessTestTools
    {
        public static async Task<CreateIllnessCommand> CreateIllness(Guid ingredientId)
        {
            var createIllnessCommand = new CreateIllnessCommand
            {
                Name = "test",
                Ingredients = new List<IngredientIllnessDto>()
                {
                    new IngredientIllnessDto()
                    {
                        IngredientId = ingredientId.ToString()
                    }
                }
            };
            await SendAsync(createIllnessCommand);
            var createIllnessCommand2 = new CreateIllnessCommand
            {
                Name = "allergie",
                Ingredients = new List<IngredientIllnessDto>()
                {
                   new IngredientIllnessDto()
                    {
                        IngredientId = ingredientId.ToString()
                    }
                }
            };
            await SendAsync(createIllnessCommand2);
            return createIllnessCommand;
        }
        public static async Task<CreateIllnessCommand> CreateIllnesswithDeffirentIngredients(Guid ingredientId, Guid ingredientId2)
        {
            var createIllnessCommand = new CreateIllnessCommand
            {
                Name = "test",
                Ingredients = new List<IngredientIllnessDto>()
                {
                    new IngredientIllnessDto()
                    {
                        IngredientId = ingredientId.ToString()
                    }
                }
            };
            await SendAsync(createIllnessCommand);
            var createIllnessCommand2 = new CreateIllnessCommand
            {
                Name = "allergie",
                Ingredients = new List<IngredientIllnessDto>()
                {
                   new IngredientIllnessDto()
                    {
                        IngredientId = ingredientId2.ToString()
                    }
                }
            };
            await SendAsync(createIllnessCommand2);
            return createIllnessCommand;
        }
    }
}
