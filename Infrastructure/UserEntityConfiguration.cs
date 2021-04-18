using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToContainer("Users");

            //builder.HasKey( x => x.UserName);

            builder.HasNoDiscriminator();

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.UserName).IsRequired();
            builder.HasPartitionKey(x => x.UserName);
            builder.Property(x => x.UserId).HasValueGenerator<GuidValueGenerator>();
            //builder.OwnsMany( user => user.);
        }
    }
}
