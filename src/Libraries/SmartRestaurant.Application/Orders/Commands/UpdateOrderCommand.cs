using System;
using System.Collections.Generic;
using FluentValidation;
using MediatR;
using SmartRestaurant.Application.Common.Dtos.OrdersDtos;
using SmartRestaurant.Application.Common.Dtos.ValueObjects;
using SmartRestaurant.Application.Common.Tools;
using SmartRestaurant.Application.Common.Validators;
using SmartRestaurant.Application.Common.WebResults;
using SmartRestaurant.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartRestaurant.Application.Orders.Commands
{
    public class UpdateOrderCommand : IRequest<NoContent>
    {
        [SwaggerSchema(ReadOnly = true)] public string Id { get; set; }
        public OrderTypes Type { get; set; }
        public TakeoutDetailsDto TakeoutDetails { get; set; }
        public List<OrderDishDto> Dishes { get; set; }
        public List<OrderProductDto> Products { get; set; }
        public List<OrderOccupiedTableDto> OccupiedTables { get; set; }
    }

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(product => product.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotEqual(Guid.Empty.ToString())
                .Must(ValidatorHelper.ValidateGuid).WithMessage("'{PropertyName}' must be a valid GUID");

            RuleFor(m => m.Type)
                .IsInEnum();

            RuleFor(x => x.TakeoutDetails)
                 .Cascade(CascadeMode.StopOnFirstFailure)
                 .NotEmpty().WithMessage("'{PropertyName}' details must not be empty")
                 .DependentRules(() => {
                     RuleFor(x => x.TakeoutDetails.Type)
                      .IsInEnum();

                     RuleFor(x => x.TakeoutDetails.DeliveryTime)
                        .Must(x => false).WithMessage("'{PropertyName}' must be null because you have set Takeout type as Instant")
                        .When(x => x.TakeoutDetails.Type == TakeoutType.Instant && x.TakeoutDetails.DeliveryTime != null);

                     RuleFor(x => x.TakeoutDetails.DeliveryTime)
                       .Must(x => false).WithMessage("You have to set Delivery Time because you have set Takeout type as Delayed")
                       .When(x => x.TakeoutDetails.Type == TakeoutType.Delayed && x.TakeoutDetails.DeliveryTime == default);
                 })
                 .When(x => x.Type == OrderTypes.Takeout);
        
            RuleFor(x => x.Dishes)
                 .Must(x => false).WithMessage("Order can not be empty, please select at least a dish or product")
                 .When(x => ChecksHelper.IsEmptyList(x.Dishes) == true && ChecksHelper.IsEmptyList(x.Products) == true);

            RuleFor(x => x.OccupiedTables)
                 .Must(x => false).WithMessage("Please select the Occupied Tables because the order type is DineIn")
                 .When(x => ChecksHelper.IsEmptyList(x.OccupiedTables) == true && x.Type == OrderTypes.DineIn);

            RuleFor(x => x.OccupiedTables)
                 .Must(x => false).WithMessage("You can not occupy tables if Order type is Takeout or Delivery")
                 .When(x => ChecksHelper.IsEmptyList(x.OccupiedTables) == false && (x.Type == OrderTypes.Takeout || x.Type == OrderTypes.Delivery));

            RuleForEach(x => x.OccupiedTables)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("'Dish Occupied Table' must not be empty")
                .SetValidator(new OrderOccupiedTableDtoValidator())
                .When(x => ChecksHelper.IsEmptyList(x.OccupiedTables) == false && x.Type == OrderTypes.DineIn);

            RuleForEach(x => x.Products)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("'Ingredient' must not be empty")
                .SetValidator(new OrderProductDtoValidator())
                .When(x => ChecksHelper.IsEmptyList(x.Products) == false);

            RuleForEach(x => x.Dishes)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("'Dish' must not be empty")
                .SetValidator(new OrderDishDtoValidator())
                .When(x => ChecksHelper.IsEmptyList(x.Dishes) == false);
        }
    }
}