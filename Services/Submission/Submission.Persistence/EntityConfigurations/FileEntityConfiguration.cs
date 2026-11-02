using Blocks.Core.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Submission.Persistence.EntityConfigurations;

internal class FileEntityConfiguration
{
    internal void Configure(ComplexPropertyBuilder<Domain.ValueObjects.File> builder)
    {
        builder.Property(e => e.OriginalName).HasMaxLength(MaxLength.C256).HasComment("Original full file name, with extension.");
        builder.Property(e => e.FileserverId).HasMaxLength(MaxLength.C64).IsRequired();
        builder.Property(e => e.Size).HasComment("Size of the file in kilobytes");

        builder.ComplexProperty(
            e => e.Extension, complexBuilder =>
            {
                complexBuilder.Property(n => n.Value)
                .HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                .HasMaxLength(MaxLength.C8);
            });

        builder.ComplexProperty(
            e => e.Name, complexBuilder =>
            {
                complexBuilder.Property(n => n.Value)
                .HasColumnName($"{builder.Metadata.ClrType.Name}_{complexBuilder.Metadata.PropertyInfo!.Name}")
                .HasMaxLength(MaxLength.C64).HasComment("Final name of the file after renaming");
            });
    }
}
