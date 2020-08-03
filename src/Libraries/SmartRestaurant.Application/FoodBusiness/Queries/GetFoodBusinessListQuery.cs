﻿using System.Collections.Generic;
using MediatR;
using SmartRestaurant.Application.Common.Dtos;

namespace SmartRestaurant.Application.FoodBusiness.Queries
{
    public class GetFoodBusinessListQuery : IRequest<List<FoodBusinessDto>>
    {
    }
}
