﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Domain.Common
{
    public abstract class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
