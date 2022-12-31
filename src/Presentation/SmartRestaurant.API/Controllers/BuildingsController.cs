﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRestaurant.API.Swagger.Exception;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Buildings.Commands;
using Swashbuckle.AspNetCore.Annotations;
using SmartRestaurant.Application.Buildings.Queries;
using System;
using SmartRestaurant.Application.Rooms.Commands;

namespace SmartRestaurant.API.Controllers
{
    [Route("api/hotels/buildings")]
    [ApiController]
    [SwaggerTag("List of actions that can be applied on Buildings in a building of hotel")]
    public class BuildingsController : ApiController
    {

        [Route("{id:guid}")]
        [HttpGet]
        [Authorize(Roles = "FoodBusinessManager")]
        public async Task<IActionResult> GetAllBuildingsByHotelId([FromRoute] Guid id, string currentFilter, string searchKey, int page, int pageSize)
        {
            return await SendWithErrorsHandlingAsync(new GetAllBuildingsByHotelId {
               
                HotelId = id,
                Page = page,
                PageSize = pageSize,
                CurrentFilter = currentFilter,
                SearchKey = searchKey,

            });
        }


        /// <summary> CreateNewBuilding() </summary>
        /// <remarks>
        ///     This endpoint allows <b>foodbuisness manager</b> to create a new building. 
        /// </remarks> 
        /// <param name="command">This is payload object used to create a new building</param>
        /// <response code="204">The building has been successfully created.</response>
        /// <response code="400">The payload data sent to the backend-server in order to create a new building is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [HttpPost]
        [Authorize(Roles = "FoodBusinessManager,FoodBusinessAdministrator,SuperAdmin,SupportAgent")]
        public async Task<IActionResult> Create([FromForm] CreateBuildingCommand command)
        {
            return await SendWithErrorsHandlingAsync(command);
        }


        [HttpPut]
        [Authorize(Roles = "FoodBusinessManager,SupportAgent,FoodBusinessAdministrator,SuperAdmin")]
        public async Task<IActionResult> Update([FromForm] UpdateBuildingCommand command)
        {
            return await SendWithErrorsHandlingAsync(command);
        }

        [Route("{id:guid}")]
        [HttpDelete]
        [Authorize(Roles = "FoodBusinessManager,SupportAgent,FoodBusinessAdministrator,SuperAdmin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return await SendWithErrorsHandlingAsync(new DeleteBuildingCommand { Id = id });
        }
    }
}