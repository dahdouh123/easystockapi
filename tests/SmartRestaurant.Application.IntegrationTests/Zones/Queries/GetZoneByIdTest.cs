﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SmartRestaurant.Application.FoodBusiness.Commands;
using SmartRestaurant.Application.Zones.Commands;
using SmartRestaurant.Application.Zones.Queries;

namespace SmartRestaurant.Application.IntegrationTests.Zones.Queries
{
    using static Testing;

    public class GetZoneByIdTest : TestBase
    {
        [Test]
        public async Task ShouldReturnZoneById()
        {
            var createFoodBusinessCommand = new CreateFoodBusinessCommand
            {
                FoodBusinessAdministratorId = Guid.NewGuid().ToString(),
                Name = "fast food test"
            };
            await SendAsync(createFoodBusinessCommand);
            var name = "zone " + Guid.NewGuid();
            var zoneId = Guid.NewGuid();
            await SendAsync(new CreateZoneCommand
            {
                FoodBusinessId = createFoodBusinessCommand.Id,
                ZoneTitle = name,
                Id = zoneId
            });
            var item = await SendAsync(new GetZoneByIdQuery {ZoneId = zoneId});
            item.Should().NotBeNull();
            item.ZoneTitle.Should().Be(name);
        }
    }
}