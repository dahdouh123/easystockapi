﻿using FluentValidation;
using MediatR;
using SmartRestaurant.Application.Common.Dtos.OrdersDtos;
using SmartRestaurant.Application.Common.Dtos;
using SmartRestaurant.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using SmartRestaurant.Domain.Entities;

namespace SmartRestaurant.Application.Orders.Queries
{
    public class GetAllClientSHOrdersTotalQuery : IRequest<float>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchKey { get; set; }
        public string SortOrder { get; set; }
        public string HotelId { get; set; }
        public string OrderDestinationId { get; set; }
        public string CurrentFilter { get; set; }
        public DateFilter DateInterval { get; set; }
    }

    public class GetAllClientSHOrdersTotalQueryValidator : AbstractValidator<GetAllClientSHOrdersTotalQuery>
    {
        public GetAllClientSHOrdersTotalQueryValidator()
        {
            RuleFor(v => v.PageSize)
                .LessThanOrEqualTo(100);


        }
    }
}
