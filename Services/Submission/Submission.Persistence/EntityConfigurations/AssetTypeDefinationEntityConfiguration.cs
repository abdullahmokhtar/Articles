using Blocks.Core.Constraints;
using Blocks.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class AssetTypeDefinationEntityConfiguration : IEntityTypeConfiguration<AssetTypeDefination>
{
    public void Configure(EntityTypeBuilder<AssetTypeDefination> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Name).IsUnique();

        builder.Property(e => e.Name).HasEnumConversion().HasMaxLength(MaxLength.C64).IsRequired().HasColumnOrder(1);
        builder.Property(e => e.MaxFileSizeInMB).HasDefaultValue(5);
        builder.Property(e => e.DefaultFileExtension).HasMaxLength(MaxLength.C8).HasDefaultValue("pdf").IsRequired();

        builder.ComplexProperty(e => e.AllowedFileExtensions, builder =>
        {
            var convertor = BuilderExtension.BuildJsonListConverter<string>();
            builder.Property(f => f.Extensions)
            .HasConversion(convertor)
            .HasColumnName(builder.Metadata.PropertyInfo!.Name)
            .IsRequired();
        });
    }
}
