﻿using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Dishes.Queries;
using SmartRestaurant.Application.IntegrationTests.TestTools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartRestaurant.Application.IntegrationTests.Dishes.Queries
{
    using static Testing;

    [TestFixture]
    public class GetDishByIdTest : TestBase
    {   
        [Test]
        public async Task GetDishById_ShouldReturnSelectedDish()
        {
            await RolesTestTools.CreateRoles();
            var foodBusinessAdministrator = await UsersTestTools.CreateFoodBusinessAdministrator(_authenticatedUserId);
            var fastFood = await FoodBusinessTestTools.CreateFoodBusiness(foodBusinessAdministrator.Id);
            var createIngredientCommand = await IngredientTestTools.CreateIngredient();
            var createDishCommand = await DishTestTools.CreateDish(fastFood.FoodBusinessId, createIngredientCommand.Id);
            var dish = await GetDish(createDishCommand.Id);


            var selectedDish = await SendAsync(new GetDishByIdQuery { Id = createDishCommand.Id.ToString() });
            selectedDish.Should().NotBeNull();

            selectedDish.DishId.Should().Be(dish.DishId);
            selectedDish.Name.Should().Be(dish.Name);
            selectedDish.Price.Should().Be(dish.Price);
            selectedDish.FoodBusinessId.Should().Be(dish.FoodBusinessId.ToString());
            selectedDish.IsSupplement.Should().Be(dish.IsSupplement);
            selectedDish.EstimatedPreparationTime.Should().Be(dish.EstimatedPreparationTime);
            selectedDish.Picture.Should().BeEquivalentTo(Convert.ToBase64String(dish.Picture));


            selectedDish.Specifications.Should().HaveCount(2);
            selectedDish.Specifications[0].Title.Should().Be(dish.Specifications[0].Title);
            selectedDish.Specifications[0].ContentType.Should().Be(dish.Specifications[0].ContentType);
            selectedDish.Specifications[0].CheckBoxContent.Should().Be(dish.Specifications[0].CheckBoxContent);
            selectedDish.Specifications[0].ComboBoxContent.Should().BeEquivalentTo(new string[0]);
            selectedDish.Specifications[1].Title.Should().Be(dish.Specifications[1].Title);
            selectedDish.Specifications[1].ContentType.Should().Be(dish.Specifications[1].ContentType);
            selectedDish.Specifications[1].CheckBoxContent.Should().Be(dish.Specifications[1].CheckBoxContent);
            selectedDish.Specifications[1].ComboBoxContent.Should().BeEquivalentTo(dish.Specifications[1].ComboBoxContent.Split(';'));

            selectedDish.Ingredients.Should().HaveCount(1);
            selectedDish.Ingredients[0].InitialAmount.Should().Be(dish.Ingredients[0].InitialAmount);
            selectedDish.Ingredients[0].MinimumAmount.Should().Be(dish.Ingredients[0].MinimumAmount);
            selectedDish.Ingredients[0].MaximumAmount.Should().Be(dish.Ingredients[0].MaximumAmount);
            selectedDish.Ingredients[0].AmountIncreasePerStep.Should().Be(dish.Ingredients[0].AmountIncreasePerStep);
            selectedDish.Ingredients[0].PriceIncreasePerStep.Should().Be(dish.Ingredients[0].PriceIncreasePerStep);
            selectedDish.Ingredients[0].Ingredient.IngredientId.Should().Be(dish.Ingredients[0].IngredientId);
            selectedDish.Ingredients[0].Ingredient.Names.Should().BeEquivalentTo(JsonConvert.DeserializeObject<List<IngredientNameDto>>(dish.Ingredients[0].Ingredient.Names));
            selectedDish.Ingredients[0].Ingredient.EnergeticValue.Amount.Should().Be(dish.Ingredients[0].Ingredient.EnergeticValue.Amount);
            selectedDish.Ingredients[0].Ingredient.EnergeticValue.MeasurementUnit.Should().Be(dish.Ingredients[0].Ingredient.EnergeticValue.MeasurementUnit);
            selectedDish.Ingredients[0].Ingredient.EnergeticValue.Fat.Should().Be(dish.Ingredients[0].Ingredient.EnergeticValue.Fat);
            selectedDish.Ingredients[0].Ingredient.EnergeticValue.Protein.Should().Be(dish.Ingredients[0].Ingredient.EnergeticValue.Protein);
            selectedDish.Ingredients[0].Ingredient.EnergeticValue.Carbohydrates.Should().Be(dish.Ingredients[0].Ingredient.EnergeticValue.Carbohydrates);
            selectedDish.Ingredients[0].Ingredient.EnergeticValue.Energy.Should().Be(dish.Ingredients[0].Ingredient.EnergeticValue.Energy);
            selectedDish.Ingredients[0].Ingredient.Picture.Should().Be(Convert.ToBase64String(dish.Ingredients[0].Ingredient.Picture));


            selectedDish.Supplements.Should().HaveCount(1);
            selectedDish.Supplements[0].DishId.Should().Be(dish.Supplements[0].Supplement.DishId);
            selectedDish.Supplements[0].Description.Should().Be(dish.Supplements[0].Supplement.Description);
            selectedDish.Supplements[0].EnergeticValue.Should().Be(dish.Supplements[0].Supplement.EnergeticValue);
            selectedDish.Supplements[0].Name.Should().Be(dish.Supplements[0].Supplement.Name);
            selectedDish.Supplements[0].Picture.Should().Be(Convert.ToBase64String(dish.Supplements[0].Supplement.Picture));
            selectedDish.Supplements[0].Price.Should().Be(dish.Supplements[0].Supplement.Price);
        }
    }
}
