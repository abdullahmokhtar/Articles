using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(e => e.Discipline).HasMaxLength(64)
            .HasComment("The author's main field of study or research (e.g Biology, Computer Sciencr).");

        builder.Property(e => e.Degree).HasMaxLength(64)
           .HasComment("The author's highest acedimic qualification (e.g PhD, MSc).");
    }
}
