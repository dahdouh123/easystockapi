﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SmartRestaurant.Application.FoodBusiness.Commands;
using SmartRestaurant.Application.Menus.Commands;
using SmartRestaurant.Application.Sections.Commands;
using SmartRestaurant.Application.SubSections.Commands;
using SmartRestaurant.Domain.Entities;
using SmartRestaurant.Domain.Enums;

namespace SmartRestaurant.Application.IntegrationTests.SubSections
{
    using static Testing;

    [TestFixture]
    public class DeleteSubSectionTest
    {
        [Test]
        public async Task DeleteSection_ShouldSaveDb()
        {
            var createFoodBusinessCommand = new CreateFoodBusinessCommand
            {
                FoodBusinessAdministratorId = Guid.NewGuid().ToString(),
                Name = "fast food test"
            };
            await SendAsync(createFoodBusinessCommand);
            var menuCmdId = Guid.NewGuid();
            await SendAsync(new CreateMenuCommand
            {
                Id = menuCmdId,
                Name = "test menu",
                MenuState = (int) MenuState.Enabled,
                FoodBusinessId = createFoodBusinessCommand.Id
            });
            var sectionCmdId = Guid.NewGuid();
            await SendAsync(new CreateSectionCommand
            {
                Id = sectionCmdId,
                MenuId = menuCmdId,
                Name = "section test menu"
            });
            var subSectionCmdId = Guid.NewGuid();
            await SendAsync(new CreateSubSectionCommand
            {
                Id = subSectionCmdId,
                SectionId = sectionCmdId,
                Name = "section test menu"
            });
            await SendAsync(new DeleteSubSectionCommand {Id = subSectionCmdId});
            var item = await FindAsync<SubSection>(subSectionCmdId);
            item.Should().BeNull();
        }
    }
}