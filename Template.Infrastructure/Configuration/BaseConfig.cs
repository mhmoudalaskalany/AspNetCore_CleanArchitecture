﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities.Base;

namespace Template.Infrastructure.Configuration
{
    public class BaseConfig<TEntity , TId> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TId> where TId : struct
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(e => e.CreatedById).HasMaxLength(50);
            builder.Property(e => e.ModifiedById).HasMaxLength(50);
        }
    }
}
