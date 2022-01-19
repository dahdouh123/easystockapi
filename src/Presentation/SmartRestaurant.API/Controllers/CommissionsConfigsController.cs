﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRestaurant.API.Swagger.Exception;
using SmartRestaurant.Application.commisiones.Commands;
using SmartRestaurant.Application.commisiones.Queries;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Dtos.CommissionsDtos;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartRestaurant.API.Controllers
{
    [Route("api/commissions")]
    [ApiController]
    [SwaggerTag("List of actions that can be applied on FoodBusiness Commissions Configs")]
    public class CommissionsConfigsController : ApiController
    {
        /// <summary> GetListOfCommissionConfigs() </summary>
        /// <remarks>This endpoint allows us to fetch list of commission configs.</remarks>
        /// <param name="currentFilter">List of commission configs can be filtred by: <b>foodbusinessname</b></param>
        /// <param name="searchKey">Search keyword</param>
        /// <param name="sortOrder">List of commission configs can be sorted by: <b>acs</b> | <b>desc</b>. Default value is: <b>acs</b></param>
        /// <param name="page">The start position of read pointer in a request results. Default value is: <b>1</b></param>
        /// <param name="pageSize">The max number of Reservations that should be returned. Default value is: <b>10</b>. Max value is: <b>100</b></param>
        /// <response code="200"> List of commission configs has been successfully fetched.</response>
        /// <response code="400">The payload data sent to the backend-server in order to fetch list of commission configs is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(PagedListDto<CommissionConfigsDto>), 200)]
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Authorize(Roles = "SuperAdmin,SupportAgent,SalesMan")]
        [HttpGet]
        public Task<IActionResult> GetListOfCommissionConfigs(string currentFilter, string searchKey, string sortOrder, int page, int pageSize)
        {
            var query = new GetCommissionConfigsListQuery
            {
                CurrentFilter = currentFilter,
                SearchKey = searchKey,
                SortOrder = sortOrder,
                Page = page,
                PageSize = pageSize
            };
            return SendWithErrorsHandlingAsync(query);
        }


        /// <summary> SetFoodBusinessCommissionConfigs() </summary>
        /// <remarks>This endpoint allows user to set FoodBusiness Commission Configs. </remarks>        
        /// <param name="id">id of the FoodBusiness that we will set its Commission </param>
        /// <param name="command">This is payload object used to set FoodBusiness Commission</param>
        /// <response code="204">FoodBusiness Commission has been successfully set.</response>
        /// <response code="400">The payload data sent to the backend-server in-order to set FoodBusiness Commission is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Route("{id}")]
        [HttpPatch]
        [Authorize(Roles = "SuperAdmin,SupportAgent,SalesMan")]
        public async Task<IActionResult> SetFoodBusinessCommissionConfigs([FromRoute] string id, SetFoodBusinessCommissionCommand command)
        {
            command.FoodBusinessId = id;
            return await SendWithErrorsHandlingAsync(command);
        }


        /// <summary> GetFoodBusinessCommissionConfigs() </summary>
        /// <remarks>
        ///     This endpoint allows user to get commission configs for the selected FoodBusiness<br></br>
        ///     <b>Note 01:</b> This is the enum used to describe Commission Type: <b>  enum CommissionType { PerPerson, PerOrder } </b><br></br>
        ///     <b>Note 02:</b> This is the enum used to describe Who Pay Commission: <b> enum WhoPayCommission { FoodBusiness, FoodBusinessCustomer } </b><br></br>
        /// </remarks>
        /// <param name="id">id of the FoodBusiness that would be used to fetch its commission configs</param>
        /// <response code="200"> FoodBusiness commission configs has been successfully fetched.</response>
        /// <response code="400">The payload data sent to the backend-server in order to fetch FoodBusiness commission configs is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(CommissionConfigsDto), 200)]
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Route("{id}")]
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,SupportAgent,SalesMan,FoodBusinessManager,FoodBusinessAdministrator")]
        public async Task<IActionResult> GetFoodBusinessCommissionConfigs([FromRoute] string id)
        {
            return await SendWithErrorsHandlingAsync(new GetCommissionConfigsByFoodBusinessIdQuery { FoodBusinessId = id });
        }
    }
}