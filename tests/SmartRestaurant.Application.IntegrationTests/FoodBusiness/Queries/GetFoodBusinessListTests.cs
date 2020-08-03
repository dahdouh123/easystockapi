﻿using System;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SmartRestaurant.Application.FoodBusiness.Commands;
using SmartRestaurant.Application.FoodBusiness.Queries;

namespace SmartRestaurant.Application.IntegrationTests.FoodBusiness.Queries
{
    using static Testing;
    public class GetFoodBusinessListTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllFoodBusinessList()
        {
            for (int i = 0; i < 5; i++)
            { 
                await SendAsync(new CreateFoodBusinessCommand
                {
                    NameFrench = "tacos Dz  " + i.ToString(),
                    NameEnglish = "tacos Dz  " + i.ToString(),
                    NameArabic = "تاكوس دزاد" + i.ToString(),
                    FoodBusinessAdministratorId = Guid.NewGuid().ToString(),
                    AverageRating = 12,
                    HasCarParking = true
                });
            }
            var query = new GetFoodBusinessListQuery();

            var result = await SendAsync(query);

            result.Should().HaveCount(5);
        }
    }
}
