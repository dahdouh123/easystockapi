﻿using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Domain.Common;
using SmartRestaurant.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Domain.Entities.Globalisation
{
    [Owned]
    public class Address
    {
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public GeoPosition GeoPosition { get; set; }
    }
}
