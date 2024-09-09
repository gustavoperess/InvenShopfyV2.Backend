using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvenShopfy.API.Data.Mapping.Identity;

public class IdentityUserRoleMapping
    : IEntityTypeConfiguration<IdentityUserRole<long>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<long>> builder)
    {
        builder.ToTable("IdentityUserRole");
        builder.HasKey(r => new { r.UserId, r.RoleId });
    }
}