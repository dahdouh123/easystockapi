﻿using SmartRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Domain.Entities
{
    public class MouvementStock

    {
        public Guid Id { get; set; }

        public Guid ProduitId { get; set; }
        public DateTime DateMvt { get; set; }
        public string ProduitName { get;set; }
        public TypeMouv TypeMouvement { get; set; }
        public decimal Qte { get; set; }
    }
}
