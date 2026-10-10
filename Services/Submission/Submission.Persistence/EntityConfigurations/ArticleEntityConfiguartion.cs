using Blocks.EntityFramework;
using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

internal class ArticleEntityConfiguartion : EntityConfiguration<Article>
{
    public override void Configure(EntityTypeBuilder<Article> builder)
    {
        base.Configure(builder);
        builder.Property(a => a.Title).HasMaxLength(256).IsRequired();
        builder.Property(a => a.Scope).HasMaxLength(2048).IsRequired();
        builder.Property(a => a.Stage).HasEnumConversion();
        builder.Property(a => a.Type).HasEnumConversion();

        builder.HasOne(a => a.Journal)
            .WithMany(j => j.Articles)
            .HasForeignKey(a => a.JournalId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
