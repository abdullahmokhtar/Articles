using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class JournalEntityConfiguration : EntityConfiguration<Journal>
{
    public override void Configure(EntityTypeBuilder<Journal> builder)
    {
        base.Configure(builder);

        builder.Property(a => a.Name).HasMaxLength(64).IsRequired();
        builder.Property(a => a.Abreviation).HasMaxLength(8).IsRequired();

        builder.HasMany(j => j.Articles)
            .WithOne(a => a.Journal)
            .HasForeignKey(a => a.JournalId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
