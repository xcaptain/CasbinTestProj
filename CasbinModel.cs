using Microsoft.EntityFrameworkCore;
using Casbin.NET.Adapter.EFCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CasbinTestProj
{
    public class CustomCasbinRuleTableConfiguration : IEntityTypeConfiguration<CasbinRule>
    {
        public void Configure(EntityTypeBuilder<CasbinRule> builder)
        {
            builder.ToTable("casbin_rules");
            // builder.ToTable("CasbinRule");
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.PType).HasColumnName("p_type");
            builder.Property(e => e.V0).HasColumnName("v0");
            builder.Property(e => e.V1).HasColumnName("v1");
            builder.Property(e => e.V2).HasColumnName("v2");
            builder.Property(e => e.V3).HasColumnName("v3");
            builder.Property(e => e.V4).HasColumnName("v4");
            builder.Property(e => e.V5).HasColumnName("v5");
        }
    }
}
