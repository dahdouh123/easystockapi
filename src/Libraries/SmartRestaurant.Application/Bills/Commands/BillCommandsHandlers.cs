using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Application.Common.Exceptions;
using SmartRestaurant.Application.Common.Interfaces;
using SmartRestaurant.Application.Common.Tools;
using SmartRestaurant.Application.Common.WebResults;
using SmartRestaurant.Domain.Entities;
using SmartRestaurant.Domain.Enums;

namespace SmartRestaurant.Application.Bills.Commands
{
    public class BillCommandsHandlers : IRequestHandler<UpdateBillCommand, NoContent>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public BillCommandsHandlers(IApplicationDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

         
        public async Task<NoContent> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBillCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) throw new ValidationException(result);
           
            var order = await _context.Orders
                .Include(o => o.Dishes)
                .ThenInclude(o => o.Specifications)
                .Include(o => o.Dishes)
                .ThenInclude(o => o.Ingredients)
                .Include(o => o.Dishes)
                .ThenInclude(o => o.Supplements)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.OrderId == Guid.Parse(request.Id), cancellationToken)
                .ConfigureAwait(false);
            if (order == null)
                throw new NotFoundException(nameof(Order), request.Id);

            MapBillDiscount(request, order);
            CalculateAndSetOrderTotalPrice(order);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return default;
        }

        private void MapBillDiscount(UpdateBillCommand request, Order order)
        {
            order.Discount = request.Discount;
            foreach (var dish in order.Dishes)
            {
                dish.Discount = request.Dishes.Find(d => d.OrderDishId == dish.OrderDishId.ToString()).Discount;   
            }

            foreach (var product in order.Products)
            {
                product.Discount = request.Products.Find(d => d.OrderProductId == product.OrderProductId.ToString()).Discount;
            }
        }

        private  void CalculateAndSetOrderTotalPrice(Order order)
        {
            float totalToPay = 0;

            foreach (var dish in order.Dishes)
            {
                float totalDishPrice = dish.InitialPrice;

                foreach (var supplement in dish.Supplements)
                {
                    if(supplement.IsSelected == true)
                        totalDishPrice += supplement.Price;
                }

                foreach (var ingredient in dish.Ingredients)
                {
                    totalDishPrice += (ingredient.Steps * ingredient.PriceIncreasePerStep);
                }
                dish.UnitPrice = totalDishPrice;
                totalToPay += (dish.Quantity * CalculatePriceAfterDiscount(dish.UnitPrice, dish.Discount));
            }

            foreach (var product in order.Products)
            {
                totalToPay += (product.Quantity * CalculatePriceAfterDiscount(product.UnitPrice, product.Discount));
            }

            order.TotalToPay = CalculatePriceAfterDiscount(totalToPay, order.Discount);            
        }

        private float CalculatePriceAfterDiscount(float price, int discount)
        {
            var discountAmount = (price * discount) / 100;
            return price - discountAmount;
        }
    }
}