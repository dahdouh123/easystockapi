﻿using System.Collections.Generic;
using MediatR;
using SmartRestaurant.Application.Common.Dtos;

namespace SmartRestaurant.Application.Hotels.Queries
{
    public class GetAllHotelsByFoodBusinessManagerQuery : IRequest<List<HotelDto>>
    {
    }
}