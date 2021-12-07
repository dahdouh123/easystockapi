﻿using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SmartRestaurant.Application.IntegrationTests.TestTools;
using SmartRestaurant.Application.Menus.Commands;
using SmartRestaurant.Application.Sections.Commands;
using SmartRestaurant.Application.SubSections.Commands;
using SmartRestaurant.Application.SubSections.Queries;
using SmartRestaurant.Domain.Enums;

namespace SmartRestaurant.Application.IntegrationTests.SubSections.Queries
{
    using static Testing;

    [TestFixture]
    public class GetAllSubSectionsTest : TestBase
    {
        [Test]
        public async Task ShouldGetAllSections_ByFoodMenuId()
        {
            await RolesTestTools.CreateRoles();
            var foodBusinessAdministrator = await UsersTestTools.CreateFoodBusinessAdministrator();
            var fastFood = await FoodBusinessTestTools.CreateFoodBusiness(foodBusinessAdministrator.Id);

            var createMenuCommand = new CreateMenuCommand
            {
                Name = "test menu",
                FoodBusinessId = fastFood.FoodBusinessId
            };
            await SendAsync(createMenuCommand);


            var createSectionCommand = new CreateSectionCommand
            {
                Name = "section test",
                MenuId = createMenuCommand.Id
            };
            await SendAsync(createSectionCommand).ConfigureAwait(false);

            for (var i = 0; i < 5; i++)
                await SendAsync(new CreateSubSectionCommand
                {
                    Name = "sub-section test " + i,
                    SectionId = createSectionCommand.Id
                }).ConfigureAwait(false);
            var query = new GetSubSectionsListQuery { SectionId = createSectionCommand.Id, Page = 1, PageSize = 5};
            var result = await SendAsync(query);

            result.Data.Should().HaveCount(5);
        }
    }
}