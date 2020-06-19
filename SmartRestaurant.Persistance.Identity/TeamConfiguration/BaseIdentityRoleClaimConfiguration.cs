﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRestaurant.Domain.BaseIdentity;

namespace SmartRestaurant.Persistance.Identity.TeamConfiguration
{
    public class BaseIdentityRoleClaimConfiguration : IEntityTypeConfiguration<BaseIdentityRoleClaim>
    {
        public void Configure(EntityTypeBuilder<BaseIdentityRoleClaim> builder)
        {
            // Primary key
            builder.HasKey(rc => rc.Id);

            // Maps to the AspNetRoleClaims table
            builder.ToTable("BaseRoleClaims");
        }
    }
}
