﻿using FluentAssertions;
using NUnit.Framework;
using SmartRestaurant.Application.FoodBusiness.Commands;
using SmartRestaurant.Application.Zones.Commands;
using SmartRestaurant.Application.Zones.Queries;
using System;
using System.Threading.Tasks;

namespace SmartRestaurant.Application.IntegrationTests.Zones.Queries
{
    using static Testing;

    public class GetZonesListTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllZonesList()
        {

            CreateFoodBusinessCommand createFoodBusinessCommand = new CreateFoodBusinessCommand
            {
                FoodBusinessAdministratorId = Guid.NewGuid().ToString(),
                NameEnglish = "fast food test"
            };
            await SendAsync(createFoodBusinessCommand);
            for (int i = 0; i < 5; i++)
            {
                var name = "zone " + Guid.NewGuid();
                await SendAsync(new CreateZoneCommand
                {
                    FoodBusinessId = createFoodBusinessCommand.CmdId,
                    ZoneTitle = name
                });
            }

            var query = new GetZonesListQuery { FoodBusinessId = createFoodBusinessCommand.CmdId };

            var result = await SendAsync(query);

            result.Should().HaveCount(5);
        }
    }
}
