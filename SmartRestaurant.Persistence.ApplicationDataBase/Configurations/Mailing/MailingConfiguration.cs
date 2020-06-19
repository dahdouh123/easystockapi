﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmartRestaurant.Persistence.ApplicationDataBase.Configurations.Commun
{
    internal class MailingConfiguration : IEntityTypeConfiguration<Domain.Mailing>
    {
        public void Configure(EntityTypeBuilder<Domain.Mailing> b)
        {
            //inherit BaseEntity<TId> 
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();
            b.Property(a => a.Alias).HasMaxLength(5);
            b.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();


            b.HasMany(x => x.Users)
                .WithOne(cc => cc.Mailing)
                .HasForeignKey(cc => cc.MailingId)
                .OnDelete(DeleteBehavior.Cascade);


            b.ToTable("Mailings");
        }
    }
}
