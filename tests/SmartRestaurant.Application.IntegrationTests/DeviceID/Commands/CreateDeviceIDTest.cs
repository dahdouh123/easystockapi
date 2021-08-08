﻿using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;
using SmartRestaurant.Application.DeviceID.Commands;
using SmartRestaurant.Application.DeviceID.Queries;
using SmartRestaurant.Application.FoodBusiness.Commands;
using SmartRestaurant.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace SmartRestaurant.Application.IntegrationTests.DeviceID.Commands
{
    using static Testing;
    [TestFixture]
    public class CreateDeviceIDTest
    {
        [Test]
        public async Task CreateDeviceID_ShouldSaveToDB()
        {
            var createFoodBusinessCommand = new CreateFoodBusinessCommand
            {
                FoodBusinessAdministratorId = Guid.NewGuid().ToString(),
                Name = "fast food test"
            };
            await SendAsync(createFoodBusinessCommand);
            var fastFood = await FindAsync<Domain.Entities.FoodBusiness>(createFoodBusinessCommand.Id   );

            var createDeviceIDCommand = new CreateDeviceIDCommand
            {
                FoodBusinessId = fastFood.FoodBusinessId,
                IdentifierDevice= "5SD-65F5-F5S-DF65SF-5SF6"
            };
            var validationResult = await SendAsync(createDeviceIDCommand);
            var item = await FindAsync<LinkedDevice>(createDeviceIDCommand.Id);

            var query = new GetDeviceIDByIdQuery { IdentifierDevice = createDeviceIDCommand.IdentifierDevice};
            var result = await SendAsync(query);
            result.Should().NotBeNull();
            result.IdentifierDevice.Should().Be("5SD-65F5-F5S-DF65SF-5SF6");
        }
    }
}
