﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRestaurant.Domain.Clients.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartRestaurant.Persistance.Identity.Configuration
{
    public class SRIdentityRoleClaimConfiguration : IEntityTypeConfiguration<SRIdentityRoleClaim>
    {
        public void Configure(EntityTypeBuilder<SRIdentityRoleClaim> builder)
        {
            // Primary key
            builder.HasKey(rc => rc.Id);

            // Maps to the AspNetRoleClaims table
            builder.ToTable("SRRoleClaims");
        }
    }
}
