﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartRestaurant.Domain.Commun;

namespace SmartRestaurant.Persistence.ApplicationDataBase.Configurations.Commun
{
    internal class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
    {
        public void Configure(EntityTypeBuilder<Speciality> b)
        {
            //inherit BaseEntity<TId> 
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Property(a => a.Alias).HasMaxLength(5);
            b.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            b.ToTable("Specialties");
        }
    }
}
