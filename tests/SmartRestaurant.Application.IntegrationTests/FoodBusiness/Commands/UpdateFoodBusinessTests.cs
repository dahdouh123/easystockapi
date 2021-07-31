﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;
using SmartRestaurant.Application.FoodBusiness.Commands;

namespace SmartRestaurant.Application.IntegrationTests.FoodBusiness.Commands
{
    using static Testing;

    public class UpdateFoodBusinessTests : TestBase
    {
        [Test]
        public async Task ShouldUpdateFoodBusiness()
        {
            var foodBusinessId = Guid.NewGuid();
            await SendAsync(new CreateFoodBusinessCommand
            {
                Name = "Taj mahal",
                AverageRating = 12,
                HasCarParking = true,
                Id = foodBusinessId,
                FoodBusinessAdministratorId = "4"
            });

            await Task.Delay(0).ContinueWith(async t =>
            {
                var updateFoodBusinessCommand = new UpdateFoodBusinessCommand
                {
                    Id = foodBusinessId,
                    Name = "Taj mahal Updated test"
                };

                var validationResult = await SendAsync(updateFoodBusinessCommand);

                var list = await FindAsync<Domain.Entities.FoodBusiness>(foodBusinessId);
                validationResult.Should().Be(default(ValidationResult));


                list.FoodBusinessId.Should().Be(updateFoodBusinessCommand.Id);
            });
        }
    }
}