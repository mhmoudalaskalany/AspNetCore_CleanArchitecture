using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Domain.Entities.Identity;

namespace Template.Infrastructure.Configuration
{
    public class PermissionConfig : LookupConfig<Permission, int>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Id)
                .ValueGeneratedNever();
         
        }
    }
}