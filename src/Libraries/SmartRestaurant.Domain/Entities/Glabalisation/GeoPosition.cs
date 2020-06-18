﻿using Microsoft.EntityFrameworkCore;
using SmartRestaurant.Domain.Common;
using System.Collections.Generic;

namespace SmartRestaurant.Domain.Entities.Glabalisation
{
    [Owned]
    public class GeoPosition : ValueObject
    {
        public string Latitude { get; protected set; }
        public string Longitude { get; protected set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new List<object>
            {
                Latitude,
                Longitude
            };
        }

        public static GeoPosition Create(string latitude, string longitude)
        {
            GeoPosition position = new GeoPosition()
            {
                Latitude = latitude,
                Longitude = longitude,
            };
            return position;
        }
    }
}
