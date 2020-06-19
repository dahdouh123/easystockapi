﻿using System.Collections.Generic;

namespace SmartRestaurant.Diner.Models
{
    /// <summary>
    /// Used to manage dish Supplements
    /// </summary>
    public class SpecificationModel
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string NameFr { get; set; }
        public string NameEn { get; set; }
        public List<string> Possible_ValuesAr { get; set; }
        public List<string> Possible_ValuesFr { get; set; }
        public List<string> Possible_ValuesEn { get; set; }
    }
}
