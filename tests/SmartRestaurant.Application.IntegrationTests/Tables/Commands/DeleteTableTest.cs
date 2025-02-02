﻿using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SmartRestaurant.Application.IntegrationTests.TestTools;
using SmartRestaurant.Application.Tables.Commands;
using SmartRestaurant.Application.Zones.Commands;
using SmartRestaurant.Domain.Entities;

namespace SmartRestaurant.Application.IntegrationTests.Tables.Commands
{
    using static Testing;

    [TestFixture]
    public class DeleteTableTest : TestBase
    {
        [Test]
        public async Task DeleteTable_ShouldSaveToDB()
        {
            await RolesTestTools.CreateRoles();
            var foodBusinessAdministrator = await UsersTestTools.CreateFoodBusinessAdministrator();
            var fastFood = await FoodBusinessTestTools.CreateFoodBusiness(foodBusinessAdministrator.Id);

            var createZoneCommand = await ZoneTestTools.CreateZone(fastFood);

            var zone = await FindAsync<Zone>(createZoneCommand.Id);
            var createTableCommand = new CreateTableCommand
            {
                Capacity = 4,
                ZoneId = zone.ZoneId.ToString(),
            };
            await SendAsync(createTableCommand);
            var deleteTableCommand = new DeleteTableCommand {Id = createTableCommand.Id};
            await SendAsync(deleteTableCommand);
            var item = await FindAsync<Table>(createTableCommand.Id);
            item.Should().BeNull();
        }
    }
}