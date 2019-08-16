﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Application.Services.Models
{
    public class TodaysChefSelection
    {
        IReadOnlyList<MenuDishItemModel> Dishes { get; }        
        IReadOnlyList<MenuProductItemModel> Products { get; }        
    }

    public class TodaysDish
    {
        public ServiceDishModel Dish { get; set; }
    }
}
