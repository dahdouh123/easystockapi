﻿using SmartRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Domain.Entities
{
    public class Depense
    {
        public Guid Id { get; set; }
        public TypeDepense Type { get; set; }
        public decimal Somme { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Observations { get; set; }


    }
}
