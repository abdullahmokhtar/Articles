using Blocks.EntityFramework.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Submission.Domain.Entities;

namespace Submission.Persistence.EntityConfigurations;

public class PersonEntityConfiguration : EntityConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.UserId).IsUnique();

        builder.HasDiscriminator(e => e.TypeDiscriminator)
            .HasValue<Person>(nameof(Person))
            .HasValue<Author>(nameof(Author));

        builder.Property(p => p.FirstName).HasMaxLength(64).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(64).IsRequired();
        builder.Property(p => p.Title).HasMaxLength(64);
        builder.Property(p => p.Affiliation).HasMaxLength(256).IsRequired()
            .HasComment("Institution or organization thet are associated with when they conduct their research.");

        builder.Property(e => e.UserId).IsRequired(false);

        builder.ComplexProperty(e => e.EmailAddress, builder =>
        {
            builder.Property(n => n.Value)
            .HasColumnName(builder.Metadata.PropertyInfo!.Name)
            .HasMaxLength(64);
        });


    }
}
