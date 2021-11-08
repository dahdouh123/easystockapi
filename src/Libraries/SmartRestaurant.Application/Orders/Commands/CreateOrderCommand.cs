using System;
using System.Collections.Generic;
using FluentValidation;
using SmartRestaurant.Application.Common.Commands;
using SmartRestaurant.Application.Common.Dtos.OrdersDtos;
using SmartRestaurant.Application.Common.Tools;
using SmartRestaurant.Application.Common.Validators;
using SmartRestaurant.Domain.Enums;

namespace SmartRestaurant.Application.Orders.Commands
{
    public class CreateOrderCommand : CreateCommand
    {
        public OrderTypes Type { get; set; }
        public List<OrderDishDto> Dishes { get; set; }
        public List<OrderProductDto> Products { get; set; }
        public List<OrderOccupiedTableDto> OccupiedTables { get; set; }
        public string FoodBusinessId { get; set; }
    }

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(m => m.Type)
                .IsInEnum();

            RuleFor(m => m.FoodBusinessId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotEqual(Guid.Empty.ToString())
                .Must(ValidatorHelper.ValidateGuid).WithMessage("'{PropertyName}' must be a valid GUID");

            RuleFor(x => x.Dishes)
                 .Must(x => false).WithMessage("Order can not be empty, please select at least a dish or product")
                 .When(x => ChecksHelper.IsEmptyList(x.Dishes) == true && ChecksHelper.IsEmptyList(x.Products) == true);

            RuleFor(x => x.OccupiedTables)
                 .Must(x => false).WithMessage("Please select the Occupied Tables because the order type is DineIn")
                 .When(x => ChecksHelper.IsEmptyList(x.OccupiedTables) == true && x.Type == OrderTypes.DineIn);

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