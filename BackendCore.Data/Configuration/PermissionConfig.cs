using BackendCore.Entities.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendCore.Data.Configuration
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