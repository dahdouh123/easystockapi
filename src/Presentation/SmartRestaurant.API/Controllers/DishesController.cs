﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRestaurant.API.Swagger.Exception;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Dtos.DishDtos;
using SmartRestaurant.Application.Dishes.Commands;
using SmartRestaurant.Application.Dishes.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartRestaurant.API.Controllers
{
    [Route("api/dishes")]
    [ApiController]
    [SwaggerTag("List of actions that can be applied on Dishes")]
    public class DishesController : ApiController
    {
        /// <summary> GetListOfDishes() </summary>
        /// <remarks>This endpoint allows us to fetch list of dishes.</remarks>
        /// <param name="currentFilter">Dishes list can be filtred by: <b>name</b></param>
        /// <param name="searchKey">Search keyword</param>
        /// <param name="sortOrder">Dishes list can be sorted by: <b>acs</b> | <b>desc</b>. Default value is: <b>acs</b></param>
        /// <param name="foodBusinessId">If the foodBusinessId is set, we will get dishes list linked to that foodBusinessId else we will get an empty list.</param>
        /// <param name="page">The start position of read pointer in a request results. Default value is: <b>1</b></param>
        /// <param name="pageSize">The max number of Reservations that should be returned. Default value is: <b>10</b>. Max value is: <b>100</b></param>
        /// <response code="200"> Dishes list has been successfully fetched.<br></br><b>Note:</b> Picture will be encoded in Base64</response>
        /// <response code="400">The payload data sent to the backend-server in order to fetch dishes list is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(PagedListDto<DishDto>), 200)]
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Authorize(Roles = "FoodBusinessManager,SupportAgent,Diner")]
        [HttpGet]
        public Task<IActionResult> GetList(string currentFilter, string searchKey, string sortOrder, string foodBusinessId,  int page, int pageSize)
        {
            var query = new GetDishesListQuery
            {
                CurrentFilter = currentFilter,
                SearchKey = searchKey,
                SortOrder = sortOrder,
                Page = page,
                PageSize = pageSize,
                FoodBusinessId = foodBusinessId
            };
            return SendWithErrorsHandlingAsync(query);
        }


        /// <summary> GetDishDetails() </summary>
        /// <remarks>This endpoint allows us to fetch Dish details.</remarks>
        /// <param name="id">id of the dish that would be used to fetch dish details</param>
        /// <response code="200"> Dish details has been successfully fetched.<br></br><b>Note:</b> Picture will be encoded in Base64</response>
        /// <response code="400">The payload data sent to the backend-server in order to fetch dish details is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(DishDto), 200)]
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "FoodBusinessManager,SupportAgent,Diner")]
        public Task<IActionResult> Get([FromRoute] string id)
        {
            return SendWithErrorsHandlingAsync(new GetDishByIdQuery {Id = id});
        }


        /// <summary> CreateNewDish() </summary>
        /// <remarks>
        ///     This endpoint allows SM user to create a new dish.<br></br>
        ///     <b>Note 01:</b> This is the enum used to set ingredient measurement unit: <b> enum MeasurementUnits { Gramme, KiloGramme, MilliLiter, Liter, Unit }</b><br></br>
        ///     <b>Note 02:</b> This is the enum used to set Specification Content Type: <b> enum ContentType { CheckBox, ComboBox }</b><br></br>
        ///     <b>Note 03:</b> Picture should be encoded in Base64
        /// </remarks>
        /// <param name="command">This is the payload object used to create a new dish</param>
        /// <response code="204">The dish has been successfully created.</response>
        /// <response code="400">The payload data sent to the backend-server in order to create a new dish is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [HttpPost]
        [Authorize(Roles = "FoodBusinessManager,SupportAgent")]
        public async Task<IActionResult> Create(CreateDishCommand command)
        {
            return await SendWithErrorsHandlingAsync(command);
        }


        /// <summary> UpdateNewDish() </summary>
        /// <remarks>
        ///     This endpoint allows SM user to update dish.<br></br>
        ///     <b>Note 01:</b> This is the enum used to set ingredient measurement unit: <b> enum MeasurementUnits { Gramme, KiloGramme, MilliLiter, Liter, Unit }</b><br></br>
        ///     <b>Note 02:</b> This is the enum used to set Specification Content Type: <b> enum ContentType { CheckBox, ComboBox }</b><br></br>
        ///     <b>Note 03:</b> Picture should be encoded in Base64
        /// </remarks>
        /// <param name="id">id of the dish that would be updated</param>
        /// <param name="command">This is the payload object used to update dish</param>
        /// <response code="204">The dish has been successfully updated.</response>
        /// <response code="400">The payload data sent to the backend-server in order to update a dish is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Route("{id}")]
        [HttpPut]
        [Authorize(Roles = "FoodBusinessManager,SupportAgent")]
        public async Task<IActionResult> Update([FromRoute] string id, UpdateDishCommand command)
        {
            command.Id = id;
            return await SendWithErrorsHandlingAsync(command);
        }


        /// <summary> DeleteDish() </summary>
        /// <remarks>This endpoint allows <b>Support Agent</b> to delete dish.</remarks>
        /// <param name="id">id of the dish that would be deleted</param>
        /// <response code="204">The dish has been successfully deleted.</response>
        /// <response code="400">The payload data sent to the backend-server in order to delete a dish is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(ExceptionResponse), 400)]

        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "FoodBusinessManager,SupportAgent")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            return await SendWithErrorsHandlingAsync(new DeleteDishCommand {Id = id});
        }
    }
}