﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRestaurant.API.Swagger.Exception;
using SmartRestaurant.Application.Bills.Commands;
using SmartRestaurant.Application.Bills.Queries;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Dtos.BillDtos;
using SmartRestaurant.Application.Common.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartRestaurant.API.Controllers
{
    [Route("api/bills")]
    [ApiController]
    [SwaggerTag("List of actions that can be applied on Order Bill")]
    public class BillController : ApiController
    {
        /// <summary> UpdateBillDiscount() </summary>
        /// <remarks>This endpoint allows user to update Bill Discount. </remarks>        
        /// <param name="id">id of the order that will used to update its bill biscount </param>
        /// <param name="command">This is payload object used to update bill discount</param>
        /// <response code="204">Bill discount has been successfully updated.</response>
        /// <response code="400">The payload data sent to the backend-server in-order to update bill discount is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Route("{id}")]
        [HttpPut]
        [Authorize(Roles = "FoodBusinessManager,Cashier,SupportAgent")]
        public async Task<IActionResult> Update([FromRoute] string id, UpdateBillCommand command)
        {
            command.Id = id;
            return await SendWithErrorsHandlingAsync(command);
        }


        /// <summary> GetListOfBills() </summary>
        /// <remarks>This endpoint allows us to fetch list of bills.</remarks>
        /// <param name="currentFilter">Bills list can be filtred by: <b>order number</b></param>
        /// <param name="searchKey">Search keyword</param>
        /// <param name="sortOrder">Bills list can be sorted by: <b>acs</b> | <b>desc</b>. Default value is: <b>acs</b></param>
        /// <param name="foodBusinessId">We will get bills list linked to that foodBusinessId.</param>
        /// <param name="dateInterval">We will get results within the selected interval. Default interval is: <b>ToDay</b><br></br>
        ///     <b>Note 01:</b> This is the enum used to set Date Interval: <b>  enum DateFilter { ToDay, Last7Days, Last30Days, All } </b>
        /// </param>
        /// <param name="page">The start position of read pointer in a request results. Default value is: <b>1</b></param>
        /// <param name="pageSize">The max number of Results that should be returned. Default value is: <b>10</b>. Max value is: <b>100</b></param>
        /// <response code="200"> Bills list has been successfully fetched.</response>
        /// <response code="400">The payload data sent to the backend-server in order to fetch bills list is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(PagedListDto<BillDto>), 200)]
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Authorize(Roles = "FoodBusinessAdministrator,FoodBusinessManager,Cashier,SupportAgent")]
        [HttpGet]
        public Task<IActionResult> GetList(string currentFilter, string searchKey, string sortOrder, string foodBusinessId,
            int page, int pageSize, DateFilter dateInterval)
        {
            var query = new GetBillsListQuery
            {
                CurrentFilter = currentFilter,
                SearchKey = searchKey,
                SortOrder = sortOrder,
                Page = page,
                PageSize = pageSize,
                FoodBusinessId = foodBusinessId,
                DateInterval = dateInterval
            };
            return SendWithErrorsHandlingAsync(query);
        }


        /// <summary> GetBillDetails() </summary>
        /// <remarks>This endpoint allows us to fetch bill details.</remarks>
        /// <param name="id">id of the order that would be used to fetch its bill details</param>
        /// <response code="200"> Bill details has been successfully fetched.</response>
        /// <response code="400">The payload data sent to the backend-server in order to fetch bill details is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(BillDto), 200)]
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Route("{id}")]
        [HttpGet]
        [Authorize(Roles = "FoodBusinessAdministrator,FoodBusinessManager,Cashier,SupportAgent")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return await SendWithErrorsHandlingAsync(new GetBillByIdQuery { Id = id });
        }


        /// <summary> PayBill() </summary>
        /// <remarks>This endpoint allows user to Pay a Bill. </remarks>        
        /// <param name="id">id of the order that will used to pay its bill </param>
        /// <response code="204">Bill payment has been successfully done.</response>
        /// <response code="400">The payload data sent to the backend-server in-order to pay a bill is invalid.</response>
        /// <response code="401">The cause of 401 error is one of two reasons: Either the user is not logged into the application or authentication token is invalid or expired.</response>
        /// <response code="403"> The user account you used to log into the application, does not have the necessary privileges to execute this request.</response>
        [ProducesResponseType(typeof(ExceptionResponse), 400)]
        [Route("{id}/pay")]
        [HttpPatch]
        [Authorize(Roles = "FoodBusinessManager,Cashier,SupportAgent,FoodBusinessAdministrator")]
        public async Task<IActionResult> PayBill([FromRoute] string id)
        {
            return await SendWithErrorsHandlingAsync(new PayBillCommand { Id = id });
        }
    }
}