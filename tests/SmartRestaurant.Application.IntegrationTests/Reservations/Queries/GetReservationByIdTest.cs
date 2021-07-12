﻿using FluentAssertions;
using NUnit.Framework;
using SmartRestaurant.Application.FoodBusiness.Commands;
using SmartRestaurant.Application.Reservations.Commands;
using SmartRestaurant.Application.Reservations.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartRestaurant.Application.IntegrationTests.Reservations.Queries
{


    using static Testing;

    [TestFixture]
    public class GetReservationByIdTest : TestBase
    {
        [Test]
        public async Task ShouldGetReservation_ById()
        {

            var foodBusinessId = Guid.NewGuid();
            var createFoodBusinessCommand = new CreateFoodBusinessCommand
            {
                CmdId = foodBusinessId,
                FoodBusinessAdministratorId = Guid.NewGuid().ToString(),
                Name = "fast food test"
            };
            await SendAsync(createFoodBusinessCommand);
            var fastFood = await FindAsync<Domain.Entities.FoodBusiness>(createFoodBusinessCommand.CmdId);
          

            var createReservationCommand = new CreateReservationCommand
            {
                CmdId = Guid.NewGuid(),
                ReservationName = "Reservation Test",
                NumberOfDiners = 3,
                ReservationDate = DateTime.Now.AddDays(1),
                FoodBusinessId = foodBusinessId,
                CreatedBy = Guid.NewGuid().ToString()
            };
            await SendAsync(createReservationCommand);
           
            var query = new GetReservationByIdQuery { ReservationId= createReservationCommand.CmdId };
            var result = await SendAsync(query);
            result.Should().NotBeNull();
            result.ReservationName.Should().Be("Reservation Test");
        }
    }
}

