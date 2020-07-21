﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Diner.Models
{

    /// <summary>
    /// Used to manage Allergies, 
    /// </summary>
    public class AllergyModel
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameFr { get; set; }
        public string NameEn { get; set; }
    }
}
