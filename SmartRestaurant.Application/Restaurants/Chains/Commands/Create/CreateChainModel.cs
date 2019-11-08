﻿using System;

namespace SmartRestaurant.Application.Restaurants.Chains.Commands.Create
{
    public class CreateChainModel : ICreateChainModel
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisabled { get; set; }
        public string OwnerId { get; set; }
    }
}