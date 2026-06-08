using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
