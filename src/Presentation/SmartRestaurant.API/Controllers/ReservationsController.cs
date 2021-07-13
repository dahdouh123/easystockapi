﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Reservations.Commands;
using SmartRestaurant.Application.Reservations.Queries;

namespace SmartRestaurant.API.Controllers
{
    [Route("api/foodbusiness")]
    [ApiController]
    public class ReservationsController : ApiController
    {
        [Route("{id:Guid}/reservations/")]
        [HttpPost]
        [Authorize(Roles = "FoodBusinessManager")]
        public async Task<ActionResult> Create([FromRoute] Guid id, CreateReservationCommand command)
        {
            if (id != command.FoodBusinessId)
                return BadRequest();
            var validationResult = await SendAsync(command).ConfigureAwait(false);
            return ApiCustomResponse(validationResult);
        }

        [Route("reservations")]
        [HttpPut]
        [Authorize(Roles = "FoodBusinessManager")]
        public async Task<ActionResult> Update(UpdateReservationCommand command)
        {
            var validationResult = await SendAsync(command).ConfigureAwait(false);
            return ApiCustomResponse(validationResult, NoContent());
        }

        [Route("{id:Guid}/reservations/")]
        [HttpDelete]
        [Authorize(Roles = "FoodBusinessManager")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();
            await SendAsync(new DeleteReservationCommand {CmdId = id}).ConfigureAwait(false);
            return Ok("Successful");
        }

        [Route("{id:Guid}/reservations/")]
        [HttpGet]
        [Authorize(Roles = "FoodBusinessManager")]
        public Task<PagedListDto<ReservationDto>> GetListByReservationDateTimeInterval([FromRoute] Guid id,
            DateTime timeIntervalStart, DateTime timeIntervalEnd, int page, int pageSize)
        {
            var query = new GetReservationsListByReservationDateTimeIntervalQuery
            {
                FoodBusinessId = id,
                TimeIntervalStart = timeIntervalStart,
                TimeIntervalEnd = timeIntervalEnd,
                Page = page,
                PageSize = pageSize
            };
            return SendAsync(query);
        }

        [Route("reservations/client/{id:Guid}/history")]
        [HttpGet]
        [Authorize(Roles = "Diner")]
        public Task<PagedListDto<ReservationClientDto>> GetClientReservationsHistory([FromRoute] Guid id, int page, int pageSize)
        {
            var query = new GetClientReservationsHistoryQuery
            {
                CreatedBy = id.ToString(),
                Page = page,
                PageSize = pageSize
            };
            return SendAsync(query);
        }

        [Route("reservations/{id:Guid}/")]
        [HttpGet]
        [Authorize(Roles = "FoodBusinessManager")]
        public Task<ReservationDto> Get([FromRoute] Guid id)
        {
            return SendAsync(new GetReservationByIdQuery { ReservationId = id });
        }

        [Route("reservations/client/{id:Guid}")]
        [HttpGet]
        [Authorize(Roles = "Diner")]
        public Task<PagedListDto<ReservationClientDto>> GetClientNonExpiredReservations([FromRoute] Guid id, int page, int pageSize)
        {
            var query = new GetClientNonExpiredReservationsQuery
            {
                CreatedBy = id.ToString(),
                Page = page,
                PageSize = pageSize
            };
            return SendAsync(query);
        }
    }
}