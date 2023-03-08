using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BillCommandsHandlers : 
        IRequestHandler<UpdateBillCommand, NoContent>,
        IRequestHandler<PayBillCommand, NoContent>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ISaleOrderRepository _saleOrderRepository;
        public BillCommandsHandlers(IApplicationDbContext context, IMapper mapper, IUserService userService ,ISaleOrderRepository saleOrderRepository)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
               _saleOrderRepository = saleOrderRepository;
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

            if (order.Status == OrderStatuses.Billed)
                throw new ConflictException("Sorry, you can not update a paid Bill");

            MapBillDiscount(request, order);
            CalculateAndSetOrderTotalPrice(order);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return default;
        }

        public async Task<NoContent> Handle(PayBillCommand request, CancellationToken cancellationToken)
        {
            var validator = new PayBillCommandValidator();
            var result = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!result.IsValid) throw new ValidationException(result);

            var order = await _context.Orders
             .Include(o => o.OccupiedTables)
              .Include(o => o.Dishes)
             .Include(o => o.Products)
                 
              .FirstOrDefaultAsync(o => o.OrderId == Guid.Parse(request.Id), cancellationToken)
              .ConfigureAwait(false);
            if (order == null)
                throw new NotFoundException(nameof(Order), request.Id);

            if (order.Status == OrderStatuses.Billed)
                throw new ConflictException("Bill has been already paid");

            order.Status = OrderStatuses.Billed;

               //await _saleOrderRepository.CreateAsync(order);

            order.LastModifiedBy = ChecksHelper.GetUserIdFromToken_ThrowExceptionIfUserIdIsNullOrEmpty(_userService);
            order.LastModifiedAt = DateTime.Now;

            ChangeStatusForReleasedTablesOnlyIfOrderTypeIsDineIn(order);
            _context.Orders.Update(order);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return default;
        }

        private void ChangeStatusForReleasedTablesOnlyIfOrderTypeIsDineIn(Order order)
        {
            if (order.Type != OrderTypes.DineIn)
                return;

            List<string> releasedTables = order.OccupiedTables.Select(x => x.TableId).ToList();
            foreach (var tableId in releasedTables)
            {
                var table = _context.Tables.AsNoTracking().FirstOrDefault(t => t.TableId == Guid.Parse(tableId));
                if (table == null)
                    continue;

                table.TableState = TableState.Available;
                _context.Tables.Update(table);
            }
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
                var totalDishPriceByQuantity = dish.Quantity * dish.UnitPrice;

                if (dish.Discount > totalDishPriceByQuantity || dish.Discount < 0)
                    throw new ValidationException($"The discount value for the dish [{dish.Name}] must be between 0 and {totalDishPriceByQuantity}");
                else
                    totalToPay += totalDishPriceByQuantity - dish.Discount;
            }

            foreach (var product in order.Products)
            {
                product.UnitPrice = product.InitialPrice;
                var totalProductPriceByQuantity = product.Quantity * product.UnitPrice;

                if (product.Discount > totalProductPriceByQuantity || product.Discount < 0)
                    throw new ValidationException($"The discount value for the product [{product.Name}] must be between 0 and {totalProductPriceByQuantity}");
                else
                    totalToPay += totalProductPriceByQuantity - product.Discount;
            }

            if (order.Discount > totalToPay || order.Discount < 0)
                throw new ValidationException($"The value of the global discount must be between 0 and {totalToPay}");
            else
                order.TotalToPay = totalToPay - order.Discount;
        }
    }
}